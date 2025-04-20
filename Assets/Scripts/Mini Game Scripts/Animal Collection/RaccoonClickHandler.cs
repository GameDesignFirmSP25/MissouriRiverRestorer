using UnityEngine;
using UnityEngine.EventSystems;

public class RaccoonClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isRaccoonPanelClicked = false;
    // This method is called when the user clicks on the GameObject this script is attached to
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Raccoon panel clicked. Hiding panel...");
        isRaccoonPanelClicked = true;
        AnimalGameManager.raccoonPanelActive = false;
    }
}
