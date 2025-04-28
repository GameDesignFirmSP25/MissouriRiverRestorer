using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class SnappingTurtleClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isSnappingTurtlePanelClicked = false;

    public AnimalGameManager animalGameManager; // Reference to the AnimalGameManager

    public StarterAssetsInputs playerInput;

    // This method is called when the object is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Snapping Turtle panel is clicked. Hiding panel...");
        isSnappingTurtlePanelClicked = true;
        AnimalGameManager.snappingTurtlePanelActive = false;
        AnimalGameManager.dialogueIsActive = false;
        playerInput.controlsLocked = false; // Unlock player controls when the panel is clicked
        animalGameManager.DeactivateDialoguePanel(7);
    }
}

