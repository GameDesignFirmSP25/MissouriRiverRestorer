using UnityEngine;
using UnityEngine.EventSystems;


public class PaintedLadyButterflyClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool paintedLadyButterflyClicked = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Painted Lady Butterfly panel clicked. Hiding panel...");
        paintedLadyButterflyClicked = true;
        AnimalGameManager.dialogueIsActive = false;
        AnimalGameManager.paintedLadyButterflyPanelActive = false;
    }
}
