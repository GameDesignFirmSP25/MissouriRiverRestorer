using UnityEngine;
using UnityEngine.EventSystems;

public class GarterSnakeClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool garterSnakeClicked = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Garter Snake panel clicked. Hiding panel...");
        garterSnakeClicked = true;
        AnimalGameManager.dialogueIsActive = false;
        AnimalGameManager.garterSnakePanelActive = false;
    }
}

