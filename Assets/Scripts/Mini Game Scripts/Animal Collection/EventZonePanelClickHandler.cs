using UnityEngine;
using UnityEngine.EventSystems;

public class EventZonePanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isEventZonePanelClicked = false;

    // This method is called when the GameObject is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Event zone panel clicked. Hiding panel...");
        isEventZonePanelClicked = true;
        AnimalGameManager.eventZonePanelActive = false;
    }
    
}

