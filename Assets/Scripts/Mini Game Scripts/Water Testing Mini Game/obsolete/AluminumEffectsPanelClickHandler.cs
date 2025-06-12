using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets; // Importing StarterAssets namespace for PointerEventData

public class AluminumEffectsPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool isEffectsOfAluminumPanelClicked = false; // Variable to track if the panel has been clicked

    public StarterAssetsInputs playerInput;

    public WaterTestingManager gameManagerScript;

    // Method called when clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Effects of aluminum panel has been clicked!"); // Debug.Log
        isEffectsOfAluminumPanelClicked = true; // Set bool isEffectsOfAluminumPanelClicked to true
        WaterTestingManager.effectsOfAluminumPanelActive = false; // Set bool effectsOfAluminumPanelActive to false;
        gameManagerScript.AreObjectivesComplete(); // Call the method to check if all objectives are complete
        WaterTestingManager.aPanelIsActive = false; // Set bool aPanelIsActive to false
        playerInput.controlsLocked = false; // Unlock player controls
    }
}
