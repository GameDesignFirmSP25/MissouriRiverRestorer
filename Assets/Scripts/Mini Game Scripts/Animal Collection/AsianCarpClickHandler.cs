using UnityEngine;
using UnityEngine.EventSystems;

public class AsianCarpClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isAsianCarpPanelClicked = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Asian Carp panel clicked. Hiding panel...");
        isAsianCarpPanelClicked = true;
        AnimalGameManager.asianCarpPanelActive = false;
        AnimalGameManager.dialogueIsActive = false;
    }
}
