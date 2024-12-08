using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadLighting : MonoBehaviour
{
    private void Start()
    {
        LightProbes.TetrahedralizeAsync();
        Debug.Log("Rebaked lights");
    }
}
