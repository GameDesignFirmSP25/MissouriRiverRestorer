using UnityEngine;
using UnityEngine.EventSystems;


public class PaintedLadyButterflyClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isPaintedLadyButterflyPanelClicked = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Painted Lady Butterfly panel clicked. Hiding panel...");
        isPaintedLadyButterflyPanelClicked = true;
        AnimalGameManager.paintedLadyButterflyPanelActive = false;
        AnimalGameManager.dialogueIsActive = false;
    }
}
