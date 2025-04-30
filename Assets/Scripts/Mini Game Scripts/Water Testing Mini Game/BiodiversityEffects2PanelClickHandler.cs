using StarterAssets;
using UnityEngine;
using UnityEngine.EventSystems;

public class BiodiversityEffects2PanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool isEffectsOfBiodiversity2PanelClicked = false; // Bool to check if the panel is clicked

    public StarterAssetsInputs playerInput;

    public WaterTestingManager gameManagerScript;

    // Method called when clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Effects of biodiversity 2 has been clicked!"); // Debug.Log
        isEffectsOfBiodiversity2PanelClicked = true; // Set bool isEffectsOfAluminumPanelClicked to true
        WaterTestingManager.effectsOfBiodiversity2PanelActive = false; // Set bool effectsOfAluminumPanelActive to false;
        gameManagerScript.AreObjectivesComplete(); // Call the method to check if all objectives are complete
        WaterTestingManager.aPanelIsActive = false; // Set bool aPanelIsActive to false
        playerInput.controlsLocked = false; // Unlock player controls
    }
}
