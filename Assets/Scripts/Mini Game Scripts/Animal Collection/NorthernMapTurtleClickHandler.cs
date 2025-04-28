using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;


public class NorthernMapTurtleClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isNorthernMapTurtlePanelClicked = false;

    public AnimalGameManager animalGameManager;

    public StarterAssetsInputs playerInput;

    // This method is called when the user clicks on the GameObject this script is attached to
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Northern Map Turtle panel clicked. Hiding panel...");
        isNorthernMapTurtlePanelClicked = true;
        AnimalGameManager.northernMapTurtlePanelActive = false;
        AnimalGameManager.dialogueIsActive = false;
        playerInput.cursorLocked = false; // Lock the cursor
        animalGameManager.DeactivateDialoguePanel(9);
    }
    
}
