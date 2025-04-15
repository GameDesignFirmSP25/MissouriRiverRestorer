using UnityEngine;
using UnityEngine.EventSystems;

public class BiodiversityEffects1PanelClickHandler : MonoBehaviour
{
    public bool isEffectsOfBiodiversity1PanelClicked = false; // Bool to check if the panel is clicked

    // Method called when clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Effects of biodiversity 1 has been clicked!"); // Debug.Log
        isEffectsOfBiodiversity1PanelClicked = true; // Set bool isEffectsOfAluminumPanelClicked to true
        WaterTestingManager.effectsOfBiodiversity1PanelActive = false; // Set bool effectsOfAluminumPanelActive to false;
    }
}
