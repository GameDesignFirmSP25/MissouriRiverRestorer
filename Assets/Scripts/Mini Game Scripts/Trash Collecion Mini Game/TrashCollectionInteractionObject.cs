using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TrashCollectionInteractionObject : MonoBehaviour
{
    [Header("Script References")]
    public InteractionObjectSO interactionObjectSO;
    public TrashCollectionGame trashCollectionGameScript;

    [Header("Player References")]
    public GameObject player;
    public Transform playerPosition;

    [Header("String Variables")]
    public string message = "Player is within interaction distance!"; // Message to display when within interaction distance

    [Header("Static Variables")]
    public static TrashCollectionInteractionObject currentActiveObject = null;

    [Header("Booleans")]
    public bool nearInteractionObject = false; // Flag to check if the player is near the interaction object
    public bool hasBeenInteractedWith = false; // Flag to check if the object has been interacted with

    [Header("UI Elements")]
    public GameObject interactPanel; // Reference to the UI Panel for interaction
    public Image interactPanelImage; // Reference to the UI image for interaction prompt
    public TextMeshProUGUI interactPanelText; // Reference to the UI text for interaction prompt

    [Header("Lists")]
    public static List<TrashCollectionInteractionObject> allInteractionObjects = new List<TrashCollectionInteractionObject>();

    [Header("Materials")]
    private Material trashInteraction;
    private Material animalInteraction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trashCollectionGameScript = FindAnyObjectByType<TrashCollectionGame>();

        player = GameObject.Find("PlayerArmature"); // Find the player GameObject in the scene

        playerPosition = player.GetComponent<Transform>(); // Get the Transform component of the player GameObject

        interactPanel = GameObject.Find("Interact Panel"); // Find the interact panel in the scene

        interactPanelText = GameObject.Find("Interact Panel Text").GetComponent<TextMeshProUGUI>(); // Find the interact panel text in the scene

        interactPanelImage = interactPanel.GetComponent<Image>(); // Find the interact panel image in the scene

        interactPanelImage.enabled = false; // Disable the interact panel image

        interactPanelText.text = ""; // Clear the interact panel text

        Renderer renderer = GetComponentInChildren<Renderer>(); // Get the Renderer component of the interaction object

        if (renderer != null)
        {
            trashInteraction = new Material(renderer.material);
            animalInteraction = new Material(renderer.material); // Create unique instance

            trashInteraction.SetFloat("_OutlineType", 3);
            animalInteraction.SetFloat("_OutlineType", 3);

            if (interactionObjectSO.interactionType == "Bottle" || interactionObjectSO.interactionType == "Trash Bag" || 
                interactionObjectSO.interactionType == "Gas Can" || interactionObjectSO.interactionType == "Styrofoam Cup")
            {
                renderer.material = trashInteraction; // Assign one of them as the active material
            }

            if (interactionObjectSO.interactionType == "Save Raccoon" || interactionObjectSO.interactionType == "Save Deer" ||
                interactionObjectSO.interactionType == "Save Bird" || interactionObjectSO.interactionType == "Save Fish")
            {
                renderer.material = animalInteraction; // Assign one of them as the active material
            }
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} has no Renderer component in children. Material setup skipped.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerDistance();

        if (nearInteractionObject && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void OnEnable()
    {
        allInteractionObjects.Add(this); // Add this interaction object to the list when enabled
    }

    private void OnDisable()
    {
        allInteractionObjects.Remove(this); // Remove this interaction object from the list when disabled
    }

    private void CheckPlayerDistance()
    {
        // Check if the object has already been interacted with
        // If hasBeenInteractedWith  is true...
        if (hasBeenInteractedWith)
        {
            // If the current active object is this object...
            if (currentActiveObject == this)
            {
                HideInteractionUI(); // Hide the interaction UI if the object has been interacted with
                currentActiveObject = null; // Reset the current active object
            }

            nearInteractionObject = false; // Reset the nearInteractionObject flag
            return; // Exit if the object has already been interacted with
        }

        if (currentActiveObject == this)
        {
            ShowInteractionUI(); // Hide the interaction UI if the player is able to interact
        }

        // Check if the player is within the interaction distance
        // If the distance between the player and this interaction object is less than the interaction distance defined in the ScriptableObject...
        if (Vector3.Distance(playerPosition.position, transform.position) < interactionObjectSO.interactionDistance)
        {
            // If the current active object is not this object...
            if (currentActiveObject != this)
            {
                Debug.Log(message); // Log the message indicating the player is within interaction distance

                // Hide UI for previous object
                // If there is a current active object...
                if (currentActiveObject != null)
                {
                    currentActiveObject.HideInteractionUI(); // Hide the interaction UI for the previous object
                    currentActiveObject.nearInteractionObject = false; // Reset the nearInteractionObject flag for the previous object
                }

                // Show UI for this object
                ShowInteractionUI(); // Show the interaction UI for this object
                currentActiveObject = this; // Set this object as the current active object
                nearInteractionObject = true; // Set the nearInteractionObject flag to true
            }
        }
        else
        {
            // If the player is not within the interaction distance...
            if (currentActiveObject == this)
            {
                HideInteractionUI(); // Hide the interaction UI for this object
                currentActiveObject = null; // Reset the current active object
                nearInteractionObject = false; // Reset the nearInteractionObject flag
            }
        }
    }

    // Method to show the interaction UI
    private void ShowInteractionUI()
    {
        interactPanelImage.enabled = true; // Enable the interact panel image

        if (interactionObjectSO.interactionType == "Bottle" || interactionObjectSO.interactionType == "Trash Bag" ||
                interactionObjectSO.interactionType == "Gas Can" || interactionObjectSO.interactionType == "Styrofoam Cup")
        {
            interactPanelText.text = "E to Collect " + interactionObjectSO.interactionType; // Update the button text with the interaction type
            trashInteraction.SetFloat("_HasOutline", 1.0f); // Set the outline for trash interaction objects
        }

        if (interactionObjectSO.interactionType == "Save Raccoon" || interactionObjectSO.interactionType == "Save Deer" ||
                interactionObjectSO.interactionType == "Save Bird" || interactionObjectSO.interactionType == "Save Fish")
        {
            interactPanelText.text = "E to " + interactionObjectSO.interactionType; // Update the button text with the interaction type
            animalInteraction.SetFloat("_HasOutline", 1.0f); // Set the outline for animal interaction objects
        }
    }

    // Method to hide the interaction UI
    private void HideInteractionUI()
    {
        interactPanelImage.enabled = false; // Disable the interact panel image
        interactPanelText.text = ""; // Clear the button text

        if (interactionObjectSO.interactionType == "Bottle" || interactionObjectSO.interactionType == "Trash Bag" ||
                interactionObjectSO.interactionType == "Gas Can" || interactionObjectSO.interactionType == "Styrofoam Cup")
        {
            trashInteraction.SetFloat("_HasOutline", 0.0f); // Set the outline for trash interaction objects
        }

        if (interactionObjectSO.interactionType == "Save Raccoon" || interactionObjectSO.interactionType == "Save Deer" ||
                interactionObjectSO.interactionType == "Save Bird" || interactionObjectSO.interactionType == "Save Fish")
        {
            animalInteraction.SetFloat("_HasOutline", 0.0f); // Set the outline for animal interaction objects
        }
    }

    // Method to handle interaction with the object
    private void Interact()
    {
        Debug.Log("Interacting with " + interactionObjectSO.interactionType); // Debug.Log

        // If the interaction type is "Bottle"
        if (interactionObjectSO.interactionType == "Bottle")
        {
            BottleClicked(); // Call the 

            // Play the interaction sound
            // If the interactSound is not null...
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
            }
        }

        // If the interaction type is "Trash Bag"
        if (interactionObjectSO.interactionType == "Trash Bag")
        {
            TrashBagClicked(); // Call the 

            // Play the interaction sound
            // If the interactSound is not null...
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
            }
        }

        // If the interaction type is "Gas Canister"
        if (interactionObjectSO.interactionType == "Gas Can")
        {
            GasCanisterClicked(); // Call the 

            // Play the interaction sound
            // If the interactSound is not null...
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
            }
        }

        // If the interaction type is "Styrofoam Cup"
        if (interactionObjectSO.interactionType == "Styrofoam Cup")
        {
            StyrofoamCupClicked(); // Call the 

            // Play the interaction sound
            // If the interactSound is not null...
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
            }
        }

        // If the interaction type is "Save Raccoon"
        if (interactionObjectSO.interactionType == "Save Raccoon")
        {
            SaveRaccoonClicked(); // Call the 

            // Play the interaction sound
            // If the interactSound is not null...
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
            }
        }

        // If the interaction type is "Save Deer"
        if (interactionObjectSO.interactionType == "Save Deer")
        {
            SaveDeerClicked(); // Call the 

            // Play the interaction sound
            // If the interactSound is not null...
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
            }
        }

        // If the interaction type is "Save Bird"
        if (interactionObjectSO.interactionType == "Save Bird")
        {
            SaveBirdClicked(); // Call the 

            // Play the interaction sound
            // If the interactSound is not null...
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
            }
        }

        // If the interaction type is "Save Fish"
        if (interactionObjectSO.interactionType == "Save Fish")
        {
            SaveFishClicked(); // Call the 

            // Play the interaction sound
            // If the interactSound is not null...
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
            }
        }
    }

    private void BottleClicked()
    {
        hasBeenInteractedWith = true; // Set the hasBeenInteractedWith flag to true

        // If the current active object is this object...
        if (currentActiveObject == this)
        {
            HideInteractionUI(); // Hide the interaction UI for this object
            currentActiveObject = null; // Reset the current active object
        }

        Debug.Log("Bottle clicked!"); // Debug.Log to indicate the bottle has been clicked
        trashCollectionGameScript.BottleClicked(); // Call the BottleClicked method in the TrashCollectionGame script
    }

    private void TrashBagClicked()
    {
        hasBeenInteractedWith = true; // Set the hasBeenInteractedWith flag to true

        // If the current active object is this object...
        if (currentActiveObject == this)
        {
            HideInteractionUI(); // Hide the interaction UI for this object
            currentActiveObject = null; // Reset the current active object
        }

        Debug.Log("Trash Bag clicked!"); // Debug.Log to indicate the trash bag has been clicked
        trashCollectionGameScript.TrashBagClicked(); // Call the TrashBagClicked method in the TrashCollectionGame script
    }

    private void GasCanisterClicked()
    {
        hasBeenInteractedWith = true; // Set the hasBeenInteractedWith flag to true

        // If the current active object is this object...
        if (currentActiveObject == this)
        {
            HideInteractionUI(); // Hide the interaction UI for this object
            currentActiveObject = null; // Reset the current active object
        }

        Debug.Log("Gas Canister clicked!"); // Debug.Log to indicate the gas canister has been clicked
        trashCollectionGameScript.GasCanisterClicked(); // Call the GasCanisterClicked method in the TrashCollectionGame script
    }

    private void StyrofoamCupClicked()
    {
        hasBeenInteractedWith = true; // Set the hasBeenInteractedWith flag to true

        // If the current active object is this object...
        if (currentActiveObject == this)
        {
            HideInteractionUI(); // Hide the interaction UI for this object
            currentActiveObject = null; // Reset the current active object
        }

        Debug.Log("Styrofoam Cup clicked!"); // Debug.Log to indicate the styrofoam cup has been clicked
        trashCollectionGameScript.StyrofoamCupClicked(); // Call the StyrofoamCupClicked method in the TrashCollectionGame script
    }

    private void SaveRaccoonClicked()
    {
        hasBeenInteractedWith = true; // Set the hasBeenInteractedWith flag to true

        // If the current active object is this object...
        if (currentActiveObject == this)
        {
            HideInteractionUI(); // Hide the interaction UI for this object
            currentActiveObject = null; // Reset the current active object
        }

        Debug.Log("Save Raccoon clicked!"); // Debug.Log to indicate the save raccoon has been clicked
        trashCollectionGameScript.SaveRaccoonClicked(); // Call the SaveRaccoonClicked method in the TrashCollectionGame script
    }

    private void SaveDeerClicked()
    {
        hasBeenInteractedWith = true; // Set the hasBeenInteractedWith flag to true

        // If the current active object is this object...
        if (currentActiveObject == this)
        {
            HideInteractionUI(); // Hide the interaction UI for this object
            currentActiveObject = null; // Reset the current active object
        }

        Debug.Log("Save Deer clicked!"); // Debug.Log to indicate the save deer has been clicked
        trashCollectionGameScript.SaveDeerClicked(); // Call the SaveDeerClicked method in the TrashCollectionGame script
    }

    private void SaveBirdClicked()
    {
        hasBeenInteractedWith = true; // Set the hasBeenInteractedWith flag to true

        // If the current active object is this object...
        if (currentActiveObject == this)
        {
            HideInteractionUI(); // Hide the interaction UI for this object
            currentActiveObject = null; // Reset the current active object
        }

        Debug.Log("Save Bird clicked!"); // Debug.Log to indicate the save bird has been clicked
        trashCollectionGameScript.SaveBirdClicked(); // Call the SaveBirdClicked method in the TrashCollectionGame script
    }

    private void SaveFishClicked()
    {
        hasBeenInteractedWith = true; // Set the hasBeenInteractedWith flag to true

        // If the current active object is this object...
        if (currentActiveObject == this)
        {
            HideInteractionUI(); // Hide the interaction UI for this object
            currentActiveObject = null; // Reset the current active object
        }

        Debug.Log("Save Fish clicked!"); // Debug.Log to indicate the save fish has been clicked
        trashCollectionGameScript.SaveFishClicked(); // Call the SaveFishClicked method in the TrashCollectionGame script
    }
}
