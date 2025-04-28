using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class BeaverClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isBeaverPanelClicked = false;

    public AnimalGameManager animalGameManager;

    public StarterAssetsInputs playerInput;

    // This method is called when the user clicks on the GameObject this script is attached to
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Beaver panel clicked. Hiding panel...");
        isBeaverPanelClicked = true;
        AnimalGameManager.beaverPanelActive = false;
        AnimalGameManager.dialogueIsActive = false;
        playerInput.controlsLocked = false; // Unlock player controls when the panel is clicked
        animalGameManager.DeactivateDialoguePanel(4);
    }
}