using System.Collections;
using UnityEngine;
using TMPro; 

public class TimerDisplay : MonoBehaviour
{
    public TextMeshProUGUI timerText; 
    public GameObject[] objectsToShow;
    public static TimerDisplay Instance;
    private float remainingTime;

    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    void Start()
    {
        StartNewTimer(15); 
    }
    public void StartNewTimer(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(Countdown(duration));
    }

    IEnumerator Countdown(float duration)
    {
        remainingTime = duration;
        
        while (remainingTime > 0)
        {
            timerText.text = "Timer: " + remainingTime.ToString("0"); 
            yield return new WaitForSeconds(1f);
            remainingTime--;
        }

        timerText.text = "^.^";
        ShowObjects();
    }

    void ShowObjects()
    {
        foreach (GameObject obj in objectsToShow)
        {
            obj.SetActive(true); 
        }
    }
}
