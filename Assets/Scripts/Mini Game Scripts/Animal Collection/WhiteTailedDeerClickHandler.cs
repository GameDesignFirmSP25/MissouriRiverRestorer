using UnityEngine;
using UnityEngine.EventSystems;

public class WhiteTailedDeerClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isWhiteTailedDeerPanelClicked = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("White Tailed Deer panel clicked. Hiding panel...");
        isWhiteTailedDeerPanelClicked = true;
        AnimalGameManager.whiteTailedDeerPanelActive = false;
        AnimalGameManager.dialogueIsActive = false;
    }
}
