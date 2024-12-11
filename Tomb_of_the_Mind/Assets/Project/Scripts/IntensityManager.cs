using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

    public class IntensityManager : MonoBehaviour
{
    public static IntensityManager instance = null;

    [SerializeField]
    private List<GameObject> _subscenes = new List<GameObject>();
    private int _currentSubScene = 0;

    private void Awake()
    {        
        Init();                                         // initialize singleton

        foreach (GameObject scene in _subscenes)  // disabling all the scenes
        {
            scene.SetActive(false);
        }
        IncrementIntensity(0);                          // basically, enables the first scene so the user can play
    }
    /// <summary>
    /// Instantiates singleton
    /// </summary>
    private void Init()
    {
        if(IntensityManager.instance == null)
        {
            IntensityManager.instance = this;
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
        _subscenes[_currentSubScene].SetActive(false); // disabling previous intensity subscene

        _currentSubScene += amount;
        if (_currentSubScene >= _subscenes.Count)
        {
            _currentSubScene = _subscenes.Count - 1;
            Debug.LogWarning($"Reached max intensity. Can't incrase further than {_currentSubScene + 1}");
            return;
        }

        _subscenes[_currentSubScene].SetActive(true);  // enabling new intensity subscene
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
                    _subscenes[i] = new GameObject();
                    _subscenes[i].transform.parent = gameObject.transform;

                    _subscenes[i].name = name;
                }
                else
                {
                    if (obj.gameObject.name != name) // if the object found does not have the name of the subscene, we create the subscene and add it
                    {
                        _subscenes[i] = new GameObject();
                        _subscenes[i].transform.parent = gameObject.transform;

                        _subscenes[i].name = name;
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
