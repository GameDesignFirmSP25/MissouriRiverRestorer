using UnityEngine;
using UnityEngine.EventSystems;

public class PlantSortingPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public GameObject plantsSwappedCounterText;

    public static bool isPlantSortingPanelClicked = false;
    // This method is called when the user clicks on the GameObject this script is attached to
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Plant Sorting panel clicked. Hiding panel...");
        isPlantSortingPanelClicked = true;
        AnimalGameManager.plantSortingPanelActive = false;
        Debug.Log($"isPlantSortingPanelClicked: {isPlantSortingPanelClicked}, plantSortingPanleActive: {AnimalGameManager.plantSortingPanelActive}");
        
        plantsSwappedCounterText.SetActive(true);
    }
}

