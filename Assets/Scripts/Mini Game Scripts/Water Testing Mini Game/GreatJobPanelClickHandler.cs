using UnityEngine;
using UnityEngine.EventSystems;

public class GreatJobPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool isGreatJobPanelClicked = false;

    // Method called when clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Great Job panel has been clicked!"); // Debug.Log
        isGreatJobPanelClicked = true; // Set bool isGreatJobPanelClicked to true
        WaterTestingManager.greatJobPanelActive = false; // Set bool greatJobPanelActive to false
    }
}
