using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class BaldEagleClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isBaldEaglePanelClicked = false;

    public AnimalGameManager animalGameManager;

    public StarterAssetsInputs playerInput;

    // This method is called when the user clicks on the GameObject this script is attached to
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Bald Eagle panel clicked. Hiding panel...");
        isBaldEaglePanelClicked = true;
        AnimalGameManager.baldEaglePanelActive = false;
        //AnimalGameManager.dialogueIsActive = false;
        playerInput.controlsLocked = false; // Unlock player controls
    }
}
