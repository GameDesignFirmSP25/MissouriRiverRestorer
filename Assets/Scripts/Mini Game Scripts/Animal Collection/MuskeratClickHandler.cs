using UnityEngine;
using UnityEngine.EventSystems;

public class MuskeratClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool muskeratClicked = false;
    // This method is called when the user clicks on the GameObject this script is attached to
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Muskrat panel clicked. Hiding panel...");
        muskeratClicked = true;
        AnimalGameManager.dialogueIsActive = false;
        AnimalGameManager.muskratPanelActive = false;
    }
}

