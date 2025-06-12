using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class BirdEventZonePanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    //public GameObject eventPanel;
    public static bool isBirdEventZonePanelClicked = false;

    public StarterAssetsInputs playerInput;

    // This method is called when the GameObject is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        //eventPanel.SetActive(false);
        Debug.Log("Bird Event zone panel clicked. Hiding panel...");
        isBirdEventZonePanelClicked = true;
        AnimalGameManager.eventZonePanelActive = false;
        playerInput.controlsLocked = false;
    }
}
