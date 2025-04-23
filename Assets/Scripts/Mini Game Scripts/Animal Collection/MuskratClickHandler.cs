using UnityEngine;
using UnityEngine.EventSystems;

public class MuskratClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isMuskratPanelClicked = false;
    // This method is called when the user clicks on the GameObject this script is attached to
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Muskrat panel clicked. Hiding panel...");
        isMuskratPanelClicked = true;
        AnimalGameManager.muskratPanelActive = false;
        AnimalGameManager.dialogueIsActive = false;
    }
}

