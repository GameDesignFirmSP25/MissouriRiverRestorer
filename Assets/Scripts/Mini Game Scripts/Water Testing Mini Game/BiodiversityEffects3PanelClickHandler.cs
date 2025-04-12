using UnityEngine;
using UnityEngine.EventSystems;

public class BiodiversityEffects3PanelClickHandler : MonoBehaviour
{
    public bool isEffectsOfBiodiversity3PanelClicked = false;

    // Method called when clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Effects of biodiversity 3 has been clicked!"); // Debug.Log
        isEffectsOfBiodiversity3PanelClicked = true; // Set bool isEffectsOfAluminumPanelClicked to true
        WaterTestingManager.effectsOfBiodiversity3PanelActive = false; // Set bool effectsOfAluminumPanelActive to false;
    }
}
