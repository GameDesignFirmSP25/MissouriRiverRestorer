using UnityEngine;
using UnityEngine.EventSystems;

public class BeaverClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isBeaverPanelClicked = false;
    // This method is called when the user clicks on the GameObject this script is attached to
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Beaver panel clicked. Hiding panel...");
        isBeaverPanelClicked = true;
        AnimalGameManager.beaverPanelActive = false;
        AnimalGameManager.dialogueIsActive = false;
    }
}