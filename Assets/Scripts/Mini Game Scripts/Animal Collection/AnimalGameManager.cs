using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using System.Data.SqlTypes;

public class AnimalGameManager : BaseMiniGameManager
{
    [Header("UI Elements")]
    [SerializeField]
    TextMeshProUGUI objectiveText1;

    [SerializeField]
    TextMeshProUGUI objectiveText2;

    [SerializeField]
    TextMeshProUGUI objectiveText3;

    [SerializeField]
    TextMeshProUGUI objectiveSubtext1;

    [SerializeField]
    TextMeshProUGUI objectiveSubtext2;

    [SerializeField]
    TextMeshProUGUI objectiveSubtext3;

    [SerializeField]
    TextMeshProUGUI objectiveSubtext4;

    [SerializeField]
    TextMeshProUGUI objectiveSubtext5;

    [SerializeField]
    TextMeshProUGUI objectiveSubtext6;

    [SerializeField]
    TextMeshProUGUI objectiveSubtext7;

    [SerializeField]
    TextMeshProUGUI EndText;

    [SerializeField]
    TextMeshProUGUI deerEventZoneText;

    [SerializeField]
    TextMeshProUGUI birdEventZoneText;

    [SerializeField]
    TextMeshProUGUI fishEventZoneText;

    [SerializeField]
    TextMeshProUGUI animalsFoundCounterText;

    private Slider slider;

    [Header("Game Objects")]
    [SerializeField]
    GameObject startPanel;

    [SerializeField]
    GameObject objectivesPanel;

    [SerializeField]
    GameObject endOfGamePanel;

    [SerializeField]
    GameObject bradfordPearTreePanel;

    [SerializeField]
    GameObject invasivePlantPanel1;

    [SerializeField]
    GameObject invasivePlantPanel2;

    [SerializeField]
    GameObject invasivePlantPanel3;

    [SerializeField]
    GameObject deerEventZonePanel;

    [SerializeField]
    GameObject birdEventZonePanel;

    [SerializeField]
    GameObject fishEventZonePanel;

    [SerializeField]
    GameObject deerEventZoneText1;

    [SerializeField]
    GameObject birdEventZoneText1;

    [SerializeField]
    GameObject fishEventZoneText1;

    [SerializeField]
    GameObject objectiveSubText1;

    [SerializeField]
    GameObject objectiveSubText2;

    [SerializeField]
    GameObject objectiveSubText3;

    [SerializeField]
    GameObject objectiveSubText4;

    [SerializeField]
    GameObject objectiveSubText5;

    [SerializeField]
    GameObject objectiveSubText6;

    [SerializeField]
    GameObject objectiveSubText7;

    [SerializeField]
    GameObject animalsFoundCounterText1;

    [SerializeField]
    GameObject startButton;

    [SerializeField]
    GameObject returnButton;

    [SerializeField]
    GameObject pauseButton;

    [SerializeField]
    GameObject replacePlantButton;

    [SerializeField]
    GameObject choice0Button;

    [SerializeField]
    GameObject choice1Button;

    [SerializeField] 
    GameObject choice2Button;

    [SerializeField]
    GameObject choice3Button;

    [SerializeField]
    GameObject clickCounter;

    [SerializeField]
    GameObject deerEventZone;

    [SerializeField]
    GameObject birdEventZone;

    [SerializeField]
    GameObject fishEventZone;

    [Header("Arrays and Lists")]
    public GameObject[] dialoguePanels = new GameObject[12];
    public GameObject[] eventPanels = new GameObject[3];
    public List<string> animalNames = new List<string>
    {
        "Asian Carp",
        "Eastern Starling",
        "White-Tailed Deer",
        "Bald Eagle",
        "Beaver",
        "Raccoon",
        "Muskrat",
        "Snapping Turtle",
        "Common Garter Snake",
        "Northern Map Turtle",
        "Banded Pennant Dragonfly",
        "Painted Lady Butterfly"
    };

    [Header("Float Variables")]
    private float targetProgress = 1f;
    private float progressIncrement = 0.2f;

    [Header("Interger Variables")]
    private int animalsFound = 0;

    [Header("Booleans")]
    public static bool trappingCompleted = false; // Global variable to check if trapping is completed
    public static bool dialogueIsActive = false;
    public static bool paintedLadyButterflyPanelActive = false;
    public static bool easternStarlingPanelActive = false; 
    public static bool whiteTailedDeerPanelActive = false;
    public static bool bandedPennantDragonflyPanelActive = false; 
    public static bool garterSnakePanelActive = false; 
    public static bool baldEaglePanelActive = false;
    public static bool muskratPanelActive = false;
    public static bool snappingTurtlePanelActive = false;
    public static bool beaverPanelActive = false; 
    public static bool raccoonPanelActive = false; 
    public static bool asianCarpPanelActive = false; 
    public static bool northernMapTurtlePanelActive = false;
    public static bool endOfGamePanelActive = false;
    public static bool eventZonePanelActive = false;
    public static bool bradfordPearTreePanelActive = false;
    public static bool invasivePlant1PanelActive = false;
    public static bool invasivePlant2PanelActive = false;
    public static bool invasivePlant3PanelActive = false;
    public static bool deerEventZoneComplete = false;
    public static bool birdEventZoneComplete = false;
    public static bool fishEventZoneComplete = false;
    private bool hasResetDialogueState = false;
    private bool hasResetEventPanelState = false;
    private bool objective1Active = false;
    private bool objective2Active = false;
    private bool objective3Active = false;
    public bool objectivesComplete = false;
    public bool deerEventActive = false;
    public bool birdEventActive = false;
    public bool fishEventActive = false;
    public bool deerEventObjectiveSet = false;
    public bool birdEventObjectiveSet = false;
    public bool fishEventObjectiveSet = false;
    public bool isAsianCarpFound = false;
    public bool isBaldEagleFound = false;
    public bool isBandedPennantDragonflyFound = false;
    public bool isBeaverFound = false;
    public bool isCommonGarterSnakeFound = false;
    public bool isEasternStarlingFound = false;
    public bool isMuskratFound = false;
    public bool isNorthernMapTurtleFound = false;
    public bool isPaintedLadyButterflyFound = false;
    public bool isRaccoonFound = false;
    public bool isSnappingTurtleFound = false;
    public bool isWhiteTailedDeerFound = false;
    public bool isBradfordPearTreeFound = false;
    public bool wasBradfordPearTreeClicked = false;
    public bool wasInvasivePlant1Clicked = false;
    public bool wasInvasivePlan2Clicked = false;
    public bool wasInvasivePlant3Clicked = false;
    public bool wasInvasivePlantsPanelButtonClicked = false;
    public bool readyToSpawnSycamoreTree = false;
    public bool isPressed = false;

    [Header("Raycast Variables")]
    private Ray ray;
    private RaycastHit hit;

    [Header("Script References")]
    public ClickCounter clickCounterScript;
    public ChangeablePlant changeablePlant;

    private void Awake()
    {
        UpdateAnimalCounter();
    }

    void Start()
    {
        GetClickCounter(); // Get click counter and slider component
        InitializeUI(); // Initialize UI elements
        GetDialoguePanels(); // Get dialogue panels
        GetEventZonePanels(); // Get event zone panels
        GetChangeablePlants();
        DeactivateAllDialoguePanels(); // Deactivate all dialogue panels
        DeactivateAllEventZonePanels(); // Deactivate all event zone panels
        DeactivatePlantSortingPanels(); // Deactivate all plant sorting panels
        DisableObjectives(); // Disable all objective subtexts
        DisableEventZones(); // Diasble event trigger zones
        Time.timeScale = 0; // Freeze time at start of game
    }

    // Update is called once per frame
    void Update()
    {   
        // If bool lowerBankEntered is true and bool objective1Active is false...
        if (LowerBankZoneTrigger.lowerBankEntered && !objective1Active)
        {
            LowerBankEntered(); //  Call method LowerBankEntered
        }

        // If bool midBankEntered is true and bool objective2Active is false...
        if (MidBankZoneTrigger.midBankEntered && !objective2Active)
        {
            MidBankEntered(); // Call method MidBankEntered
        }

        // If bool upperBankEntered is true and objective3Active is false...
        if (UpperBankZoneTrigger.upperBankEntered && !objective3Active)
        {
            UpperBankEntered(); // Call method UpperBankEntered
        }

        AnimalClicked(); // Check if animal is clicked

        DialoguePanelClicked(); // Check if any dialogue panel is clicked

        StrikethroughText(); // Strikethrough text


        // If list count of animalNames equals 8...
        if (animalNames.Count <= 8)
        {
            EnableEventZones(); // Enable Event Zones
        }


        // If bool deerEventTriggered  is true and bool deerEventZoneComplete is fale
        if (DeerEventZone.deerEventTriggered && !deerEventZoneComplete)
        {
            Debug.Log("Deer event zone condition met. Triggering DeerEventZone.");
            DeerEventZoneEntered(); // Call method DeerEventZoneEntered
        }
        else
        {
            // If bool deerEvetTriggered is not true...
            if (!DeerEventZone.deerEventTriggered)
            {
                Debug.Log("Deer Event not triggered yet.");
            }

            // If bool deerEventComplete is true...
            if (deerEventZoneComplete)
            {
                Debug.Log("Deer Event zone already completed.");
            }
        }

        // If bool birdEventTriggered and bool BirdEventZoneComplete is false...
        if (BirdEventZone.birdEventTriggered && !birdEventZoneComplete)
        {
            Debug.Log("Bird event zone condition met. Triggering BirdEventZone.");
            BirdEventZoneEntered(); // Call method BirdEventZoneEntered
        }
        else
        {
            // If bool birdEventTriggered is false...
            if (!BirdEventZone.birdEventTriggered)
            {
                Debug.Log("Bird Event not triggered yet.");
            }

            // If bool birdEventZoneComplete is true...
            if (birdEventZoneComplete)
            {
                Debug.Log("Bird Event zone already completed.");
            }
        }
        // If bool fishEventTriggered is true and fishEventComplete is false...
        if (FishEventZone.fishEventTriggered && !fishEventZoneComplete)
        {
            Debug.Log("Fish event zone condition met. Triggering FishEventZone.");
            FishEventZoneEntered(); // Call method FishEventZoneEntered
        }
        else
        {
            // If bool fishEventTrigerred is false...
            if (!FishEventZone.fishEventTriggered)
            {
                Debug.Log("Fish Event not triggered yet.");
            }

            // If bool fishEventZoneComplete is true...
            if (fishEventZoneComplete)
            {
                Debug.Log("Fish Event zone already completed.");
            }
        }

        EventPanelClicked(); // Call method EventPanelClicked

        // If bool deerEventActive and bool eventZonePanelActive is false and bool deerObjectiveSet is true...
        if (deerEventActive && !eventZonePanelActive && deerEventObjectiveSet)
        {
            GetAnimalClicks(); // Call method GetAnimalClicks
        }

        // If bool birdEventActive and bool eventZonePanelActive is false and bool bool birdEventObjectiveSet is true...
        if (birdEventActive && !eventZonePanelActive && birdEventObjectiveSet) 
        {
            GetAnimalClicks(); // Call method GetAnimalClicks
        }

        // If bool fishEventActive is true && bool eventZonePanelActive is false and fish EventObjectiveSet is true...
        if (fishEventActive && !eventZonePanelActive && fishEventObjectiveSet)
        {
            GetAnimalClicks(); // Call method GetAnimalClicks
        }

        ObjectivesComplete(); // Check if all objectives are complete
    }

    // Method that triggers on start button press
    public void StartButton()
    {
        objectivesPanel.SetActive(true); // show objectives panel
        startButton.SetActive(false); // hide start button
        startPanel.SetActive(false); // hide start panel
        pauseButton.SetActive(true); // show pause button
        animalsFoundCounterText1.SetActive(true); // show animals found counter
        Time.timeScale = 1; // Unfreeze time
    }

    //Method to set UI 
    public void InitializeUI()
    {
        objectivesPanel.SetActive(false); // hide objectives panel
        returnButton.SetActive(false); // hide return button
        startButton.SetActive(true); // show start button
        startPanel.SetActive(true); // show start panel
        pauseButton.SetActive(false); // hide pause button
        endOfGamePanel.SetActive(false); // hide end of game panel
        clickCounter.SetActive(false); // hide click counter slider
        animalsFoundCounterText1.SetActive(false); // hide animals found counter
        choice0Button.SetActive(false); // hide choice 0 button
        choice1Button.SetActive(false); // hide choice 1 button
        choice2Button.SetActive(false); // hide choice 2 button
        choice3Button.SetActive(false); // hide choice 3 button
    }

    // Method that gets panels in dialoguePanels array
    private void GetDialoguePanels()
    {
        GameObject panel1 = dialoguePanels[0]; // Get the Asian Carp panel
        GameObject panel2 = dialoguePanels[1]; // Get the Eastern Starling panel
        GameObject panel3 = dialoguePanels[2]; // Get the White-Tailed Deer panel
        GameObject panel4 = dialoguePanels[3]; // Get the Bald Eagle panel
        GameObject panel5 = dialoguePanels[4]; // Get the Beaver panel
        GameObject panel6 = dialoguePanels[5]; // Get the Raccoon panel
        GameObject panel7 = dialoguePanels[6]; // Get the Muskerat panel
        GameObject panel8 = dialoguePanels[7]; // Get the Snapping Turtle panel
        GameObject panel9 = dialoguePanels[8]; // Get the Garter Snake panel
        GameObject panel10 = dialoguePanels[9]; // Get the Northern Map Turtle panel
        GameObject panel11 = dialoguePanels[10]; // Get the Banded Pennant Dragonfly panel
        GameObject panel12 = dialoguePanels[11]; // Get the Painted Lady Butterfly panel
    }

    // Method that gets panels in eventPanels array
    private void GetEventZonePanels()
    {
        GameObject panel13 = eventPanels[0]; // Get the deer event zone panel
        GameObject panel14 = eventPanels[1]; // Get the bird event zone panel
        GameObject panel15 = eventPanels[2]; // Get the fish event zone panel
    }

    private void GetChangeablePlants()
    {
        if (changeablePlant == null)
        {
            Debug.LogWarning("changeablePlant is null. Attempting to find it...");
            changeablePlant = FindFirstObjectByType<ChangeablePlant>();
        }

        if (changeablePlant != null)
        {
            Debug.Log("changeablePlant successfully assigned.");
        }
        else
        {
            Debug.LogError("changeablePlant is still null! Ensure the ChangeablePlant script is attached to a GameObject in the scene.");

        }
    }

    // Method to get click counter
    private void GetClickCounter()
    {
        clickCounter = GameObject.FindGameObjectWithTag("Click Counter"); // clickCounter is equal to gameObject with tag "Click Counter"
        slider = clickCounter.GetComponent<Slider>(); // slider equals Slider component
    }
    
    public int AnimalsFound
    {
        get { return animalsFound; }
        set
        {
            animalsFound = value;
            UpdateAnimalCounter();
        }
    }
    // Method to check for animal clicks in event zones
    private void CheckForAnimalClicks()
    {
        // If left mouse button is down and isPressed is false...
        if (Input.GetMouseButtonDown(0) && !isPressed)
        {
            isPressed = true; // Set bool isPressed to true
            InvokeClickCounter(); // Call method InvokeProgressBar
            Debug.Log("Animal clicked..."); // Debug.Log
        }

        // If left mouse button is down and isPressed is true...
        if (Input.GetMouseButtonUp(0) && isPressed)
        {
            isPressed = false; // Set bool isPressed to false
            RaycastScript.eventAnimalClicked = false; // Set bool testTubeClicked to false
        }
    }

    public void UpdateAnimalCounter()
    {
        animalsFoundCounterText.text = $"Animals Found: {animalsFound.ToString()} / 12";
    }

    // Method to increment progress on click counter
    private void InvokeClickCounter()
    {
        Debug.Log("Click counted.");
        clickCounterScript.IncrementProgress(progressIncrement); // increment progress by variable progressIncrement
    }

    // Method to handle animal click in event zones
    private void GetAnimalClicks()
    {
        clickCounter.SetActive(true); // Enable clickCounter 

        // If bool eventAnimalClicked... 
        if (RaycastScript.eventAnimalClicked)
        {
            // If slider value is less than variable targetProgress
            if (slider.value < targetProgress)
            {
                Debug.Log("Checking for animal clicks.");
                CheckForAnimalClicks(); // Call method to check for animal clicks in event zones
            }
        }

        // If slider value is equal to variable targetProgress 
        if (slider.value >= targetProgress)
        {
            // If bool isDeerEventEntered is true and bool deerEventZoneComplete is false and bool deerEventActive is true...
            if (DeerEventZone.isDeerEventEntered && !deerEventZoneComplete && deerEventActive)
            {
                deerEventZoneComplete = true;
                Debug.Log("Deer Event Complete."); 
                clickCounter.SetActive(false);
                deerEventActive = false;
                deerEventZone.gameObject.SetActive(false);
                deerEventZonePanel.SetActive(true);
                deerEventZoneText.text = "Great Job! You dispersed that group of deer! Now native plants can grow!";
                DeerEventZone.isDeerEventEntered = false;
                slider.value = 0f;
                CancelInvoke();
            }
            
            // If bool birdEventComplete is false
            if (BirdEventZone.isBirdEventEntered && !birdEventZoneComplete && birdEventActive)
            {
                birdEventZoneComplete = true;
                Debug.Log("Bird Event Complete.");
                clickCounter.SetActive(false);
                birdEventActive = false;
                birdEventZone.gameObject.SetActive(false);
                birdEventZonePanel.SetActive(true);
                birdEventZoneText.text = "Great Job! The starlings have flown away! Now native birds can repopulate!";
                BirdEventZone.isBirdEventEntered = false;
                slider.value = 0f;
                CancelInvoke();
            }

            if (FishEventZone.isFishEventEntered && !fishEventZoneComplete && fishEventActive)
            {
                fishEventZoneComplete = true;
                Debug.Log("Fish Event Complete.");
                clickCounter.SetActive(false);
                fishEventActive = false;
                fishEventZone.gameObject.SetActive(false);
                fishEventZonePanel.SetActive(true);
                fishEventZoneText.text = "Great Job! You caught the Asian Carp! Now Now the native fish are safe!";
                FishEventZone.isFishEventEntered = false;
                slider.value = 0f;
                CancelInvoke();
            }
        }
    }

    // Method to disable all text objects
    public void DisableObjectives()
    {
        objectiveSubtext1.gameObject.SetActive(false); //hide objective subtext 1
        objectiveSubtext2.gameObject.SetActive(false); //hide objective subtext 2
        objectiveSubtext3.gameObject.SetActive(false); //hide objective subtext 3
        objectiveSubtext4.gameObject.SetActive(false); //hide objective subtext 4
        objectiveSubtext5.gameObject.SetActive(false); //hide objective subtext 5
        objectiveSubtext6.gameObject.SetActive(false); //hide objective subtext 6
        objectiveSubtext7.gameObject.SetActive(false); //hide objective subtext 7
    }

    // Method to enable event zones
    public void EnableEventZones()
    {
        deerEventZone.gameObject.SetActive(true); // Enable deer event zone
        birdEventZone.gameObject.SetActive(true);
        fishEventZone.gameObject.SetActive(true);
    }

    // Method to disable event zones
    public void DisableEventZones()
    {
        deerEventZone.gameObject.SetActive(false); // Disable deer event zone
        birdEventZone.gameObject.SetActive(false);
        fishEventZone.gameObject.SetActive(false);
    }

    // Method to activate specific panel in index
    public void ActivateDialoguePanel(int dialoguePanelIndex)
    {
        if (dialoguePanelIndex >= 0 && dialoguePanelIndex < dialoguePanels.Length)
        {
            dialoguePanels[dialoguePanelIndex].SetActive(true); // Activate the specified panel
            dialogueIsActive = true; // Set the active panel flag to true
            Debug.Log("Panel " + dialoguePanelIndex + " activated."); // Debug.Log
        }
        else
        {
            Debug.LogError("Invalid panel index: " + dialoguePanelIndex); // Log an error if the index is invalid
        }
    }

    // Method to deactivate a specific panel
    public void DeactivateDialoguePanel(int dialoguePanelIndex)
    {
        dialoguePanels[dialoguePanelIndex].SetActive(false); // Activate the specified panel
        dialogueIsActive = false; // Set the active panel flag to false
        Debug.Log("Panel " + dialoguePanelIndex + " deactivated."); // Debug.Log
    }

    // Method to deactivate all panels
    public void DeactivateAllDialoguePanels()
    {
        // Loop through each dialoguePanel in the dialoguePanels array
        foreach (GameObject dialoguePanel in dialoguePanels)
        {
            dialoguePanel.SetActive(false); // Deactivate all panels
        }

        dialogueIsActive = false; // Set the active panel flag to false
    }

    public void ActivateEventPanel(int eventPanelIndex)
    {
        eventPanels[eventPanelIndex].SetActive(true);
        eventZonePanelActive = true;
    }

    public void DeactivateAllEventZonePanels()
    {
        deerEventZonePanel.SetActive(false);
        birdEventZonePanel.SetActive(false);
        fishEventZonePanel.SetActive(false);
    }

    public void DeactivatePlantSortingPanels()
    {
        bradfordPearTreePanel.SetActive(false);
        invasivePlantPanel1.SetActive(false);
        invasivePlantPanel2.SetActive(false);
        invasivePlantPanel3.SetActive(false);
    }

    void EndLevel()
    {
        // If bool trappingCompleted is false
        if (trappingCompleted == false)
        {
            EndText.text = "Great Job clearing out the invasive species!"; // Set end text for player
            returnButton.SetActive(true); // Show return button

            // For Game Progression
            TriggerMiniGameCompleteEvent(0);   // Can add score pass through
            trappingCompleted = true; //set global variable to true
        }
    }

    // Method to return to the main scene
    public void ReturnButton()
    {
        SceneManager.LoadScene("Overworld"); //load main scene
    }

    private void AnimalClicked()
    {
        // If eastern starling is clicked and dialogue is not active...
        if (RaycastScript.easternStarlingClicked && !dialogueIsActive)
        {
            EasternStarlingClicked(); // Handle eastern starling click
        }

        // If white-tailed deer is clicked and dialogue is not active...
        if (RaycastScript.whiteTailedDeerClicked && !dialogueIsActive)
        {
            WhiteTailedDeerClicked(); // Handle white-tailed deer click
        }

        // If banded pennant dragonfly is clicked and dialogue is not active...
        if (RaycastScript.bandedPennantDragonflyClicked && !dialogueIsActive)
        {
            BandedPennantDragonflyClicked(); // Handle banded pennant dragonfly click
        }

        // If common garter snake is clicked and dialogue is not active...
        if (RaycastScript.garterSnakeClicked && !dialogueIsActive)
        {
            CommonGarterSnakeClicked(); // Handle common garter snake click
        }

        // If bald eagle is clicked and dialogue is not active...
        if (RaycastScript.baldEagleClicked && !dialogueIsActive)
        {
            BaldEagleClicked(); // Handle bald eagle click
        }

        // If painted lady butterfly is clicked and dialogue is not active...
        if (RaycastScript.paintedLadyButterflyClicked && !dialogueIsActive)
        {
            PaintedLadyButterflyClicked();
        }

        // If muskrat is clicked and dialogue is not active...
        if (RaycastScript.muskratClicked && !dialogueIsActive) 
        {
            MuskratClicked();
        }

        // If snapping turtle is clicked and dialogue is not active...
        if (RaycastScript.snappingTurtleClicked && !dialogueIsActive)
        {
            SnappingTurtleClicked();
        }

        // If beaver is clicked and dialogue is not active...
        if (RaycastScript.beaverClicked && !dialogueIsActive) 
        {
            BeaverClicked();
        }

        // If raccoon is clicked and dialogue is not active...
        if (RaycastScript.raccoonClicked && !dialogueIsActive) 
        {
            RaccoonClicked();
        }

        // If northern map turtle is clicked and dialogue is not active...
        if (RaycastScript.northernMapTurtleClicked && !dialogueIsActive) 
        {
            NorthernMapTurtleClicked();
        }

        // If asian carp is clicked and dialogue is not active...
        if (RaycastScript.asianCarpClicked && !dialogueIsActive) 
        {
            AsianCarpClicked();
        }

        //// If bradford pear tree is clicked
        //if (RaycastScript.bradfordPearTreeClicked)
        //{
        //    BradfordPearTreeClicked();
        //}
    }

    // Method to check if any dialogue panel is clicked
    private void DialoguePanelClicked()
    {
        // If dialogue is active and the dialogue panel is open...
        if (GarterSnakeClickHandler.isGarterSnakePanelClicked && !hasResetDialogueState)
        {
            ResetDialogueState();
            hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
        }
        if (WhiteTailedDeerClickHandler.isWhiteTailedDeerPanelClicked && !hasResetDialogueState)
        {  
            ResetDialogueState();
            hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
        }
        if (NorthernMapTurtleClickHandler.isNorthernMapTurtlePanelClicked && !hasResetDialogueState)
        {
            ResetDialogueState();
            hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
        }
        if (RaccoonClickHandler.isRaccoonPanelClicked && !hasResetDialogueState)
        {
            ResetDialogueState();
            hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
        }
        if (MuskratClickHandler.isMuskratPanelClicked && !hasResetDialogueState)
        {
            ResetDialogueState();
            hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
        }
        if (PaintedLadyButterflyClickHandler.isPaintedLadyButterflyPanelClicked && !hasResetDialogueState)
        {
            ResetDialogueState();
            hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
        }
        if (AsianCarpClickHandler.isAsianCarpPanelClicked && !hasResetDialogueState)
        {
            ResetDialogueState();
            hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
        }
        if (BandedPennantDragonflyClickHandler.isBandedPennantDragonflyPanelClicked && !hasResetDialogueState)
        {
            ResetDialogueState();
            hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
        }
        if (BaldEagleClickHandler.isBaldEaglePanelClicked && !hasResetDialogueState)
        {
            ResetDialogueState();
            hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
        }
        if (BeaverClickHandler.isBeaverPanelClicked && !hasResetDialogueState)
        {
            ResetDialogueState();
            hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
        }
        if (SnappingTurtleClickHandler.isSnappingTurtlePanelClicked && !hasResetDialogueState)
        {
            ResetDialogueState();
            hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
        }
        if (EasternStarlingClickHandler.isEasternStarlingPanelClicked && !hasResetDialogueState)
        {
            ResetDialogueState();
            hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
        }
    }

    // Method to check if event panell is clicked
    private void EventPanelClicked()
    {
        if (EventZonePanelClickHandler.isEventZonePanelClicked && !hasResetEventPanelState)
        {
            ResetEventPanelState();
            hasResetEventPanelState = true;
        }
    }

    // Handle clicks on eastern starling
    private void EasternStarlingClicked()
    {
        ActivateDialoguePanel(1);
        RaycastScript.easternStarlingClicked = false; // Reset the click handler for eastern starling
        dialogueIsActive = true; // Set dialogue active
        isEasternStarlingFound = true; // Set the flag for eastern starling found
        animalNames.Remove("Eastern Starling"); // Remove eastern starling from the list of animal names
    }

    // Handle clicks on white-tailed deer
    private void WhiteTailedDeerClicked()
    {
        ActivateDialoguePanel(2);
        RaycastScript.whiteTailedDeerClicked = false; // Reset the click handler for white-tailed deer
        dialogueIsActive = true; // Set dialogue active
        isWhiteTailedDeerFound = true; // Set the flag for white-tailed deer found
        animalNames.Remove("White-Tailed Deer"); // Remove white-tailed deer from the list of animal names
    }

    // Handle clicks on banded pennant dragonfly
    private void BandedPennantDragonflyClicked()
    {
        ActivateDialoguePanel(10);
        RaycastScript.bandedPennantDragonflyClicked = false; // Reset the click handler for banded pennant dragonfly
        dialogueIsActive = true; // Set dialogue active
        isBandedPennantDragonflyFound = true; // Set the flag for banded pennant dragonfly found
        animalNames.Remove("Banded Pennant Dragonfly"); // Remove banded pennant dragonfly from the list of animal names
    }

    // Handle clicks on garter snake
    private void CommonGarterSnakeClicked()
    {
        ActivateDialoguePanel(8);
        RaycastScript.garterSnakeClicked = false; // Reset the click handler for common garter snake
        dialogueIsActive = true; // Set dialogue active
        isCommonGarterSnakeFound = true; // Set the flag for common garter snake found
        animalNames.Remove("Common Garter Snake"); // Remove common garter snake from the list of animal names
    }

    // Handle clicks on bald eagle
    private void BaldEagleClicked()
    {
        ActivateDialoguePanel(3);
        RaycastScript.baldEagleClicked = false; // Reset the click handler for bald eagle
        dialogueIsActive = true; // Set dialogue active
        isBaldEagleFound = true; // Set the flag for bald eagle found
        animalNames.Remove("Bald Eagle"); // Remove bald eagle from the list of animal names
    }

    // Handle clicks on muskrat
    private void MuskratClicked()
    {
        ActivateDialoguePanel(6);
        RaycastScript.muskratClicked = false; // Reset the click handler for muskrat
        dialogueIsActive = true; // Set dialogue active
        isMuskratFound = true; // Set the flag for muskrat found
        animalNames.Remove("Muskrat"); // Remove muskrat from the list of animal names
    }

    // Handle clicks on snapping turtle
    private void SnappingTurtleClicked()
    {
        ActivateDialoguePanel(7);
        RaycastScript.snappingTurtleClicked = false; // Reset the click handler for snapping turtle
        dialogueIsActive = true; // Set dialogue active
        isSnappingTurtleFound = true; // Set the flag for snapping turtle found
        animalNames.Remove("Snapping Turtle"); // Remove snapping turtle from the list of animal names
    }

    // Handle clicks on beaver
    private void BeaverClicked()
    {
        ActivateDialoguePanel(4);
        RaycastScript.beaverClicked = false; // Reset the click handler for beaver
        dialogueIsActive = true; // Set dialogue active
        isBeaverFound = true; // Set the flag for beaver found
        animalNames.Remove("Beaver"); // Remove beaver from the list of animal names
    }

    // Handle clicks on raccoon
    private void RaccoonClicked()
    {
        ActivateDialoguePanel(5);
        RaycastScript.raccoonClicked = false; // Reset the click handler for raccoon
        dialogueIsActive = true; // Set dialogue active
        isRaccoonFound = true; // Set the flag for raccoon found
        animalNames.Remove("Raccoon"); // Remove raccoon from the list of animal names
    }

    // Handle clicks on northern map turtle
    private void NorthernMapTurtleClicked()
    {
        ActivateDialoguePanel(9);
        RaycastScript.northernMapTurtleClicked = false; // Reset the click handler for northern map turtle
        dialogueIsActive = true; // Set dialogue active
        isNorthernMapTurtleFound = true; // Set the flag for northern map turtle found
        animalNames.Remove("Northern Map Turtle"); // Remove northern map turtle from the list of animal names
    }

    // Handle clicks on asian carp
    private void AsianCarpClicked()
    {
        ActivateDialoguePanel(0);
        RaycastScript.asianCarpClicked = false; // Reset the click handler for asian carp
        dialogueIsActive = true; // Set dialogue active
        isAsianCarpFound = true; // Set the flag for asian carp found
        animalNames.Remove("Asian Carp"); // Remove asian carp from the list of animal names
    }

    // Handle clicks on painted lady butterfly
    private void PaintedLadyButterflyClicked()
    {
        ActivateDialoguePanel(11);
        RaycastScript.paintedLadyButterflyClicked = false; // Reset the click handler for painted lady butterfly
        dialogueIsActive = true; // Set dialogue active
        isPaintedLadyButterflyFound = true; // Set the flag for painted lady butterfly found
        animalNames.Remove("Painted Lady Butterfly"); // Remove painted lady butterfly from the list of animal names
    }

    //// Handle clicks on bradford pear tree
    //private void BradfordPearTreeClicked()
    //{
    //    RaycastScript.bradfordPearTreeClicked = false;
    //    isBradfordPearTreeFound = true;
    //}

    // Method to reset dialogue state
    public void ResetDialogueState()
    {
        Debug.Log("Resetting dialogue state..."); // Debug.Log
        DeactivateAllDialoguePanels(); // Deactivate all dialogue panels
        dialogueIsActive = false; // Set dialogue inactive
        hasResetDialogueState = false; // Reset the flag to allow future dialogue interactions
    }

    // Method to reset event zone panel state
    public void ResetEventPanelState()
    {
        Debug.Log("Resetting event panel state...");
        DeactivateAllEventZonePanels();
        hasResetEventPanelState = false;
        //DeerEventZonePanelClickHandler.isDeerEventZonePanelClicked = false;
        //BirdEventZonePanelClickHandler.isBirdEventZonePanelClicked = false;
        //FishEventZonePanelClickHandler.isFishEventZonePanelClicked = false;
        //Time.timeScale = 1;
        //Debug.Log($"eventZonePanelActive: {eventZonePanelActive}, hasResetEventPanelState: {hasResetEventPanelState}, isEventZonePanelClicked: {EventZonePanelClickHandler.isEventZonePanelClicked}");
    }

    // Set subtext for objective 1
    private void LowerBankEntered()
    {
        Debug.Log("Player is exploring the lower bank."); //Debug.Log
        objective1Active = true; // Set bool objective1Active to true
        objectiveSubtext1.gameObject.SetActive(true); //show objective subtext 1
        objectiveSubtext1.text = "*Interact with flora."; //set objective subtext 1 text
        objectiveSubtext2.gameObject.SetActive(true); //show objective subtext 2
        objectiveSubtext2.text = "*Find and interact with Asian Carp."; //set objective subtext 2 text
    }

    // Set subtext for objective 2
    private void MidBankEntered()
    {
        Debug.Log("Player is exploring the Mid Bank!"); //Debug.Log
        objective2Active = true; // Set bool objective2Active to true
        objectiveSubtext3.gameObject.SetActive(true); //show objective subtext 3
        objectiveSubtext3.text = "*Interact with flora."; //set objective subtext 3 text
        objectiveSubtext4.gameObject.SetActive(true); //show objective subtext 4
        objectiveSubtext4.text = "*Find and interact with White-Tailed Deer."; //set objective subtext 4 text
    }

    // Set subtext for oibjective 3
    private void UpperBankEntered()
    {
        Debug.Log("Player is exploring the Upper Bank!"); //Debug.Log
        objective3Active = true; // Set bool objective3Active to true
        objectiveSubtext5.gameObject.SetActive(true); //show objective subtext 5
        objectiveSubtext5.text = "*Interact with flora."; //set objective subtext 5 text
        objectiveSubtext6.gameObject.SetActive(true); //show objective subtext 6
        objectiveSubtext6.text = "*Find and interact with Bradford Pear Tree."; //set objective subtext 6 text
        objectiveSubtext7.gameObject.SetActive(true); //show objective subtext 7
        objectiveSubtext7.text = "*Find and interact with European Starling."; //set objective subtext 7 text
    }

    //  Method to called when deer event zone is entered
    public void DeerEventZoneEntered()
    {
        //Debug.Log($"dialogueIsactive: {dialogueIsActive}"); // Debug.Log to check if dialogue is active
        if (!dialogueIsActive && !eventZonePanelActive && !deerEventObjectiveSet)
        {
            Debug.Log("Start deer event."); // Debug.Log
            deerEventZonePanel.SetActive(true); // Show event zone panel
            eventZonePanelActive = true; // Set event zone panel active
            deerEventZoneText.text = "Oh no! There are too many White-Tailed deer here. Look at how they have destroyed the plants. Click to drive them off."; // Set event zone text
            objectivesPanel.SetActive(false); // Hide objectives panel
            deerEventActive = true; // Set deer event active
            deerEventObjectiveSet = true;
            //Time.timeScale = 0f;
        }  
    }

    // Method to called when bird event zone is entered
    public void BirdEventZoneEntered()
    {
        //Debug.Log($"dialogueIsactive: {dialogueIsActive}"); // Debug.Log to check if dialogue is active
        if (!dialogueIsActive && !eventZonePanelActive && !birdEventObjectiveSet)
        {
            Debug.Log("Start bird event."); // Debug.Log
            birdEventZonePanel.SetActive(true); // Show event zone panel
            eventZonePanelActive = true; // Set event zone panel active
            birdEventZoneText.text = "Check out those European Starling. They seem to have taken over those trees driving away native birds. Hurry! Click to shoo them away."; // Set event zone text
            objectivesPanel.SetActive(false); // Hide objectives panel
            birdEventActive = true; // Set bird event active
            birdEventObjectiveSet = true;
            //Time.timeScale = 0f;
        } 
    }

    // Method to called when fish event zone is entered
    public void FishEventZoneEntered()
    {
        //Debug.Log($"dialogueIsactive: {dialogueIsActive}"); // Debug.Log to check if dialogue is active
        if (!dialogueIsActive && !eventZonePanelActive && !fishEventObjectiveSet)
        {
            Debug.Log("Start fish event."); // Debug.Log
            fishEventZonePanel.SetActive(true); // Show event zone panel
            eventZonePanelActive = true; // Set event zone panel active
            fishEventZoneText.text = "Look at the river. There seems to be a disturbance. Large Asian Carp are attacking the native fish. We must relocate them. Quickly click on them to catch them."; // Set event zone text
            objectivesPanel.SetActive(false); // Hide objectives panel
            fishEventActive = true; // Set fish event active
            fishEventObjectiveSet = true;
            //Time.timeScale = 0f;
        }
    }

    // Method to activate panel when Bradford Pear Tree is clicked
    public void BradfordPearTreeClicked(ChangeablePlant clickedPlant)
    {
        Debug.Log($"Bradford Pear Tree Clicked: {clickedPlant.plantID}");
        wasBradfordPearTreeClicked = true;
        bradfordPearTreePanel.SetActive(true);
        bradfordPearTreePanelActive = true;
        changeablePlant = clickedPlant; // Assign the clicked plant to changeablePlant
    }

    public void InvasivePlant1Clicked()
    {
        wasInvasivePlant1Clicked = true;
        invasivePlantPanel1.SetActive(true);
        invasivePlant1PanelActive = true;
    }

    public void InvasivePlant2Clicked()
    {
        wasInvasivePlan2Clicked = true;
        invasivePlantPanel2.SetActive(true);
        invasivePlant2PanelActive = true;
    }

    public void InvasivePlant3Clicked()
    {
        wasInvasivePlant3Clicked = true;
        invasivePlantPanel3.SetActive(true);
        invasivePlant3PanelActive = true;
    }

    //
    public void BradfordPearTreePanelButtonClicked()
    {
        Debug.Log($"Swapping plant: {changeablePlant.plantID}");
        wasBradfordPearTreeClicked = false; // Reset the flag
        bradfordPearTreePanel.SetActive(false);
        bradfordPearTreePanelActive = false;
        replacePlantButton.SetActive(false);
        wasInvasivePlantsPanelButtonClicked = true;
        Invoke("ResetButtonState", 1f);
        changeablePlant.SwapPlants(); // Swap the plant to Bradford Pear Tree

    }

    public void InvasivePlant1PanelButtonClicked()
    {
        invasivePlantPanel1.SetActive(false);
        invasivePlant1PanelActive = false;
        choice0Button.SetActive(false);
        choice1Button.SetActive(false);
        choice2Button.SetActive(false);
        choice3Button.SetActive(false);
        wasInvasivePlantsPanelButtonClicked = true;
        Invoke("ResetButtonState", 1f);
    }

    public void InvasivePlant2PanelButtonClicked()
    {
        invasivePlantPanel2.SetActive(false);
        invasivePlant1PanelActive = false;
        wasInvasivePlantsPanelButtonClicked = true;
        Invoke("ResetButtonState", 1f);
    }

    public void InvasivePlant3PanelButtonClicked()
    {
        invasivePlantPanel3.SetActive(false);
        invasivePlant1PanelActive = false;
        wasInvasivePlantsPanelButtonClicked = true;
        Invoke("ResetButtonState", 1f);
    }

    public void ResetButtonState()
    {
        wasInvasivePlantsPanelButtonClicked = false;
        replacePlantButton.SetActive(true); // Show the replace plant button
    }

    // Method to strikethrough text
    private void StrikethroughText()
    {
        if (isAsianCarpFound && !objectivesComplete)
        {
            objectiveSubtext2.fontStyle = FontStyles.Strikethrough;
        }

        if (isWhiteTailedDeerFound && !objectivesComplete)
        {
            objectiveSubtext4.fontStyle = FontStyles.Strikethrough;
        }

        if (isBradfordPearTreeFound && !objectivesComplete)
        {
            objectiveSubtext6.fontStyle = FontStyles.Strikethrough;
        }

        if (isEasternStarlingFound && !objectivesComplete)
        {
            objectiveSubtext7.fontStyle = FontStyles.Strikethrough;
        }
    }

    // Method to check if all objectives are complete
    private void ObjectivesComplete()
    {
        if (isAsianCarpFound && isBaldEagleFound && isBandedPennantDragonflyFound && isBeaverFound && isCommonGarterSnakeFound
            && isEasternStarlingFound && isBeaverFound && isMuskratFound && isNorthernMapTurtleFound && isPaintedLadyButterflyFound
            && isRaccoonFound && isSnappingTurtleFound && isWhiteTailedDeerFound && isBradfordPearTreeFound && deerEventZoneComplete && fishEventZoneComplete && birdEventZoneComplete)
            { 
                objectivesComplete = true; // Set objectivesComplete to true if all objectives are met
            }
        // If all objectives are active and trapping is not completed...
        if (objectivesComplete && !trappingCompleted)
        {
            Debug.Log("All objectives are complete!"); // Debug.Log
            endOfGamePanel.SetActive(true); // Show end of game panel
            endOfGamePanelActive = true; // Set end of game panel active
            //DisableEventZones(); // Disable event zones
        }
    }

     // For dev use. Skips game
     public void DebugCompleteGame()
     {
          Debug.Log("All objectives are complete!"); // Debug.Log
          endOfGamePanel.SetActive(true); // Show end of game panel
          endOfGamePanelActive = true; // Set end of game panel active
          //DisableEventZones(); // Disable event zones
          TriggerMiniGameCompleteEvent(0);
          trappingCompleted = true; //set global variable to true
          SceneManager.LoadScene("Overworld");
     }
}
