using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets; // Assuming you have a StarterAssets namespace for the WaterTestingManager

public class CleanWaterPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool isCleanWaterPanelClicked = false;

    public StarterAssetsInputs playerInput;

    // Method called when clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clean Water panel has been clicked!"); // Debug.Log
        isCleanWaterPanelClicked = true; // Set bool isCleanWaterPanelClicked to true
        WaterTestingManager.cleanWaterPanelActive = false; // Set bool cleanWaterPanelActive to false
        playerInput.controlsLocked = false; // Unlock player controls
    }
}
