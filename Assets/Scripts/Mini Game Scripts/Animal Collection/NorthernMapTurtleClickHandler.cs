using UnityEngine;
using UnityEngine.EventSystems;

public class NorthernMapTurtleClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isNorthernMapTurtlePanelClicked = false;

    // This method is called when the user clicks on the GameObject this script is attached to
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Northern Map Turtle panel clicked. Hiding panel...");
        isNorthernMapTurtlePanelClicked = true;
        AnimalGameManager.northernMapTurtlePanelActive = false;
        AnimalGameManager.dialogueIsActive = false;
    }
    
}
