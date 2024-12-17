using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerLogic : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AdvanceIntensity"))
        {
            SubsceneManager.instance.IncrementIntensity(1);
        }
        else if (other.gameObject.CompareTag("ExitPhobia"))
        {
            SubsceneManager.instance.ExitPhobia();
        }
        
    }
  
}
