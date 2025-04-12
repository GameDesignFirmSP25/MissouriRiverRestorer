using UnityEngine;
using UnityEngine.EventSystems;

public class CleanWaterPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool isCleanWaterPanelClicked = false;

    // Method called when clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clean Water panel has been clicked!"); // Debug.Log
        isCleanWaterPanelClicked = true; // Set bool isCleanWaterPanelClicked to true
        WaterTestingManager.cleanWaterPanelActive = false; // Set bool cleanWaterPanelActive to false
    }
}
