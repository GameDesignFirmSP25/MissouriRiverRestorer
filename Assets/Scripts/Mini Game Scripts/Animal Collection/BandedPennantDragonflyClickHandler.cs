using UnityEngine;
using UnityEngine.EventSystems;

public class BandedPennantDragonflyClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isBandedPennantDragonflyPanelClicked = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Banded Pennant Dragonfly panel clicked. Hiding panel...");
        isBandedPennantDragonflyPanelClicked = true;
        AnimalGameManager.bandedPennantDragonflyPanelActive = false;
        AnimalGameManager.dialogueIsActive = false;
    }
}
