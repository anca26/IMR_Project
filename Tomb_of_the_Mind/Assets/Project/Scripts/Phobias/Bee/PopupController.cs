using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    public GameObject popupPanel; 
    public Button closeButton;

    void Start()
    {
        popupPanel.SetActive(true); 


        closeButton.onClick.AddListener(HidePopup);
    }

    public void HidePopup()
    {
        popupPanel.SetActive(false);
    }
}
