using StarterAssets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class IncorrectPlantSwappedPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public AnimalGameManager animalGameManager;

    public StarterAssetsInputs playerInput;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Incorrect plant swapped panel clicked. Hiding panel...");
        playerInput.controlsLocked = false; // Unlock player controls when the panel is clicked
        //animalGameManager.PlayIncorrectPlantSwappedSound();
    }
}
