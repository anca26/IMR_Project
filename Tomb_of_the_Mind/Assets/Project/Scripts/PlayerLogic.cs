using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerLogic : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("AdvanceIntensity"))
        {
            IntensityManager.instance.IncrementIntensity(1);
        }
        else if (collision.collider.CompareTag("ExitPhobia"))
        {
            IntensityManager.instance.ExitPhobia();
        }
    }
}
