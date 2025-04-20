using UnityEngine;
using UnityEngine.EventSystems;

public class WhiteTTailedDeerClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool whiteTailedDeerClicked = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("White Tailed Deer panel clicked. Hiding panel...");
        whiteTailedDeerClicked = true;
        AnimalGameManager.dialogueIsActive = false;
        AnimalGameManager.whiteTailedDeerPanelActive = false;
    }
}
