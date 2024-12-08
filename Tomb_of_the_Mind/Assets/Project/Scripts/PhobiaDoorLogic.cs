using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhobiaDoorLogic : MonoBehaviour
{
    [SerializeField]
    private string sceneName;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (SceneManager.GetSceneByName(sceneName) != null)
                SceneManager.LoadScene(sceneName);
        }
    }
}
