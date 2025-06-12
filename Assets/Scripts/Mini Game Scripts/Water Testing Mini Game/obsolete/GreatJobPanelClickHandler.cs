using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets; // Assuming you have a StarterAssets namespace for the WaterTestingManager

public class GreatJobPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool isGreatJobPanelClicked = false;

    public StarterAssetsInputs playerInput; 

    // Method called when clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Great Job panel has been clicked!"); // Debug.Log
        isGreatJobPanelClicked = true; // Set bool isGreatJobPanelClicked to true
        WaterTestingManager.greatJobPanelActive = false; // Set bool greatJobPanelActive to false
        playerInput.controlsLocked = false; // Unlock player controls
    }
}
