using UnityEngine;
using UnityEngine.EventSystems;

public class EventsStartPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool isEventsStartPanelClicked = false;

    // This method is called when the user clicks on the GameObject this script is attached to
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Events Start panel clicked. Hiding panel...");
        isEventsStartPanelClicked = true;
        
    }
}
