using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class MuskratClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isMuskratPanelClicked = false;

    public AnimalGameManager animalGameManager;

    public StarterAssetsInputs playerInput;

    // This method is called when the user clicks on the GameObject this script is attached to
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Muskrat panel clicked. Hiding panel...");
        isMuskratPanelClicked = true;
        AnimalGameManager.muskratPanelActive = false;
        AnimalGameManager.dialogueIsActive = false;
        playerInput.controlsLocked = false; // Unlock player controls
        animalGameManager.DeactivateDialoguePanel(6);
    }
}

