using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class DeerEventZonePanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    //public GameObject eventPanel;
    public static bool isDeerEventZonePanelClicked = false;

    public StarterAssetsInputs playerInput;

    // This method is called when the GameObject is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        //eventPanel.SetActive(false);
        Debug.Log("Event zone panel clicked. Hiding panel...");
        isDeerEventZonePanelClicked = true;
        AnimalGameManager.eventZonePanelActive = false;
        playerInput.controlsLocked = false;
    }
}
