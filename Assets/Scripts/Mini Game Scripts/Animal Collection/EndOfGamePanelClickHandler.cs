using UnityEngine;
using UnityEngine.EventSystems;

public class EndOfGamePanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isEndOfGamePanelClicked = false;
    // This method is called when the user clicks on the GameObject this script is attached to
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("End of game panel clicked. Hiding panel...");
        isEndOfGamePanelClicked = true;
        AnimalGameManager.endOfGamePanelActive = false;
    }
}
   
