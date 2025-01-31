using System.Collections;
using UnityEngine;
using TMPro; 

public class TimerDisplay : MonoBehaviour
{
    public TextMeshProUGUI timerText; 
    public GameObject[] objectsToShow;
    private float remainingTime;

    void OnEnable()
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
