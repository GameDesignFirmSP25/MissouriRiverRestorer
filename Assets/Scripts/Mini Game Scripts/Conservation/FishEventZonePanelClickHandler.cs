using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class FishEventZonePanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    //public GameObject eventPanel;
    public static bool isFishEventZonePanelClicked = false;

    public StarterAssetsInputs playerInput;

    // This method is called when the GameObject is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        //eventPanel.SetActive(false);
        Debug.Log("Fish Event zone panel clicked. Hiding panel...");
        isFishEventZonePanelClicked = true;
        AnimalGameManager.eventZonePanelActive = false;
        playerInput.controlsLocked = false;
    }
}
