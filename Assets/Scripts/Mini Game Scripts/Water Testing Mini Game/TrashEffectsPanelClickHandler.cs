using UnityEngine;
using UnityEngine.EventSystems;

public class TrashEffectsPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool isEffectsOfTrashPanelClicked = false; // Variable to track if the panel has been clicked

    public WaterTestingManager gameManagerScript;

    // Method called when clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Effects of trash panel has been clicked!"); // Debug.Log
        isEffectsOfTrashPanelClicked = true; // Set bool isEffectsOfTrashPanelClicked to true
        WaterTestingManager.effectsOfTrashPanelActive = false; // Set bool effectsOfTrashPanelActive to false;
        gameManagerScript.AreObjectivesComplete(); // Call the method to check if all objectives are complete
        WaterTestingManager.aPanelIsActive = false; // Set bool aPanelIsActive to false
    }
}
