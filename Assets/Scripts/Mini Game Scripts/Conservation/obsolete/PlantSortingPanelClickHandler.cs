using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class PlantSortingPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public GameObject plantsSwappedCounterText;

    public static bool isPlantSortingPanelClicked = false;

    public StarterAssetsInputs playerInput;

    // This method is called when the user clicks on the GameObject this script is attached to
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Plant Sorting panel clicked. Hiding panel...");
        isPlantSortingPanelClicked = true;
        //AnimalGameManager.plantSortingPanelActive = false;
        Debug.Log($"isPlantSortingPanelClicked: {isPlantSortingPanelClicked}, plantSortingPanleActive: {AnimalGameManager.plantSortingPanelActive}");
        playerInput.controlsLocked = false; // Unlock player controls when the panel is clicked
        plantsSwappedCounterText.SetActive(true);
    }
}

