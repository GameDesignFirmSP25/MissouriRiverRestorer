using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class EndOfGamePanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isEndOfGamePanelClicked = false;

    public StarterAssetsInputs playerInput;

    // This method is called when the user clicks on the GameObject this script is attached to
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("End of game panel clicked. Hiding panel...");
        isEndOfGamePanelClicked = true;
        AnimalGameManager.endOfGamePanelActive = false;
        playerInput.controlsLocked = false;
    }
}
   
