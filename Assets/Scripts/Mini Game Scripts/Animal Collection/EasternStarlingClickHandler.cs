using UnityEngine;
using UnityEngine.EventSystems;

public class EasternStarlingClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isEasternStarlingPanelClicked = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Eastern Starling panel clicked. Hiding panel...");
        isEasternStarlingPanelClicked = true;
        AnimalGameManager.easternStarlingPanelActive = false;
    }
}
