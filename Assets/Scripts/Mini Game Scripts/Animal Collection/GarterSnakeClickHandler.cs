using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;

public class GarterSnakeClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isGarterSnakePanelClicked = false;

    public AnimalGameManager animalGameManager;

    public StarterAssetsInputs playerInput;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Garter Snake panel clicked. Hiding panel...");
        isGarterSnakePanelClicked = true;
        AnimalGameManager.garterSnakePanelActive = false;
        AnimalGameManager.dialogueIsActive = false;
        playerInput.controlsLocked = false; // Unlock player controls when the panel is clicked
        animalGameManager.DeactivateDialoguePanel(8);
    }
}

