using UnityEngine;
using UnityEngine.EventSystems;

public class GasEffectsPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool isEffectsOfGasPanelClicked = false; // Variable to track if the panel has been clicked

    // Method called when clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Effects of gas panel has been clicked!"); // Debug.Log
        isEffectsOfGasPanelClicked = true; // Set bool isEffectsOfGasPanelClicked to true
        WaterTestingManager.effectsOfGasPanelActive = false; // Set bool effectsOfGasPanelActive to false;
    }
}
