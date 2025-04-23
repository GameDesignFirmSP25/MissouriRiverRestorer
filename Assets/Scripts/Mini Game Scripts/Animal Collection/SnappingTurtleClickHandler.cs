using UnityEngine;
using UnityEngine.EventSystems;

public class SnappingTurtleClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isSnappingTurtlePanelClicked = false;

    // This method is called when the object is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Snapping Turtle panel is clicked. Hiding panel...");
        isSnappingTurtlePanelClicked = true;
        AnimalGameManager.snappingTurtlePanelActive = false;
        AnimalGameManager.dialogueIsActive = false;
    }
}

