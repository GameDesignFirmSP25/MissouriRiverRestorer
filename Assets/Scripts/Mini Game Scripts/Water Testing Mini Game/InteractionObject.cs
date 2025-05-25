using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Booleans")]
    public static bool nearInteractionObject = false; // Static variable to track if the player is near an interaction object
    public static bool surfaceWaveClicked = false; // Static variable to track if the surface wave has been clicked
    public static InteractionObject clickedSurfaceWave; // Static variable to track the clicked surface wave object
    public static bool isClickable = false; // Static variable to track if the object is clickable

    [Header("UI Elements")]
    public GameObject interactPanel; // Reference to the UI Panel for interaction
    public Image interactPanelImage; // Reference to the UI image for interaction prompt
    public TextMeshProUGUI interactPanelText; // Reference to the UI text for interaction prompt

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waterTestingManagerScript = FindAnyObjectByType<WaterTestingManager>();

        player = GameObject.Find("PlayerArmature");

        playerPosition = player.GetComponent<Transform>();

        interactPanel = GameObject.Find("Interact Panel"); // Find the interact panel in the scene

        interactPanelText = GameObject.Find("Interact Panel Text").GetComponent<TextMeshProUGUI>(); // Find the interact panel text in the scene

        interactPanelImage = interactPanel.GetComponent<Image>(); // Find the interact panel image in the scene

        interactPanelImage.enabled = false; // Disable the interact panel image

        interactPanelText.text = ""; // Clear the button text
    }

    // Update is called once per frame
    void Update()
    {
        if (isClickable)
        {
            CheckPlayerDistance();

            // Handle interaction when the player presses "E"
            if (nearInteractionObject && Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }

    // Method to check if the player is within interaction distance
    private void CheckPlayerDistance()
    {
        if (interactionObjectSO == null || playerPosition == null)
        {
            return;
        }

        // Check if the player is within the interaction distance
        if (Vector3.Distance(playerPosition.position, transform.position) < interactionObjectSO.interactionDistance)
        {
            // Display the message
            Debug.Log(message);

            if (!nearInteractionObject)
            {
                nearInteractionObject = true;
                ShowInteractionUI();
            }
        }
        else
        {
            
            if (nearInteractionObject)
            {
                nearInteractionObject = false;
                HideInteractionUI();
            }  
        }
    }

    // Method to show the interaction UI
    private void ShowInteractionUI()
    {
        interactPanelImage.enabled = true; // Enable the interact panel image
        interactPanelText.text = "E to Interact with " + interactionObjectSO.interactionType; // Update the button text with the interaction type
    }

    // Method to hide the interaction UI
    private void HideInteractionUI()
    {
        interactPanelImage.enabled = false; // Disable the interact panel image
        interactPanelText.text = ""; // Clear the button text
    }

    // Method to handle interaction with the object
    private void Interact()
    {
        // Implement the interaction logic here
        Debug.Log("Interacting with " + interactionObjectSO.interactionType);

        if (interactionObjectSO.interactionType == "Surface Wave" && waterTestingManagerScript.objectivesComplete)
        {
            SurfaceWaveClicked (); // Call the SurfaceWaveInteraction method
            
            // Play the interaction sound
            if (interactionObjectSO.interactSound != null)
            {
                interactionObjectSO.interactSound.PlaySound();
            }
        }

        if (!WaterTestingManager.isFirstWaterTestComplete)
        {
            // Check if the interaction type is "Aluminum Can" tag & bool isAluminumCanObjectiveComplete is false
            if (interactionObjectSO.interactionType == "Aluminum Can" && !WaterTestingManager.isAluminumCanObjectiveComplete)
            {
                AluminumCanClicked(); // Call the AluminumCanClicked method

                // Play the interaction sound
                if (interactionObjectSO.interactSound != null)
                {
                    interactionObjectSO.interactSound.PlaySound();
                }
            }

            // Check if the interaction type is "Tire" tag & bool isTireObjectiveComplete is false
            if (interactionObjectSO.interactionType == "Tire" && !WaterTestingManager.isTireObjectiveComplete)
            {
                TireClicked(); // Call the TireClicked method

                // Play the interaction sound
                if (interactionObjectSO.interactSound != null)
                {
                    interactionObjectSO.interactSound.PlaySound();
                }
            }

            // Check if the interaction type is "Gas Can" tag & bool isGasCanObjectiveComplete is false
            if (interactionObjectSO.interactionType == "Gas Can" && !WaterTestingManager.isGasCanObjectiveComplete)
            {
                GasCanisterClicked(); // Call the GasCanisterClicked method

                // Play the interaction sound
                if (interactionObjectSO.interactSound != null)
                {
                    interactionObjectSO.interactSound.PlaySound();
                }
            }

            // Check if the interaction type is "Trash Bag" tag & bool isTrashBagObjective is false
            if (interactionObjectSO.interactionType == "Trash Bag" && !WaterTestingManager.isTrashBagObjectiveComplete)
            {
                TrashBagClicked(); // Call the TrashBagClicked method

                // Play the interaction sound
                if (interactionObjectSO.interactSound != null)
                {
                    interactionObjectSO.interactSound.PlaySound();
                }
            }
        }

        // If bool isFirstWaterTestComplete is true & isSecondWaterTestComplete is false...
        if (WaterTestingManager.isFirstWaterTestComplete && !WaterTestingManager.isSecondWaterTestComplete)
        {
            // Check if the interaction type is "Fish" tag & bool isFishObjectiveComplete is false
            if (interactionObjectSO.interactionType == "Fish" && !WaterTestingManager.isFishObjectiveComplete)
            {
                FishClicked(); // Call the FishClicked method
            }

            //Check if the interaction type is "Mammal" tag & bool isMammalObjectiveComplete is false
            if (interactionObjectSO.interactionType == "Mammal" && !WaterTestingManager.isMammalObjectiveComplete)
            {
                MammalClicked(); // Call the MammalClicked method
            }

            //// Check if the interaction type is "Riverbank" tag & bool isRiverbankObjectiveComplete is false
            //if (interactionObjectSO.interactionType == "Riverbank" && !WaterTestingManager.isRiverbankObjectiveComplete)
            //{
            //    RiverbankClicked(); // Call the RiverbankClicked method
            //}

        }
    }

    // Method to handle surface wave interaction
    private void SurfaceWaveClicked()
    {
        Debug.Log("Surface Wave clicked: " + gameObject.name);
        surfaceWaveClicked = true;
        clickedSurfaceWave = this;
    }

    // Method to handle aluminum can interaction
    private void AluminumCanClicked()
    {
        Debug.Log("Aluminum can clicked"); // Debug.Log

        if (!WaterTestingManager.aPanelIsActive)
        {
            WaterTestingManager.effectsOfAluminumPanelActive = true; // Set bool effectsOfTrashPanelActive to true
            WaterTestingManager.isAluminumCanObjectiveComplete = true; // Set bool isTrashBagObjectiveComplete to true
        }
    }

    // Method to handle tire interaction
    private void TireClicked()
    {
        Debug.Log("Tire clicked"); // Debug.Log

        if (!WaterTestingManager.aPanelIsActive)
        {
            WaterTestingManager.effectsOfTirePanelActive = true; // Set bool effectsOfTrashPanelActive to true
            WaterTestingManager.isTireObjectiveComplete = true; // Set bool isTrashBagObjectiveComplete to true
        }
    }

    // Method to handle gas canister interaction
    private void GasCanisterClicked()
    {
        Debug.Log("Gas canister clicked"); // Debug.Log

        if (!WaterTestingManager.aPanelIsActive)
        {
            WaterTestingManager.effectsOfGasPanelActive = true; // Set bool effectsOfTrashPanelActive to true
            WaterTestingManager.isGasCanObjectiveComplete = true; // Set bool isTrashBagObjectiveComplete to true
        }
    }

    // Method to handle trash bag interaction
    private void TrashBagClicked()
    {
        Debug.Log("Trash bag clicked"); // Debug.Log

        if (!WaterTestingManager.aPanelIsActive)
        {
            WaterTestingManager.effectsOfTrashPanelActive = true; // Set bool effectsOfTrashPanelActive to true
            WaterTestingManager.isTrashBagObjectiveComplete = true; // Set bool isTrashBagObjectiveComplete to true
        }
    }

    // Method to handle fish interaction
    private void FishClicked()
    {
        Debug.Log("Fish clicked"); // Debug.Log

        if (!WaterTestingManager.aPanelIsActive)
        {
            WaterTestingManager.effectsOfBiodiversity1PanelActive = true; // Set bool effectsOfFishPanelActive to true
            WaterTestingManager.isFishObjectiveComplete = true; // Set bool isFishObjectiveComplete to true
        }
    }

    // Method to handle mammal interaction
    private void MammalClicked()
    {
        Debug.Log("Mammal clicked"); // Debug.Log

        if (!WaterTestingManager.aPanelIsActive)
        {
            WaterTestingManager.effectsOfBiodiversity3PanelActive = true; // Set bool effectsOfFishPanelActive to true
            WaterTestingManager.isMammalObjectiveComplete = true; // Set bool isFishObjectiveComplete to true
        }
    }

    //// Method to handle riverbank interaction
    //private void RiverbankClicked()
    //{
    //    Debug.Log("Riverbank clicked"); // Debug.Log
    //    if (!WaterTestingManager.aPanelIsActive)
    //    {
    //        WaterTestingManager.effectsOfBiodiversity2PanelActive = true; // Set bool effectsOfFishPanelActive to true
    //        WaterTestingManager.isRiverbankObjectiveComplete = true; // Set bool isFishObjectiveComplete to true
    //    }
    //}
}
