using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class WhiteTailedDeerClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isWhiteTailedDeerPanelClicked = false;

    public AnimalGameManager animalGameManager;

    public StarterAssetsInputs playerInput;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("White Tailed Deer panel clicked. Hiding panel...");
        isWhiteTailedDeerPanelClicked = true;
        AnimalGameManager.whiteTailedDeerPanelActive = false;
        //AnimalGameManager.dialogueIsActive = false;
        playerInput.controlsLocked = false; // Unlock player controls when the panel is clicked
    }
}
