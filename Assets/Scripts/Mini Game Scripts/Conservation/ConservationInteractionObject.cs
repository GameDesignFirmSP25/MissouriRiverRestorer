using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConservationInteractionObject : MonoBehaviour
{
    [Header("Script References")]
    public InteractionObjectSO interactionObjectSO; // Reference to the ScriptableObject containing interaction data
    public AnimalGameManager animalGameManagerScript; // Reference to the Animal Game Manager script
    public ChangeablePlant changeablePlantScript; // Reference to the ChangeablePlant script

    [Header("Player References")]
    public GameObject player;
    public Transform playerPosition; // Reference to the player GameObject

    [Header("String Variables")]
    public string message = "Player is within interaction distance!"; // Message to display when within interaction distance
    public string objectID; // Unique identifier for the interaction object

    [Header("Static Variables")]
    public static ConservationInteractionObject currentActiveObject = null; // Static variable to track the current active object
    public static bool easternStarlingClicked = false;
    public static bool whiteTailedDeerClicked = false;
    public static bool bandedPennantDragonflyClicked = false;
    public static bool garterSnakeClicked = false;
    public static bool baldEagleClicked = false;
    public static bool paintedLadyButterflyClicked = false;
    public static bool asianCarpClicked = false;
    public static bool beaverClicked = false;
    public static bool raccoonClicked = false;
    public static bool muskratClicked = false;
    public static bool snappingTurtleClicked = false;
    public static bool northernMapTurtleClicked = false;
    public static bool wasEasternStarlingPreviouslyClicked = false;
    public static bool wasWhiteTailedDeerPreviouslyClicked = false;
    public static bool wasBandedPennantDragonflyPreviouslyClicked = false;
    public static bool wasGarterSnakePreviouslyClicked = false;
    public static bool wasBaldEaglePreviouslyClicked = false;
    public static bool wasPaintedLadyButterflyPreviouslyClicked = false;
    public static bool wasAsianCarpPreviouslyClicked = false;
    public static bool wasBeaverPreviouslyClicked = false;
    public static bool wasRaccoonPreviouslyClicked = false;
    public static bool wasMuskratPreviouslyClicked = false;
    public static bool wasSnappingTurtlePreviouslyClicked = false;
    public static bool wasNorthernMapTurtlePreviouslyClicked = false;

    [Header("Booleans")]
    public bool nearInteractionObject = false; // Variable to track if the player is near an interaction object
    public bool hasBeenInteractedWith = false; // Variable to track if the object has been interacted with
    public bool ableToInteractWith = false; // Static variable to track if the player is able to interact with the object

    [Header("UI Elements")]
    public GameObject interactionPanel; // Reference to the UI Panel for interaction
    public Image interactionPanelImage; // Reference to the UI image for interaction prompt
    public TextMeshProUGUI interactionPanelText; // Reference to the UI text for interaction prompt

    [Header("Lists")]
    public static List<ConservationInteractionObject> allConservationInteractionObjects = new List<ConservationInteractionObject>(); // List to store all interaction objects in the scene

    [Header("Materials")]
    private Material animalInteraction;
    private Material plantInteraction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animalGameManagerScript = FindAnyObjectByType<AnimalGameManager>(); // Find the Animal Game Manager script in the scene

        changeablePlantScript = FindAnyObjectByType<ChangeablePlant>(); // Find the ChangeablePlant script in the scene

        player = GameObject.Find("PlayerArmature"); // Find the player GameObject in the scene

        playerPosition = player.GetComponent<Transform>(); // Get the Transform component of the player

        interactionPanel = GameObject.Find("Interaction Panel"); // Find the interaction panel in the scene

        interactionPanelText = GameObject.Find("Interaction Panel Text").GetComponent<TextMeshProUGUI>(); // Find the interaction panel text in the scene

        interactionPanelImage = interactionPanel.GetComponent<Image>(); // Find the interaction panel image in the scene

        interactionPanelImage.enabled = false; // Disable the interaction panel image

        interactionPanelText.text = ""; // Clear the interaction panel text

        Renderer renderer = GetComponentInChildren<Renderer>(); // Get the Renderer component of the interaction object

        if (renderer != null)
        {
            animalInteraction = new Material(renderer.material); // Create unique instance
            plantInteraction = new Material(renderer.material);

            animalInteraction.SetFloat("_OutlineType", 3);
            plantInteraction.SetFloat("_OutlineType", 4);
            
            if (interactionObjectSO.interactionType == "Asian Carp" || interactionObjectSO.interactionType == "Beaver" ||
                    interactionObjectSO.interactionType == "Common Garter Snake" || interactionObjectSO.interactionType == "Northern Map Turtle" ||
                    interactionObjectSO.interactionType == "Snapping Turtle" || interactionObjectSO.interactionType == "Banded Pennant Dragonfly" ||
                    interactionObjectSO.interactionType == "European Starling" || interactionObjectSO.interactionType == "Muskrat" ||
                    interactionObjectSO.interactionType == "Bald Eagle" || interactionObjectSO.interactionType == "Painted Lady Butterfly" ||
                    interactionObjectSO.interactionType == "Raccoon" || interactionObjectSO.interactionType == "White-Tailed Deer")
            {
                renderer.material = animalInteraction; // Assign one of them as the active material
            }

            if (interactionObjectSO.interactionType == "Bradford Pear" || interactionObjectSO.interactionType == "Purple Loosestrife")
            {
                renderer.material = plantInteraction; // Assign one of them as the active material
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

        // Handle interaction when the player presses "E"
        // If the player is near an interaction object, the "E" key is pressed, and no dialogue is active
        if (nearInteractionObject && Input.GetKeyDown(KeyCode.E))
        {
            Interact(); // Call the Interact method to handle interaction with the object
        }
    }

    // Unity event methods to manage the interaction object lifecycle
    private void OnEnable()
    {
        allConservationInteractionObjects.Add(this); // Add this interaction object to the list of all interaction objects when enabled
    }

    // Unity event method called when the object is disabled
    private void OnDisable()
    {
        allConservationInteractionObjects.Remove(this); // Remove this interaction object from the list of all interaction objects when disabled
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
        interactionPanelImage.enabled = true; // Enable the interact panel image
        interactionPanelText.text = "E to Interact with " + interactionObjectSO.interactionType; // Update the button text with the interaction type

        if (interactionObjectSO.interactionType == "Asian Carp" || interactionObjectSO.interactionType == "Beaver" ||
                    interactionObjectSO.interactionType == "Common Garter Snake" || interactionObjectSO.interactionType == "Northern Map Turtle" ||
                    interactionObjectSO.interactionType == "Snapping Turtle" || interactionObjectSO.interactionType == "Banded Pennant Dragonfly" ||
                    interactionObjectSO.interactionType == "European Starling" || interactionObjectSO.interactionType == "Muskrat" ||
                    interactionObjectSO.interactionType == "Bald Eagle" || interactionObjectSO.interactionType == "Painted Lady Butterfly" ||
                    interactionObjectSO.interactionType == "Raccoon" || interactionObjectSO.interactionType == "White-Tailed Deer")
        {
            animalInteraction.SetFloat("_HasOutline", 1.0f); // Enable the outline for animal interaction objects
        }

        if (interactionObjectSO.interactionType == "Bradford Pear" || interactionObjectSO.interactionType == "Purple Loosestrife")
        {
            plantInteraction.SetFloat("_HasOutline", 1.0f); // Enable the outline for plant interaction objects
        }
    }

    // Method to hide the interaction UI
    private void HideInteractionUI()
    {
        interactionPanelImage.enabled = false; // Disable the interact panel image
        interactionPanelText.text = ""; // Clear the button text

        if (interactionObjectSO.interactionType == "Asian Carp" || interactionObjectSO.interactionType == "Beaver" ||
                    interactionObjectSO.interactionType == "Common Garter Snake" || interactionObjectSO.interactionType == "Northern Map Turtle" ||
                    interactionObjectSO.interactionType == "Snapping Turtle" || interactionObjectSO.interactionType == "Banded Pennant Dragonfly" ||
                    interactionObjectSO.interactionType == "European Starling" || interactionObjectSO.interactionType == "Muskrat" ||
                    interactionObjectSO.interactionType == "Bald Eagle" || interactionObjectSO.interactionType == "Painted Lady Butterfly" ||
                    interactionObjectSO.interactionType == "Raccoon" || interactionObjectSO.interactionType == "White-Tailed Deer")
        {
            animalInteraction.SetFloat("_HasOutline", 0.0f); // Disable the outline for animal interaction objects
        }

        if (interactionObjectSO.interactionType == "Bradford Pear" || interactionObjectSO.interactionType == "Purple Loosestrife")
        {
            plantInteraction.SetFloat("_HasOutline", 0.0f); // Disable the outline for plant interaction objects
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
        // Implement the interaction logic here
        //Debug.Log("Interacting with " + interactionObjectSO.interactionType); // Debug.Log

        // Prevent opening a new panel if one is already active
        if (AnimalGameManager.dialogueIsActive)
            return;

        if (animalGameManagerScript.lowerBankObjectivesActive && !AnimalGameManager.dialogueIsActive)
        {
            if (interactionObjectSO.interactionType == "Asian Carp")
            {
                HandleAsianCarpInteraction();
            }
            else if (interactionObjectSO.interactionType == "Beaver")
            {
                HandleBeaverInteraction();
            }
            else if (interactionObjectSO.interactionType == "Common Garter Snake")
            {
                HandleCommonGarterSnakeInteraction();
            }
            else if (interactionObjectSO.interactionType == "Northern Map Turtle")
            {
                HandleNorthernMapTurtleInteraction();
            }
            else if (interactionObjectSO.interactionType == "Snapping Turtle")
            {
                HandleSnappingTurtleInteraction();
            }
        }
        
        if (animalGameManagerScript.midBankObjectivesActive && !AnimalGameManager.dialogueIsActive)
        {
            if (interactionObjectSO.interactionType == "Banded Pennant Dragonfly")
            {
                HandleBandedPennantDragonflyInteraction();
            }
            else if (interactionObjectSO.interactionType == "European Starling")
            {
                HandleEuropeanStarlingInteraction();
            }
            else if (interactionObjectSO.interactionType == "Muskrat")
            {
                HandleMuskratInteraction();
            }
        }

        if (animalGameManagerScript.upperBankObjectivesActive && !AnimalGameManager.dialogueIsActive)
        {
            if (interactionObjectSO.interactionType == "Bald Eagle")
            {
                HandleBaldEagleInteraction();
            }
            else if (interactionObjectSO.interactionType == "Painted Lady Butterfly")
            {
                HandlePaintedLadyButterflyInteraction();
            }
            else if (interactionObjectSO.interactionType == "Raccoon")
            {
                HandleRaccoonInteraction();
            }
            else if (interactionObjectSO.interactionType == "White-Tailed Deer")
            {
                HandleWhiteTailedDeerInteraction();
            }
        }

        if (animalGameManagerScript.eventZonesComplete)
        {
            if (changeablePlantScript == null)
            {
                //Debug.LogError("ChangeablePlantScript is not assigned!");
                return;
            }

            if (interactionObjectSO.interactionType == "Bradford Pear")
            {
                if (!changeablePlantScript.isSwapped)
                {
                    //Debug.Log("Handling Bradford Pear interaction.");
                    HandleBradfordPearInteraction();
                }
                else
                {
                    //Debug.Log("Plant has already been swapped.");
                }
            }

            if (interactionObjectSO.interactionType == "Purple Loosestrife")
            {
                if (!changeablePlantScript.isSwapped)
                {
                    //Debug.Log("Handling Purple Loosestrife interaction.");
                    HandlePurpleLoosestrifeInteraction();
                }
                else
                {
                    //Debug.Log("Plant has already been swapped.");
                }
            }
        }
    }

    // Method to handle interaction with the Asian Carp
    private void HandleAsianCarpInteraction()
    {
        //Debug.Log($"Handling Asian Carp has been interacted with {hasBeenInteractedWith}"); // Debug.Log

        // If the player is not already interacting with the Asian Carp...
        if (!hasBeenInteractedWith && !AnimalGameManager.dialogueIsActive)
        {
            //Debug.Log("Asian Carp interacted with!"); // Debug.Log
            asianCarpClicked = true; // Set the static variable to true
            wasAsianCarpPreviouslyClicked = true; // Set the static variable to true

            // Play the interaction sound
            // If the interactSound is not null...
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
            }

            ObjectManager.instance.SetObjectFound("Asian Carp"); // Set the object as found in the ObjectManager

            // Mark all asian carp as interacted
            // Iterate through all conservation interaction objects
            foreach (var obj in allConservationInteractionObjects)
            {
                // If the object is not null and its interaction type is "Aluminum Can"...
                if (obj != null && obj.interactionObjectSO.interactionType == "Asian Carp")
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
        else
        {
            //Debug.Log("Asian Carp has already been interacted with."); // Debug.Log
        }
    }

    private void HandleBaldEagleInteraction()
    {
        //Debug.Log($"Handling Bald Eagle has been interacted with {hasBeenInteractedWith}"); // Debug.Log

        if (!hasBeenInteractedWith && !AnimalGameManager.dialogueIsActive)
        {
            //Debug.Log("Bald Eagle interacted with!"); // Debug.Log
            baldEagleClicked = true; // Set the static variable to true
            wasBaldEaglePreviouslyClicked = true; // Set the static variable to true

            // Play the interaction sound
            // If the interactSound is not null...
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
            }

            ObjectManager.instance.SetObjectFound("Bald Eagle"); // Set the object as found in the ObjectManager

            // Mark all bald eagles as interacted
            // Iterate through all conservation interaction objects
            foreach (var obj in allConservationInteractionObjects)
            {
                // If the object is not null and its interaction type is "Bald Eagle"...
                if (obj != null && obj.interactionObjectSO.interactionType == "Bald Eagle")
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
        else
        {
            //Debug.Log("Bald Eagle has already been interacted with."); // Debug.Log
        }
    }

    private void HandleBandedPennantDragonflyInteraction()
    {
        //Debug.Log($"Handling Banded Pennant Dragonfly has been interacted with {hasBeenInteractedWith}"); // Debug.Log

        if (!hasBeenInteractedWith && !AnimalGameManager.dialogueIsActive)
        {
            //Debug.Log("Banded Pennant Dragonfly interacted with!"); // Debug.Log
            bandedPennantDragonflyClicked = true; // Set the static variable to true
            wasBandedPennantDragonflyPreviouslyClicked = true; // Set the static variable to true

            // Play the interaction sound
            // If the interactSound is not null...
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
            }

            ObjectManager.instance.SetObjectFound("Banded Pennant Dragonfly"); // Set the object as found in the ObjectManager

            // Mark all banded pennant dragonflies as interacted
            // Iterate through all conservation interaction objects
            foreach (var obj in allConservationInteractionObjects)
            {
                // If the object is not null and its interaction type is "Banded Pennant Dragonfly"...
                if (obj != null && obj.interactionObjectSO.interactionType == "Banded Pennant Dragonfly")
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
        else
        {
            //Debug.Log("Banded Pennant Dragonfly has already been interacted with."); // Debug.Log
        }
    }

    private void HandleBeaverInteraction()
    {
        //Debug.Log($"Handling Beaver has been interacted with {hasBeenInteractedWith}"); // Debug.Log

        if (!hasBeenInteractedWith && !AnimalGameManager.dialogueIsActive)
        {
            //Debug.Log("Beaver interacted with!"); // Debug.Log
            beaverClicked = true; // Set the static variable to true
            wasBeaverPreviouslyClicked = true; // Set the static variable to true

            // Play the interaction sound
            // If the interactSound is not null...
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
            }

            ObjectManager.instance.SetObjectFound("Beaver"); // Set the object as found in the ObjectManager

            // Mark all beavers as interacted
            // Iterate through all conservation interaction objects
            foreach (var obj in allConservationInteractionObjects)
            {
                // If the object is not null and its interaction type is "Beaver"...
                if (obj != null && obj.interactionObjectSO.interactionType == "Beaver")
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
        else
        {
            //Debug.Log("Beaver has already been interacted with."); // Debug.Log
        }
    }

    private void HandleCommonGarterSnakeInteraction()
    {
        //Debug.Log($"Handling Common Garter Snake has been interacted with {hasBeenInteractedWith}"); // Debug.Log

        if (!hasBeenInteractedWith && !AnimalGameManager.dialogueIsActive)
        {
            //Debug.Log("Common Garter Snake interacted with!"); // Debug.Log
            garterSnakeClicked = true; // Set the static variable to true
            wasGarterSnakePreviouslyClicked = true; // Set the static variable to true

            // Play the interaction sound
            // If the interactSound is not null...
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
            }

            ObjectManager.instance.SetObjectFound("Common Garter Snake"); // Set the object as found in the ObjectManager

            // Mark all common garter snakes as interacted
            // Iterate through all conservation interaction objects
            foreach (var obj in allConservationInteractionObjects)
            {
                // If the object is not null and its interaction type is "Common Garter Snake"...
                if (obj != null && obj.interactionObjectSO.interactionType == "Common Garter Snake")
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
        else
        {
            //Debug.Log("Common Garter Snake has already been interacted with."); // Debug.Log
        }
    }

    private void HandleEuropeanStarlingInteraction()
    {
        //Debug.Log($"Handling European Starling has been interacted with {hasBeenInteractedWith}"); // Debug.Log

        if (!hasBeenInteractedWith && !AnimalGameManager.dialogueIsActive)
        {
            //Debug.Log("European Starling interacted with!"); // Debug.Log
            easternStarlingClicked = true; // Set the static variable to true
            wasEasternStarlingPreviouslyClicked = true; // Set the static variable to true

            // Play the interaction sound
            // If the interactSound is not null...
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
            }

            ObjectManager.instance.SetObjectFound("European Starling"); // Set the object as found in the ObjectManager

            // Mark all European starlings as interacted
            // Iterate through all conservation interaction objects
            foreach (var obj in allConservationInteractionObjects)
            {
                // If the object is not null and its interaction type is "European Starling"...
                if (obj != null && obj.interactionObjectSO.interactionType == "European Starling")
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
        else
        {
            //Debug.Log("European Starling has already been interacted with."); // Debug.Log
        }
    }

    private void HandleMuskratInteraction()
    {
        //Debug.Log($"Handling Muskrat has been interacted with {hasBeenInteractedWith}"); // Debug.Log

        if (!hasBeenInteractedWith && !AnimalGameManager.dialogueIsActive)
        {
            //Debug.Log("Muskrat interacted with!"); // Debug.Log
            muskratClicked = true; // Set the static variable to true
            wasMuskratPreviouslyClicked = true; // Set the static variable to true

            // Play the interaction sound
            // If the interactSound is not null...
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
            }

            ObjectManager.instance.SetObjectFound("Muskrat"); // Set the object as found in the ObjectManager

            // Mark all muskrats as interacted
            // Iterate through all conservation interaction objects
            foreach (var obj in allConservationInteractionObjects)
            {
                // If the object is not null and its interaction type is "Muskrat"...
                if (obj != null && obj.interactionObjectSO.interactionType == "Muskrat")
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
        else
        {
            //Debug.Log("Muskrat has already been interacted with."); // Debug.Log
        }
    }

    private void HandleNorthernMapTurtleInteraction()
    {
        //Debug.Log($"Handling Northern Map Turtle has been interacted with {hasBeenInteractedWith}"); // Debug.Log

        if (!hasBeenInteractedWith && !AnimalGameManager.dialogueIsActive)
        {
            //Debug.Log("Northern Map Turtle interacted with!"); // Debug.Log
            northernMapTurtleClicked = true; // Set the static variable to true
            wasNorthernMapTurtlePreviouslyClicked = true; // Set the static variable to true

            // Play the interaction sound
            // If the interactSound is not null...
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
            }

            ObjectManager.instance.SetObjectFound("Northern Map Turtle"); // Set the object as found in the ObjectManager

            // Mark all northern map turtles as interacted
            // Iterate through all conservation interaction objects
            foreach (var obj in allConservationInteractionObjects)
            {
                // If the object is not null and its interaction type is "Northern Map Turtle"...
                if (obj != null && obj.interactionObjectSO.interactionType == "Northern Map Turtle")
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
        else
        {
            //Debug.Log("Northern Map Turtle has already been interacted with."); // Debug.Log
        }
    }

    private void HandlePaintedLadyButterflyInteraction()
    {
        //Debug.Log($"Handling Painted Lady Butterfly has been interacted with {hasBeenInteractedWith}"); // Debug.Log

        if (!hasBeenInteractedWith && !AnimalGameManager.dialogueIsActive)
        {
            //Debug.Log("Painted Lady Butterfly interacted with!"); // Debug.Log
            paintedLadyButterflyClicked = true; // Set the static variable to true
            wasPaintedLadyButterflyPreviouslyClicked = true; // Set the static variable to true

            // Play the interaction sound
            // If the interactSound is not null...
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
            }

            ObjectManager.instance.SetObjectFound("Painted Lady Butterfly"); // Set the object as found in the ObjectManager

            // Mark all painted lady butterflies as interacted
            // Iterate through all conservation interaction objects
            foreach (var obj in allConservationInteractionObjects)
            {
                // If the object is not null and its interaction type is "Painted Lady Butterfly"...
                if (obj != null && obj.interactionObjectSO.interactionType == "Painted Lady Butterfly")
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
        else
        {
            //Debug.Log("Painted Lady Butterfly has already been interacted with."); // Debug.Log
        }
    }

    private void HandleRaccoonInteraction()
    {
        //Debug.Log($"Handling Raccoon has been interacted with {hasBeenInteractedWith}"); // Debug.Log

        if (!hasBeenInteractedWith && !AnimalGameManager.dialogueIsActive)
        {
            //Debug.Log("Raccoon interacted with!"); // Debug.Log
            raccoonClicked = true; // Set the static variable to true
            wasRaccoonPreviouslyClicked = true; // Set the static variable to true

            // Play the interaction sound
            // If the interactSound is not null...
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
            }

            ObjectManager.instance.SetObjectFound("Raccoon"); // Set the object as found in the ObjectManager

            // Mark all raccoons as interacted
            // Iterate through all conservation interaction objects
            foreach (var obj in allConservationInteractionObjects)
            {
                // If the object is not null and its interaction type is "Raccoon"...
                if (obj != null && obj.interactionObjectSO.interactionType == "Raccoon")
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
        else
        {
            //Debug.Log("Raccoon has already been interacted with."); // Debug.Log
        }
    }

    private void HandleSnappingTurtleInteraction()
    {
        //Debug.Log($"Handling Snapping Turtle has been interacted with {hasBeenInteractedWith}"); // Debug.Log

        if (!hasBeenInteractedWith && !AnimalGameManager.dialogueIsActive)
        {
            //Debug.Log("Snapping Turtle interacted with!"); // Debug.Log
            snappingTurtleClicked = true; // Set the static variable to true
            wasSnappingTurtlePreviouslyClicked = true; // Set the static variable to true

            // Play the interaction sound
            // If the interactSound is not null...
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
            }

            ObjectManager.instance.SetObjectFound("Snapping Turtle"); // Set the object as found in the ObjectManager

            // Mark all snapping turtles as interacted
            // Iterate through all conservation interaction objects
            foreach (var obj in allConservationInteractionObjects)
            {
                // If the object is not null and its interaction type is "Snapping Turtle"...
                if (obj != null && obj.interactionObjectSO.interactionType == "Snapping Turtle")
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
        else
        {
            //Debug.Log("Snapping Turtle has already been interacted with."); // Debug.Log
        }
    }

    private void HandleWhiteTailedDeerInteraction()
    {
        //Debug.Log($"Handling White-Tailed Deer has been interacted with {hasBeenInteractedWith}"); // Debug.Log

        if (!hasBeenInteractedWith && !AnimalGameManager.dialogueIsActive)
        {
            //Debug.Log("White-Tailed Deer interacted with!"); // Debug.Log
            whiteTailedDeerClicked = true; // Set the static variable to true
            wasWhiteTailedDeerPreviouslyClicked = true; // Set the static variable to true

            // Play the interaction sound
            // If the interactSound is not null...
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
            }

            ObjectManager.instance.SetObjectFound("White-Tailed Deer"); // Set the object as found in the ObjectManager

            // Mark all white-tailed deer as interacted
            // Iterate through all conservation interaction objects
            foreach (var obj in allConservationInteractionObjects)
            {
                // If the object is not null and its interaction type is "White-Tailed Deer"...
                if (obj != null && obj.interactionObjectSO.interactionType == "White-Tailed Deer")
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
        else
        {
            //Debug.Log("White-Tailed Deer has already been interacted with."); // Debug.Log
        }
    }

    private void HandleBradfordPearInteraction()
    {
        // Get the ChangeablePlant component from the current active object
        ChangeablePlant clickedPlant = currentActiveObject != null
            ? currentActiveObject.GetComponentInParent<ChangeablePlant>()
            : null;

        if (clickedPlant != null)
        {
            // Pass the clicked plant to AnimalGameManager
            animalGameManagerScript.changeablePlant = clickedPlant; // Assign the clicked plant to the AnimalGameManager script
            animalGameManagerScript.BradfordPearTreeClicked(clickedPlant);

            // Play the interaction sound
            // If the interactSound is not null...
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
            }

            ObjectManager.instance.SetObjectFound("Bradford Pear"); // Set the object as found in the ObjectManager

            // If the current active object is this object...
            if (currentActiveObject == this)
            {
                ableToInteractWith = false; // Set ableToInteractWith to false to prevent further interaction
                HideInteractionUI(); // Hide the interaction UI for this object
                currentActiveObject = null; // Reset the current active object
            }
        }
        else
        {
            //Debug.LogWarning("Current active object's parent does not have a ChangeablePlant component.");
        }
    }

    private void HandlePurpleLoosestrifeInteraction()
    {
        // Get the ChangeablePlant component from the current active object
        ChangeablePlant clickedPlant = currentActiveObject != null
            ? currentActiveObject.GetComponentInParent<ChangeablePlant>()
            : null;

        if (clickedPlant != null)
        {
            // Pass the clicked plant to AnimalGameManager
            animalGameManagerScript.changeablePlant = clickedPlant; // Assign the clicked plant to the AnimalGameManager script
            animalGameManagerScript.PurpleLoosestrifeClicked(clickedPlant);

            // Play the interaction sound
            // If the interactSound is not null...
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound(); // Play the interaction sound
            }

            ObjectManager.instance.SetObjectFound("Purple Loosestrife"); // Set the object as found in the ObjectManager

            // If the current active object is this object...
            if (currentActiveObject == this)
            {
                ableToInteractWith = false; // Set ableToInteractWith to false to prevent further interaction
                HideInteractionUI(); // Hide the interaction UI for this object
                currentActiveObject = null; // Reset the current active object
            }
        }
        else
        {
            //Debug.LogWarning("Current active object's parent does not have a ChangeablePlant component.");
        }
    }

    public void LowerBankObjectiveObjectsReady()
    {
        foreach (var obj in allConservationInteractionObjects)
        {
            if (obj.objectID == "Lower Bank Objective")
            {
                obj.ableToInteractWith = true; // Set ableToInteractWith to true if the object ID is "Lower Bank Objective"
            }
        } 
    }

    public void LowerBankObjectiveObjectsNotReady()
    {
        foreach (var obj in allConservationInteractionObjects)
        {
            if (obj.objectID == "Lower Bank Objective")
            {
                obj.ableToInteractWith = false; // Set ableToInteractWith to false if the object ID is "Lower Bank Objective"
            }
        }    
    }

    public void MidBankObjectiveObjectsReady()
    {
        foreach (var obj in allConservationInteractionObjects)
        {
            if (obj.objectID == "Mid Bank Objective")
            {
                obj.ableToInteractWith = true; // Set ableToInteractWith to true if the object ID is "Mid Bank Objective"
            }
        }  
    }

    public void MidBankObjectiveObjectsNotReady()
    {
        foreach (var obj in allConservationInteractionObjects)
        {
            if (obj.objectID == "Mid Bank Objective")
            {
                obj.ableToInteractWith = false; // Set ableToInteractWith to false if the object ID is "Mid Bank Objective"
            }
        }  
    }

    public void UpperBankObjectiveObjectsReady()
    {
        foreach (var obj in allConservationInteractionObjects)
        {
            if (obj.objectID == "Upper Bank Objective")
            {
                obj.ableToInteractWith = true; // Set ableToInteractWith to true if the object ID is "Upper Bank Objectives"
            }
        }     
    }

    public void UpperBankObjectiveObjectsNotReady()
    {
        foreach (var obj in allConservationInteractionObjects)
        {
            if (obj.objectID == "Upper Bank Objective")
            {
                obj.ableToInteractWith = false; // Set ableToInteractWith to false if the object ID is "Upper Bank Objective"
            }
        }    
    }

    public void PlantSortingObjectiveObjectsReady()
    {
        foreach (var obj in allConservationInteractionObjects)
        {
            if (obj.objectID == "Plant Sorting Objective")
            {
                obj.ableToInteractWith = true; // Set ableToInteractWith to true if the object ID is "Plant Sorting Objective"
            }
        }
    }

    public void PlantSortingObjectiveObjectsNotReady()
    {
        foreach (var obj in allConservationInteractionObjects)
        {
            if (obj.objectID == "Plant Sorting Objective")
            {
                obj.ableToInteractWith = false; // Set ableToInteractWith to false if the object ID is "Plant Sorting Objective"
            }
        }
    }
}
