using UnityEngine;
using UnityEngine.EventSystems;

public class BaldEagleClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool baldEagleClicked = false;
    // This method is called when the user clicks on the GameObject this script is attached to
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Bald Eagle panel clicked. Hiding panel...");
        baldEagleClicked = true;
        AnimalGameManager.dialogueIsActive = false;
        AnimalGameManager.baldEaglePanelActive = false;
    }
}
