using UnityEngine;
using UnityEngine.EventSystems;

public class BeaverClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool beaverClicked = false;
    // This method is called when the user clicks on the GameObject this script is attached to
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Beaver panel clicked. Hiding panel...");
        beaverClicked = true;
        AnimalGameManager.dialogueIsActive = false;
        AnimalGameManager.beaverPanelActive = false;
    }
}