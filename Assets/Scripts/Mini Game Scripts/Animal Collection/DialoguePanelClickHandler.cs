using UnityEngine;
using UnityEngine.EventSystems;

public class DialoguePanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool isDialoguePanelClicked = false; // Variable to track if the dialogue panel has been clicked
   
    // Method called when the panel is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Dialogue panel has been clicked!"); // Log message for debugging
        isDialoguePanelClicked = true; // Set the boolean to true
    }
}

