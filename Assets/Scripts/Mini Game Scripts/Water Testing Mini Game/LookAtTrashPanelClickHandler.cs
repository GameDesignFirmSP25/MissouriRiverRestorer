using UnityEngine;
using UnityEngine.EventSystems;

public class LookAtTrashPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool isLookAtTrashPanelClicked = false;

    // Method called when clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Look At Trash panel has been clicked!"); // Debug.Log
        isLookAtTrashPanelClicked = true; // Set bool isLookAtTrashPanelClicked to true
        WaterTestingManager.lookAtTrashPanelActive = false; // Set bool lookAtTrashPanelActive to false;
    }
}
