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
        
        transform.Rotate( Vector3.up    * rotateHorizontal * sensitivity); 
        transform.Rotate(-Vector3.right * rotateVertical   * sensitivity);
        
    }
}
