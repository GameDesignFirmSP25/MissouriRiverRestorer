using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class InteractionObject : MonoBehaviour
{
    [Header("Script References")]
    public InteractionObjectSO interactionObjectSO; // Reference to the ScriptableObject containing interaction data
    public WaterTestingManager waterTestingManagerScript; // Reference to the WaterTestingManager script

    [Header("Player References")]
    public GameObject player;
    public Transform playerPosition; // Reference to the player GameObject

    [Header("String Variables")]
    public string message = "Player is within interaction distance!"; // Message to display when within interaction distance
    public string objectID;

    [Header("Static Variables")]
    public static bool surfaceWaveClicked = false; // Static variable to track if the surface wave has been clicked
    public static bool isAnyInteractionActive = false; // Static variable to track if any interaction is currently active
    public static bool requireERelease = false; // Static variable to track if the player needs to release the "E" key after interaction
    public static InteractionObject clickedSurfaceWave; // Static variable to track the clicked surface wave object
    public static InteractionObject currentActiveObject = null; // Static variable to track the current active object

    [Header("Booleans")]
    public bool nearInteractionObject = false; // Variable to track if the player is near an interaction object
    public bool hasBeenInteractedWith = false; // Variable to track if the object has been interacted with
    public bool ableToInteractWith = false;

    [Header("UI Elements")]
    public GameObject interactPanel; // Reference to the UI Panel for interaction
    public Image interactPanelImage; // Reference to the UI image for interaction prompt
    public TextMeshProUGUI interactPanelText; // Reference to the UI text for interaction prompt

    [Header("Lists")]
    public static List<InteractionObject> allInteractionObjects = new List<InteractionObject>(); // List to store all interaction objects in the scene

    [Header("Materials")]
    private Material trashInteraction;
    private Material animalInteraction;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waterTestingManagerScript = FindAnyObjectByType<WaterTestingManager>(); // Find the WaterTestingManager script in the scene

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

            trashInteraction.SetFloat("_OutlineType", 1);
            animalInteraction.SetFloat("_OutlineType", 3);

            if (interactionObjectSO.interactionType == "Trash Bag" || interactionObjectSO.interactionType == "Tire" || interactionObjectSO.interactionType == "Gas Canister" ||
                interactionObjectSO.interactionType == "Aluminum Can")
            {
                renderer.material = trashInteraction; // Assign one of them as the active material
            }

            if (interactionObjectSO.interactionType == "Asian Carp" || interactionObjectSO.interactionType == "Catfish" || interactionObjectSO.interactionType == "Pallid Sturgeon" || 
                interactionObjectSO.interactionType == "Beaver" || interactionObjectSO.interactionType == "Buck" || interactionObjectSO.interactionType == "Deer" || 
                interactionObjectSO.interactionType == "Raccoon")
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
        CheckPlayerDistance(); // Check if the player is within interaction distance

        if (requireERelease)
        {
            if (!Input.GetKey(KeyCode.E))
            {
                requireERelease = false; // E was released, allow new interaction next frame
                isAnyInteractionActive = false; // Reset the interaction state
            }
                
            return;
        }

        // Handle interaction when the player presses "E"
        // If the player is near an interaction object, the "E" key is pressed, and no panel is active
        if (nearInteractionObject && Input.GetKeyDown(KeyCode.E) && !WaterTestingManager.aPanelIsActive)
        {
            if (isAnyInteractionActive)
            {
                Debug.Log("Interaction already active with another object."); // Debug.Log
                return; // Exit if any interaction is already active
            }

            Interact(); // Call the Interact method to handle interaction with the object
            requireERelease = true; // Set the requireERelease flag to true to prevent immediate re-interaction
        }
    }

    // Unity event methods to manage the interaction object lifecycle
    private void OnEnable()
    {
        allInteractionObjects.Add(this); // Add this interaction object to the list of all interaction objects when enabled
    }

    // Unity event method called when the object is disabled
    private void OnDisable()
    {
        allInteractionObjects.Remove(this); // Remove this interaction object from the list of all interaction objects when disabled
    }

    // Method to check if the player is within interaction distance
    private void CheckPlayerDistance()
    {
        // If the interactionObjectSO or playerPosition is null, exit the method
        if (interactionObjectSO == null || playerPosition == null)
        {
            return;
        }

        // If the player is not able to interact with the object...
        if (!ableToInteractWith)
        {
            if (currentActiveObject == this)
            {
                HideInteractionUI(); // Hide the interaction UI if the player is not able to interact
                currentActiveObject = null; // Reset the current active object
            }

            nearInteractionObject = false; // Reset the nearInteractionObject flag
            return; // Exit the method if not able to interact
        }
        else if (ableToInteractWith)
        {
            if (currentActiveObject == this)
            {
                ShowInteractionUI(); // Hide the interaction UI if the player is able to interact
            }
        }

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
        interactPanelText.text = "E to Interact with " + interactionObjectSO.interactionType; // Update the button text with the interaction type

        if (interactionObjectSO.interactionType == "Trash Bag" || interactionObjectSO.interactionType == "Tire" || interactionObjectSO.interactionType == "Gas Canister" ||
                interactionObjectSO.interactionType == "Aluminum Can")
        {
            trashInteraction.SetFloat("_HasOutline", 1.0f); // Set the outline for trash interaction objects
        }

        if (interactionObjectSO.interactionType == "Asian Carp" || interactionObjectSO.interactionType == "Catfish" || interactionObjectSO.interactionType == "Pallid Sturgeon" ||
            interactionObjectSO.interactionType == "Beaver" || interactionObjectSO.interactionType == "Buck" || interactionObjectSO.interactionType == "Deer" ||
            interactionObjectSO.interactionType == "Raccoon")
        {
            animalInteraction.SetFloat("_HasOutline", 1.0f); // Set the outline for animal interaction objects
        }
    }

    // Method to hide the interaction UI
    private void HideInteractionUI()
    {
        interactPanelImage.enabled = false; // Disable the interact panel image
        interactPanelText.text = ""; // Clear the button text

        if (interactionObjectSO.interactionType == "Trash Bag" || interactionObjectSO.interactionType == "Tire" || interactionObjectSO.interactionType == "Gas Canister" ||
                interactionObjectSO.interactionType == "Aluminum Can")
        {
            trashInteraction.SetFloat("_HasOutline", 0.0f); // Set the outline for trash interaction objects
        }

        if (interactionObjectSO.interactionType == "Asian Carp" || interactionObjectSO.interactionType == "Catfish" || interactionObjectSO.interactionType == "Pallid Sturgeon" ||
            interactionObjectSO.interactionType == "Beaver" || interactionObjectSO.interactionType == "Buck" || interactionObjectSO.interactionType == "Deer" ||
            interactionObjectSO.interactionType == "Raccoon")
        {
            animalInteraction.SetFloat("_HasOutline", 0.0f); // Set the outline for animal interaction objects
        }
    }

    public void SetAbleToInteractFlag()
    {
        ableToInteractWith = true; // Set the ableToInteractWith flag to true
        //Debug.Log("Able to interact with " + interactionObjectSO.interactionType); // Debug.Log
    }

    // Method to handle interaction with the object
    private void Interact()
    {
        if (!isAnyInteractionActive)
        {
            isAnyInteractionActive = true; // Set the static variable IsAnyInteractionActive to true

            Debug.Log("Interacting with " + interactionObjectSO.interactionType); // Debug.Log

            // If the interaction type is "Surface Wave" & bool objectivesComplete is true...
            if (interactionObjectSO.interactionType == "Surface Wave" && waterTestingManagerScript.objectivesComplete)
            {
                SurfaceWaveClicked(); // Call the SurfaceWaveInteraction method

                // Play the interaction sound
                // If the interactSound is not null...
                if (interactionObjectSO.interactSound != null)
                {
                    interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
                }
            }

            // If isFirstWaterTestComplete is false...
            else if (!WaterTestingManager.isFirstWaterTestComplete)
            {
                // If the interaction type is "Aluminum Can" tag & bool isAluminumCanObjectiveComplete is false
                if (interactionObjectSO.interactionType == "Aluminum Can" && !WaterTestingManager.isAluminumCanObjectiveComplete)
                {
                    AluminumCanClicked(); // Call the AluminumCanClicked method

                    // Play the interaction sound
                    // If the interactSound is not null...
                    if (interactionObjectSO.interactSound != null)
                    {
                        interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
                    }
                }

                // If the interaction type is "Tire" tag & bool isTireObjectiveComplete is false
                if (interactionObjectSO.interactionType == "Tire" && !WaterTestingManager.isTireObjectiveComplete)
                {
                    TireClicked(); // Call the TireClicked method

                    // Play the interaction sound
                    // If the interactSound is not null...
                    if (interactionObjectSO.interactSound != null)
                    {
                        interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
                    }
                }

                // If the interaction type is "Gas Can" tag & bool isGasCanObjectiveComplete is false
                if (interactionObjectSO.interactionType == "Gas Canister" && !WaterTestingManager.isGasCanObjectiveComplete)
                {
                    GasCanisterClicked(); // Call the GasCanisterClicked method

                    // Play the interaction sound
                    // If the interactSound is not null...
                    if (interactionObjectSO.interactSound != null)
                    {
                        interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
                    }
                }

                // If the interaction type is "Trash Bag" tag & bool isTrashBagObjective is false
                if (interactionObjectSO.interactionType == "Trash Bag" && !WaterTestingManager.isTrashBagObjectiveComplete)
                {
                    TrashBagClicked(); // Call the TrashBagClicked method

                    // Play the interaction sound
                    // If the interactSound is not null...
                    if (interactionObjectSO.interactSound != null)
                    {
                        interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
                    }
                }
            }

            // If bool isFirstWaterTestComplete is true & isSecondWaterTestComplete is false...
            else if (WaterTestingManager.isFirstWaterTestComplete && !WaterTestingManager.isSecondWaterTestComplete)
            {
                // If bool isFishObjectiveComplete is false...
                if (!WaterTestingManager.isFishObjectiveComplete)
                {
                    // If the interaction type is "Fish: Asian Carp", "Fish: Catfish", or "Fish: Pallid Sturgeon"...
                    if (interactionObjectSO.interactionType == "Asian Carp" || interactionObjectSO.interactionType == "Catfish" || interactionObjectSO.interactionType == "Pallid Sturgeon")
                    {
                        FishClicked(); // Call the FishClicked method

                        // Play the interaction sound
                        // If the interactSound is not null...
                        if (interactionObjectSO.interactSound != null)
                        {
                            interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
                        }
                    }
                }

                // / If bool isMammalObjectiveComplete is false...
                if (!WaterTestingManager.isMammalObjectiveComplete)
                {
                    // If the interaction type is "Mammal: Beaver", "Mammal: Buck", "Mammal: Deer", or "Mammal: Raccoon"...
                    if (interactionObjectSO.interactionType == "Beaver" || interactionObjectSO.interactionType == "Buck" || interactionObjectSO.interactionType == "Deer" || interactionObjectSO.interactionType == "Raccoon")
                    {
                        MammalClicked(); // Call the MammalClicked method

                        // Play the interaction sound
                        // If the interactSound is not null...
                        if (interactionObjectSO.interactSound != null)
                        {
                            interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
                        }
                    }
                }
            }
        } 
    }

    // Method to handle surface wave interaction
    private void SurfaceWaveClicked()
    {
        Debug.Log("Surface Wave clicked: " + gameObject.name); // Debug.Log
        surfaceWaveClicked = true; // Set the static variable surfaceWaveClicked to true
        clickedSurfaceWave = this; // Set the static variable clickedSurfaceWave to this instance
    }

    // Fix for the invalid foreach statement in the AluminumCanClicked method
    private void AluminumCanClicked()
    {
        Debug.Log("Aluminum can clicked"); // Debug.Log

        // If bool aPanelIsActive is false...
        if (!WaterTestingManager.aPanelIsActive)
        {
            WaterTestingManager.effectsOfAluminumPanelActive = true; // Set bool effectsOfTrashPanelActive to true
            WaterTestingManager.isAluminumCanObjectiveComplete = true; // Set bool isTrashBagObjectiveComplete to true
            waterTestingManagerScript.HandleAluminumCanInteraction(); // Call the HandleAluminumCanInteraction method in WaterTestingManager

            // Mark all aluminum cans as interacted
            // Iterate through all interaction objects
            foreach (var obj in allInteractionObjects)
            {
                // If the object is not null and its interaction type is "Aluminum Can"...
                if (obj != null && obj.interactionObjectSO.interactionType == "Aluminum Can")
                {
                    obj.hasBeenInteractedWith = true; // Mark the object as interacted with

                    // If the current active object is this object...
                    if (currentActiveObject == obj)
                    {
                        obj.HideInteractionUI(); // Hide the interaction UI for this object
                        currentActiveObject = null; // Reset the current active object
                    }
                }
            }
        }
    }

    // Method to handle tire interaction
    private void TireClicked()
    {
        Debug.Log("Tire clicked"); // Debug.Log

        // If bool aPanelIsActive is false...
        if (!WaterTestingManager.aPanelIsActive)
        {
            WaterTestingManager.effectsOfTirePanelActive = true; // Set bool effectsOfTrashPanelActive to true
            WaterTestingManager.isTireObjectiveComplete = true; // Set bool isTrashBagObjectiveComplete to true
            waterTestingManagerScript.HandleTireInteraction(); // Call the HandleTireInteraction method in WaterTestingManager

            // Mark all tires as interacted
            // Iterate through all interaction objects
            foreach (var obj in allInteractionObjects)
            {
                // If the object is not null and its interaction type is "Tire"...
                if (obj != null && obj.interactionObjectSO.interactionType == "Tire")
                {
                    obj.hasBeenInteractedWith = true; // Mark the object as interacted with

                    // If the current active object is this object...
                    if (currentActiveObject == obj)
                    {
                        obj.HideInteractionUI(); // Hide the interaction UI for this object
                        currentActiveObject = null; // Reset the current active object
                    }
                }
            }
        }
    }

    // Method to handle gas canister interaction
    private void GasCanisterClicked()
    {
        Debug.Log("Gas canister clicked"); // Debug.Log

        // If bool aPanelIsActive is false...
        if (!WaterTestingManager.aPanelIsActive)
        {
            WaterTestingManager.effectsOfGasPanelActive = true; // Set bool effectsOfTrashPanelActive to true
            WaterTestingManager.isGasCanObjectiveComplete = true; // Set bool isTrashBagObjectiveComplete to true
            waterTestingManagerScript.HandleGasCanisterInteraction(); // Call the HandleGasCanisterInteraction method in WaterTestingManager

            // Mark all gas cans as interacted
            // Iterate through all interaction objects
            foreach (var obj in allInteractionObjects)
            {
                // If the object is not null and its interaction type is "Gas Canister"...
                if (obj != null && obj.interactionObjectSO.interactionType == "Gas Canister")
                {
                    obj.hasBeenInteractedWith = true; // Mark the object as interacted with

                    // If the current active object is this object...
                    if (currentActiveObject == obj)
                    {
                        obj.HideInteractionUI(); // Hide the interaction UI for this object
                        currentActiveObject = null; // Reset the current active object
                    }
                }
            }
        }
    }

    // Method to handle trash bag interaction
    private void TrashBagClicked()
    {
        Debug.Log("Trash bag clicked"); // Debug.Log

        // If bool aPanelIsActive is false...
        if (!WaterTestingManager.aPanelIsActive)
        {
            WaterTestingManager.effectsOfTrashPanelActive = true; // Set bool effectsOfTrashPanelActive to true
            WaterTestingManager.isTrashBagObjectiveComplete = true; // Set bool isTrashBagObjectiveComplete to true
            waterTestingManagerScript.HandleTrashBagInteraction(); // Call the HandleTrashBagInteraction method in WaterTestingManager

            // Mark all trash bags as interacted
            // Iterate through all interaction objects
            foreach (var obj in allInteractionObjects)
            {
                // If the object is not null and its interaction type is "Trash Bag"...
                if (obj != null && obj.interactionObjectSO.interactionType == "Trash Bag")
                {
                    obj.hasBeenInteractedWith = true; // Mark the object as interacted with

                    // If the current active object is this object...
                    if (currentActiveObject == obj)
                    {
                        obj.HideInteractionUI(); // Hide the interaction UI for this object
                        currentActiveObject = null; // Reset the current active object
                    }
                }
            }
        }
    }

    // Method to handle fish interaction
    private void FishClicked()
    {
        Debug.Log("Fish clicked"); // Debug.Log

        // If bool aPanelIsActive is false...
        if (!WaterTestingManager.aPanelIsActive)
        {
            WaterTestingManager.effectsOfBiodiversity1PanelActive = true; // Set bool effectsOfFishPanelActive to true
            WaterTestingManager.isFishObjectiveComplete = true; // Set bool isFishObjectiveComplete to true

            // Mark all fish as interacted
            // Iterate through all interaction objects
            foreach (var obj in allInteractionObjects)
            {
                // If the object is not null and its interaction type is "Fish: Asian Carp", "Fish: Catfish", or "Fish: Pallid Sturgeon"...
                if (obj != null && (obj.interactionObjectSO.interactionType == "Asian Carp" || obj.interactionObjectSO.interactionType == "Catfish" || obj.interactionObjectSO.interactionType == "Pallid Sturgeon"))
                {
                    obj.hasBeenInteractedWith = true; // Mark the object as interacted with

                    // If the current active object is this object...
                    if (currentActiveObject == obj)
                    {
                        obj.HideInteractionUI(); // Hide the interaction UI for this object
                        currentActiveObject = null; // Reset the current active object
                    }
                }
            }
        }
    }

    // Method to handle mammal interaction
    private void MammalClicked()
    {
        Debug.Log("Mammal clicked"); // Debug.Log

        // If bool aPanelIsActive is false...
        if (!WaterTestingManager.aPanelIsActive)
        {
            WaterTestingManager.effectsOfBiodiversity3PanelActive = true; // Set bool effectsOfFishPanelActive to true
            WaterTestingManager.isMammalObjectiveComplete = true; // Set bool isFishObjectiveComplete to true

            // Mark all mammals as interacted
            // Iterate through all interaction objects
            foreach (var obj in allInteractionObjects)
            {
                // If the object is not null and its interaction type is "Mammal: Beaver", "Mammal: Buck", "Mammal: Deer", or "Mammal: Raccoon"...
                if (obj != null && (obj.interactionObjectSO.interactionType == "Beaver" || obj.interactionObjectSO.interactionType == "Buck" || obj.interactionObjectSO.interactionType == "Deer" || obj.interactionObjectSO.interactionType == "Raccoon"))
                {
                    obj.hasBeenInteractedWith = true; // Mark the object as interacted with

                    // If the current active object is this object...
                    if (currentActiveObject == obj)
                    {
                        obj.HideInteractionUI(); // Hide the interaction UI for this object
                        currentActiveObject = null; // Reset the current active object
                    }
                }
            }
        }
    }

    // Method to reset static variables
    public static void ResetStaticVariables()
    {
        surfaceWaveClicked = false; // Reset the static variable surfaceWaveClicked to false
        clickedSurfaceWave = null; // Reset the static variable clickedSurfaceWave to null
        Debug.Log("InteractionObject static variables reset."); // Debug.Log
    }

    public void TrashObjectiveObjectsReady()
    {
        foreach (var obj in allInteractionObjects)
        {
            if (obj.objectID == "Trash Bag" || obj.objectID == "Tire" || obj.objectID == "Gas Canister" || obj.objectID == "Aluminum Can")
            {
                obj.ableToInteractWith = true; // Set the ableToInteractWith flag for trash-related objects
            }
        }
    }

    public void TrashObjectiveObjectsNotReady()
    {
        foreach (var obj in allInteractionObjects)
        {
            if (obj.objectID == "Trash Bag" || obj.objectID == "Tire" || obj.objectID == "Gas Canister" || obj.objectID == "Aluminum Can")
            {
                obj.ableToInteractWith = false; // Reset the ableToInteractWith flag for trash-related objects
            }
        }
    }

    public void AnimalObjectiveObjectsReady()
    {
        foreach (var obj in allInteractionObjects)
        {
            if (obj.objectID == "Asian Carp" || obj.objectID == "Catfish" || obj.objectID == "Pallid Sturgeon" ||
                obj.objectID == "Beaver" || obj.objectID == "Buck" || obj.objectID == "Deer" || obj.objectID == "Raccoon")
            {
                obj.ableToInteractWith = true; // Set the ableToInteractWith flag for animal-related objects
            }
        }
    }

    public void AnimalObjectiveObjectsNotReady()
    {
        foreach (var obj in allInteractionObjects)
        {
            if (obj.objectID == "Asian Carp" || obj.objectID == "Catfish" || obj.objectID == "Pallid Sturgeon" ||
                obj.objectID == "Beaver" || obj.objectID == "Buck" || obj.objectID == "Deer" || obj.objectID == "Raccoon")
            {
                obj.ableToInteractWith = false; // Reset the ableToInteractWith flag for animal-related objects
            }
        }
    }

    public void WaterTestObjectiveObjectsReady()
    {
        foreach (var obj in allInteractionObjects)
        {
            if (obj.objectID == "Surface Wave")
            {
                obj.ableToInteractWith = true; // Set the ableToInteractWith flag for surface wave objects
            }
        }
    }

    public void WaterTestObjectiveObjectsNotReady()
    {
        foreach (var obj in allInteractionObjects)
        {
            if (obj.objectID == "Surface Wave")
            {
                obj.ableToInteractWith = false; // Reset the ableToInteractWith flag for surface wave objects
            }
        }
    }
}
