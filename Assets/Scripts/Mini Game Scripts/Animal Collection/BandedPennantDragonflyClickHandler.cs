using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class BandedPennantDragonflyClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isBandedPennantDragonflyPanelClicked = false;

    public AnimalGameManager animalGameManager;

    public StarterAssetsInputs playerInput; 

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Banded Pennant Dragonfly panel clicked. Hiding panel...");
        isBandedPennantDragonflyPanelClicked = true;
        AnimalGameManager.bandedPennantDragonflyPanelActive = false;
        AnimalGameManager.dialogueIsActive = false;
        playerInput.controlsLocked = false; // Unlock player controls when the panel is clicked
        animalGameManager.DeactivateDialoguePanel(10);
    }
}
