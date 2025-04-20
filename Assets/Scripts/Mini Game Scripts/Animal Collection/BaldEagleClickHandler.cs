using UnityEngine;
using UnityEngine.EventSystems;

public class BaldEagleClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isBaldEaglePanelClicked = false;
    // This method is called when the user clicks on the GameObject this script is attached to
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Bald Eagle panel clicked. Hiding panel...");
        isBaldEaglePanelClicked = true;
        AnimalGameManager.baldEaglePanelActive = false;
    }
}
