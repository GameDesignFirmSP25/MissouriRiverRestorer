using UnityEngine;
using UnityEngine.EventSystems;

public class EasternStarlingClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool easternStarlingClicked = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Eastern Starling panel clicked. Hiding panel...");
        easternStarlingClicked = true;
        AnimalGameManager.dialogueIsActive = false;
        AnimalGameManager.easternStarlingPanelActive = false;
    }
}
