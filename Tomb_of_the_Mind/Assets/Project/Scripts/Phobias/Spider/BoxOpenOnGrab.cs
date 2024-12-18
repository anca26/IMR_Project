using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BoxOpenOnGrab : MonoBehaviour
{
    public GameObject openedBoxPrefab; 
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Spawn the opened box prefab at the current position/rotation
        Instantiate(openedBoxPrefab, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}