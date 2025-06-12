using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class EasternStarlingClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isEasternStarlingPanelClicked = false;

    public AnimalGameManager animalGameManager;

    public StarterAssetsInputs playerInput;  

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Eastern Starling panel clicked. Hiding panel...");
        isEasternStarlingPanelClicked = true;
        AnimalGameManager.easternStarlingPanelActive = false;
        //AnimalGameManager.dialogueIsActive = false;
        playerInput.controlsLocked = false; // Unlock player controls
    }
}
