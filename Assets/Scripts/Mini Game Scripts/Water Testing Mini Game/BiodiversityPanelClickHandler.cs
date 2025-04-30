using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets; // Assuming you have a StarterAssets namespace for the WaterTestingManager

public class BiodiversityPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool isBiodiversityPanelClicked = false;

    public StarterAssetsInputs playerInput; // Reference to the player input script

    // Method called when clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Biodiversity Panel Clicked!"); // Debug.Log
        isBiodiversityPanelClicked = true; // Set bool isbiodiversityPanelClicked to true
        WaterTestingManager.biodiversityPanelActive = false; // Set bool biodiversityPanelActive to false
        WaterTestingManager.aPanelIsActive = false; // Set bool aPanelActive to false;
        playerInput.controlsLocked = false; // Unlock player controls
    }
}
