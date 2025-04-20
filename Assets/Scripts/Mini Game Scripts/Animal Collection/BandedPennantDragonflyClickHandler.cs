using UnityEngine;
using UnityEngine.EventSystems;

public class BandedPennantDragonflyClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool bandedPennantDragonflyClicked = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Banded Pennant Dragonfly panel clicked. Hiding panel...");
        bandedPennantDragonflyClicked = true;
        AnimalGameManager.dialogueIsActive = false;
        AnimalGameManager.bandedPennantDragonflyPanelActive = false;
    }
}
