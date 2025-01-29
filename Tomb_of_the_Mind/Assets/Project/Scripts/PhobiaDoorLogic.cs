using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhobiaDoorLogic : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    [SerializeField]
    private Animator _canvasAnimator;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.LoadScene(sceneName);
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneI(sceneName));
        
    }
    public IEnumerator LoadSceneI(string sceneName)
    {
        _canvasAnimator.Play("Fade_Out");
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetSceneByName(sceneName) != null)
            SceneManager.LoadScene(sceneName);
    }
}
