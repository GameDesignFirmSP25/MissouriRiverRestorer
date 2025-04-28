using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class DialoguePanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isDialoguePanelClicked = false; // Variable to track if the dialogue panel has been clicked

    public StarterAssetsInputs playerInput;

    // Method called when the panel is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Dialogue panel has been clicked!"); // Log message for debugging
        isDialoguePanelClicked = true; // Set the boolean to true
        playerInput.controlsLocked = false; // Unlock player controls
    }
}
