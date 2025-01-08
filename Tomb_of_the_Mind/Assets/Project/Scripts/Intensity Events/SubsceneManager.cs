using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

    public class SubsceneManager : MonoBehaviour
{
    public static SubsceneManager instance = null;

    [SerializeField]
    public Animator canvasAnimator;

    [SerializeField]
    private List<GameObject> _subscenes = new List<GameObject>();
    private int _currentSubScene = 0;

    private float _intensityTransitionTime = 1f;

    private Coroutine _currentCoroutine = null;

    private void Awake()
    {
        Init();                                   // initialize singleton

        foreach (GameObject scene in _subscenes)  // disabling all the scenes
        {
            scene.SetActive(false);
        }
        canvasAnimator.Play("Fade_Still");

        _subscenes[0].SetActive(true);

        IncrementIntensity(0);                    // basically, enables the first scene so the user can play

    }
    /// <summary>
    /// Instantiates singleton
    /// </summary>
    private void Init()
    {
        if(SubsceneManager.instance == null)
        {
            SubsceneManager.instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    public void ExitPhobia()
    {
        SceneManager.LoadScene(0); // loads door scene
    }
    public void IncrementIntensity(int amount)
    {
        if(_currentCoroutine == null)
            _currentCoroutine = StartCoroutine(IncrementIntensityI(amount));
    }
    public IEnumerator IncrementIntensityI(int amount)
    {
        if(canvasAnimator == null)
        {
            Debug.LogError("[ERR] NO CANVAS ANIMATOR ON SUBSCENE MANAGER");
        }  
      
        if(amount != 0)
            canvasAnimator.Play("Fade_Out");
        yield return new WaitForSeconds(_intensityTransitionTime);
        
        _subscenes[_currentSubScene].SetActive(false); // disabling previous intensity subscene

        _currentSubScene += amount;
        if (_currentSubScene >= _subscenes.Count)
        {
            _currentSubScene = _subscenes.Count - 1;
            Debug.LogWarning($"Reached max intensity. Can't incrase further than {_currentSubScene + 1}");

            yield return null;
        }

        _subscenes[_currentSubScene].SetActive(true);  // enabling new intensity subscene

        if (amount != 0)      
            yield return new WaitForSeconds(_intensityTransitionTime);

        canvasAnimator.Play("Fade_In");

        _currentCoroutine = null;
    }
    private GameObject CreateChild(Transform parent, string name)
    {
        GameObject child = new GameObject();
        child.transform.parent = parent;
        child.name = name;

        child.AddComponent<IntensityManager>();

        return child;
    }
    public void ManageSubScenes()
    {
        for(int i = 0; i < _subscenes.Count; i++)
        {
            _subscenes[i] = null;

            string name = $"intensity{i + 1}";
            GameObject scene = _subscenes[i];
            if(scene == null)
            {
                
                Transform obj = i < gameObject.transform.childCount ? gameObject.transform.GetChild(i) : null;
                if (obj == null) // there is no child
                {
                    _subscenes[i] = CreateChild(gameObject.transform, name);
                }
                else
                {
                    if (obj.gameObject.name != name) // if the object found does not have the name of the subscene, we create the subscene and add it
                    {
                        _subscenes[i] = CreateChild(gameObject.transform, name);
                    }
                    else
                    {
                        _subscenes[i] = obj.gameObject;
                    }
                }
            }
        }
    }

}
