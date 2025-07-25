using UnityEngine;
using UnityEngine.EventSystems;

public class TireEffectsPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool isEffectsOfTirePanelClicked = false; // Variable to track if the panel has been clicked

    // Method called when clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Effects of tire panel has been clicked!"); // Debug.Log
        isEffectsOfTirePanelClicked = true; // Set bool isEffectsOfTirePanelClicked to true
        WaterTestingManager.effectsOfTirePanelActive = false; // Set bool effectsOfTirePanelActive to false;
    }
}
