using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class SubsceneManager : MonoBehaviour
{
    public static SubsceneManager instance = null;

    [SerializeField]
    private GameObject _cameraObject;
    [SerializeField]
    public Animator canvasAnimator;

    [SerializeField]
    private List<GameObject> _subscenes = new List<GameObject>();
    private int _currentSubScene = 0;

    private float _intensityTransitionTime = 1f;

    private Coroutine _currentCoroutine = null;

    private List<AudioSource> _audioSources;
    private List<float> _initialVolumes;
    private float _multiplySpeed = 1.1f;

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
        _audioSources = new List<AudioSource>(FindObjectsOfType<AudioSource>());
        StartCoroutine(ExitPhobiaI());
        //SceneManager.LoadScene(0); // loads door scene
        
    }
    public IEnumerator ExitPhobiaI()
    {
        canvasAnimator.Play("Fade_Out");
        int iterations = 100;

        float waitTime = 2.0f / 100;

        while(iterations > 0)
        {
            _audioSources.ForEach(a => a.volume /= _multiplySpeed);

            iterations--;
            yield return new WaitForSeconds(waitTime);
        }

        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("RoomsScene");
    }

    public void IncrementIntensity(int amount)
    {
        _audioSources = new List<AudioSource>(FindObjectsOfType<AudioSource>());
        _initialVolumes = _audioSources.Select(t => t.volume).ToList();
        if(amount == 0)
        {
            _audioSources.ForEach(t => t.volume = 0.0001f);
        }
        if (_currentCoroutine == null)
            _currentCoroutine = StartCoroutine(IncrementIntensityI(amount));
    }
    public IEnumerator IncrementIntensityI(int amount)
    {
        if(canvasAnimator == null)
        {
            Debug.LogError("[ERR] NO CANVAS ANIMATOR ON SUBSCENE MANAGER");
        }
        int iterations = 100;
        float waitTime = _intensityTransitionTime / iterations;
        if(amount != 0)
        {
            canvasAnimator.Play("Fade_Out");

            iterations = 100;


            while (iterations > 0)
            {
                _audioSources.ForEach(a => a.volume /= _multiplySpeed);

                iterations--;
                yield return new WaitForSeconds(waitTime);
            }


        }

        _subscenes[_currentSubScene].SetActive(false); // disabling previous intensity subscene

        _currentSubScene += amount;

        if (_currentSubScene >= _subscenes.Count)
        {
            _currentSubScene = _subscenes.Count - 1;
            Debug.LogWarning($"Reached max intensity. Can't incrase further than {_currentSubScene + 1}");

            yield return null;
        }

        _subscenes[_currentSubScene].SetActive(true);  // enabling new intensity subscene

        iterations = 100;

        //if (amount != 0)      
            while (iterations > 0)
            {
                for (int i = 0; i < _audioSources.Count; i++)
                    _audioSources[i].volume = Mathf.Min(_multiplySpeed * _audioSources[i].volume, _initialVolumes[i]);

                iterations--;
                yield return new WaitForSeconds(waitTime);
            }


        if(this._cameraObject != null)
        {
            Transform newTransformData = _subscenes[_currentSubScene].GetComponent<IntensityManager>().GetCameraTeleportData();
            this._cameraObject.transform.position = newTransformData.position;
            this._cameraObject.transform.rotation = newTransformData.rotation;
            Debug.Log("Changed camera position");
            Debug.Log($"New camera position: {newTransformData.position}");
        } else { Debug.Log("No camera object found"); }

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
