using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class AsianCarpClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isAsianCarpPanelClicked = false;

    public AnimalGameManager animalGameManager; // Reference to the AnimalGameManager script

    public StarterAssetsInputs playerInput;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Asian Carp panel clicked. Hiding panel...");
        isAsianCarpPanelClicked = true;
        AnimalGameManager.asianCarpPanelActive = false;
        //AnimalGameManager.dialogueIsActive = false;
        playerInput.controlsLocked = false; // unlock player controls
    }
}
