using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class CorrectPlantSwappedPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public AnimalGameManager animalGameManager;

    public StarterAssetsInputs playerInput;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Correct plant swapped panel clicked. Hiding panel...");
        playerInput.controlsLocked = false; // Unlock player controls when the panel is clicked+
        //animalGameManager.PlayCorrectPlantSwappedSound();
    }
}

