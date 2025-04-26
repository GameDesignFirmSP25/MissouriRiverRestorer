using UnityEngine;
using UnityEngine.EventSystems;

public class BirdEventZonePanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject objectivesPanel;

    //public GameObject eventPanel;
    public static bool isBirdEventZonePanelClicked = false;

    // This method is called when the GameObject is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        //eventPanel.SetActive(false);
        Debug.Log("Bird Event zone panel clicked. Hiding panel...");
        isBirdEventZonePanelClicked = true;
        AnimalGameManager.eventZonePanelActive = false;
        if (AnimalGameManager.birdEventZoneComplete)
        {
            objectivesPanel.SetActive(true); // show objectives panel
        }
    }
}
