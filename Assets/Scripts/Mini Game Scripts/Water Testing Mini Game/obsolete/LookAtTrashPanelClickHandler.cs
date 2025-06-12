using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets; // Assuming StarterAssets is the namespace where WaterTestingManager is defined

public class LookAtTrashPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool isLookAtTrashPanelClicked = false;

    public StarterAssetsInputs playerInput;

    // Method called when clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Look At Trash panel has been clicked!"); // Debug.Log
        isLookAtTrashPanelClicked = true; // Set bool isLookAtTrashPanelClicked to true
        WaterTestingManager.lookAtTrashPanelActive = false; // Set bool lookAtTrashPanelActive to false;
        WaterTestingManager.aPanelIsActive = false; // Set bool aPanelActive to false;
        playerInput.controlsLocked = false; // Unlock player controls
    }
}
