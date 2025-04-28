using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class RaccoonClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isRaccoonPanelClicked = false;

    public AnimalGameManager animalGameManager; // Reference to the AnimalGameManager

    public StarterAssetsInputs playerInput;

    // This method is called when the user clicks on the GameObject this script is attached to
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Raccoon panel clicked. Hiding panel...");
        isRaccoonPanelClicked = true;
        AnimalGameManager.raccoonPanelActive = false;
        AnimalGameManager.dialogueIsActive = false;
        playerInput.controlsLocked = false; // Unlock player controls when the panel is clicked
        animalGameManager.DeactivateDialoguePanel(5);
    }
}
