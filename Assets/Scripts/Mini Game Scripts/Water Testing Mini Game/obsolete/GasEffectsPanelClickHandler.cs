using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets; // Assuming you have a StarterAssets namespace for player inputs

public class GasEffectsPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool isEffectsOfGasPanelClicked = false; // Variable to track if the panel has been clicked

    public StarterAssetsInputs playerInput;

    public WaterTestingManager gameManagerScript;

    // Method called when clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Effects of gas panel has been clicked!"); // Debug.Log
        isEffectsOfGasPanelClicked = true; // Set bool isEffectsOfGasPanelClicked to true
        WaterTestingManager.effectsOfGasPanelActive = false; // Set bool effectsOfGasPanelActive to false;
        gameManagerScript.AreObjectivesComplete(); // Call the method to check if all objectives are complete
        WaterTestingManager.aPanelIsActive = false; // Set bool aPanelIsActive to false
        playerInput.controlsLocked = false; // Unlock player controls
    }
}
