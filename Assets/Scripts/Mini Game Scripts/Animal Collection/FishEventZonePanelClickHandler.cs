using UnityEngine;
using UnityEngine.EventSystems;

public class FishEventZonePanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject objectivesPanel;

    //public GameObject eventPanel;
    public static bool isFishEventZonePanelClicked = false;

    // This method is called when the GameObject is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        //eventPanel.SetActive(false);
        Debug.Log("Fish Event zone panel clicked. Hiding panel...");
        isFishEventZonePanelClicked = true;
        AnimalGameManager.eventZonePanelActive = false;
        if (AnimalGameManager.fishEventZoneComplete)
        {
            objectivesPanel.SetActive(true); // show objectives panel
            AnimalGameManager.objectivesShown = true; // set objectivesShown to true
        }
    }
}
