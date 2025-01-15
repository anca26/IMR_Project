using UnityEngine;
using UnityEngine.InputSystem;

public class MoveHead : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 1.0f;

    private Vector2 lookInput;

    // Input Action Reference
    [SerializeField]
    private InputActionReference lookAction;

    private void OnEnable()
    {
        // Enable the input action
        lookAction.action.Enable();
    }

    private void OnDisable()
    {
        // Disable the input action
        lookAction.action.Disable();
    }

    void Update()
    {
        // Read input
        lookInput = lookAction.action.ReadValue<Vector2>();

        // Apply rotation
        transform.Rotate(Vector3.up * lookInput.x * sensitivity);
        transform.Rotate(Vector3.right * -lookInput.y * sensitivity);
    }
}