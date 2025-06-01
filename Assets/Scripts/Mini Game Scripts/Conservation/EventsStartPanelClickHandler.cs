using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class EventsStartPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool isEventsStartPanelClicked = false;

    public StarterAssetsInputs playerInput;

    // This method is called when the user clicks on the GameObject this script is attached to
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Events Start panel clicked. Hiding panel...");
        isEventsStartPanelClicked = true;
        playerInput.controlsLocked = false; // Unlock player controls
    }
}
