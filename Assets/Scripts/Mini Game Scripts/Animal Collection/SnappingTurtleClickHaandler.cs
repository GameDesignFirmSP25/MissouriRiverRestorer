using UnityEngine;
using UnityEngine.EventSystems;

public class SnappingTurtleClickHaandler : MonoBehaviour, IPointerClickHandler
{
    public bool snappingTurtleClicked = false;

    // This method is called when the object is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Snapping Turtle panel is clicked. Hiding panel...");
        snappingTurtleClicked = true;
        AnimalGameManager.dialogueIsActive = false;
        AnimalGameManager.snappingTurtlePanelActive = false;
    }
}

