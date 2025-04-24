using UnityEngine;
using UnityEngine.EventSystems;

public class DeerEventZonePanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    //public GameObject eventPanel;
    public static bool isDeerEventZonePanelClicked = false;

    // This method is called when the GameObject is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        //eventPanel.SetActive(false);
        Debug.Log("Event zone panel clicked. Hiding panel...");
        isDeerEventZonePanelClicked = true;
        AnimalGameManager.eventZonePanelActive = false;
    }
}
