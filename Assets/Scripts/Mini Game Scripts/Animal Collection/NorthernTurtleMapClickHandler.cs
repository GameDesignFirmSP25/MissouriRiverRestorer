using UnityEngine;
using UnityEngine.EventSystems;

public class NorthernMapTurtleClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool northernMapTurtleClicked = false;

    // This method is called when the user clicks on the GameObject this script is attached to
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Northern Map Turtle panel clicked. Hiding panel...");
        northernMapTurtleClicked = true;
        AnimalGameManager.dialogueIsActive = false;
        AnimalGameManager.northernMapTurtlePanelActive = false;
    }
    
}
