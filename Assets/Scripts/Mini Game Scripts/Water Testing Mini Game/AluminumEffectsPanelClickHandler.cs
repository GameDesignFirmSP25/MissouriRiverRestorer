using UnityEngine;
using UnityEngine.EventSystems;

public class AluminumEffectsPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool isEffectsOfAluminumPanelClicked = false; // Variable to track if the panel has been clicked

    // Method called when clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Effects of aluminum panel has been clicked!"); // Debug.Log
        isEffectsOfAluminumPanelClicked = true; // Set bool isEffectsOfAluminumPanelClicked to true
        WaterTestingManager.effectsOfAluminumPanelActive = false; // Set bool effectsOfAluminumPanelActive to false;
    }
}
