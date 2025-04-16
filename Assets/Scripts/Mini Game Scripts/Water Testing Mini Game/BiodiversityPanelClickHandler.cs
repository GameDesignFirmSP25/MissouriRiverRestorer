using UnityEngine;
using UnityEngine.EventSystems;

public class BiodiversityPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool isBiodiversityPanelClicked = false;

    // Method called when clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Biodiversity Panel Clicked!"); // Debug.Log
        isBiodiversityPanelClicked = true; // Set bool isbiodiversityPanelClicked to true
        WaterTestingManager.biodiversityPanelActive = false; // Set bool biodiversityPanelActive to false
        WaterTestingManager.aPanelIsActive = false; // Set bool aPanelActive to false;
    }
}
