using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerLogic : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter called. Collided with: {other.gameObject.name}");
        if (other.gameObject.CompareTag("AdvanceIntensity"))
        {
            Debug.Log("AdvanceIntensity trigger detected. Incrementing intensity by 1.");
            SubsceneManager.instance.IncrementIntensity(1);
        }
        else if (other.gameObject.CompareTag("ExitPhobia"))
        {
            SubsceneManager.instance.ExitPhobia();
        }
        
    }
  
}
