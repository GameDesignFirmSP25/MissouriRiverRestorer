using UnityEngine;
using UnityEngine.EventSystems;

public class AsianCarpClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool asianCarpClicked = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Asian Carp panel clicked. Hiding panel...");
        asianCarpClicked = true;
        AnimalGameManager.dialogueIsActive = false;
        AnimalGameManager.asianCarpPanelActive = false;
    }
}
