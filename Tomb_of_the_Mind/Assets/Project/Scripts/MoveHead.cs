using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MoveHead : MonoBehaviour
{
    [SerializeField]
    private float sensitivity;
    void Update()
    {
        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y");
        transform.Rotate(transform.up * rotateHorizontal * sensitivity); 
        transform.Rotate(-transform.right * rotateVertical * sensitivity);
    }
}
