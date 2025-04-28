using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;


public class PaintedLadyButterflyClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isPaintedLadyButterflyPanelClicked = false;

    public AnimalGameManager animalGameManager;

    public StarterAssetsInputs playerInput;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Painted Lady Butterfly panel clicked. Hiding panel...");
        isPaintedLadyButterflyPanelClicked = true;
        AnimalGameManager.paintedLadyButterflyPanelActive = false;
        AnimalGameManager.dialogueIsActive = false;
        playerInput.cursorLocked = false; // Unlock the cursor when the panel is clicked
        animalGameManager.DeactivateDialoguePanel(11);
    }
}
