using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using StarterAssets;
using System.Runtime.CompilerServices;

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
    TextMeshProUGUI objectiveSubtext8;

    [SerializeField]
    TextMeshProUGUI objectiveSubtext9;

    [SerializeField]
    TextMeshProUGUI objectiveSubtext10;

    [SerializeField]
    TextMeshProUGUI objectiveSubtext11;

    [SerializeField]
    TextMeshProUGUI objectiveSubtext12;

    [SerializeField]
    TextMeshProUGUI objectiveSubtext13;

    [SerializeField]
    TextMeshProUGUI objectiveSubtext14;

    [SerializeField]
    TextMeshProUGUI objectiveSubtext15;

    [SerializeField]
    TextMeshProUGUI eventObjectiveSubtext1;

    [SerializeField]
    TextMeshProUGUI eventObjectiveSubtext2;

    [SerializeField]
    TextMeshProUGUI eventObjectiveSubtext3;

    [SerializeField]
    TextMeshProUGUI plantSortingObjectiveText;

    [SerializeField]
    TextMeshProUGUI EndText;

    [SerializeField]
    TextMeshProUGUI deerEventZoneText;

    [SerializeField]
    TextMeshProUGUI birdEventZoneText;

    [SerializeField]
    TextMeshProUGUI fishEventZoneText;

    //[SerializeField]
    //TextMeshProUGUI animalsFoundCounterText;

    [SerializeField]
    TextMeshProUGUI bradfordPearsSwappedCounterText;

    [SerializeField]
    TextMeshProUGUI purpleLoosestrifesSwappedCounterText;

    [SerializeField]
    TextMeshProUGUI plantsCorrectlySwappedCounterText;

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
    GameObject purpleLoosestrifePanel;

    [SerializeField]
    GameObject deerEventZonePanel;

    [SerializeField]
    GameObject birdEventZonePanel;

    [SerializeField]
    GameObject fishEventZonePanel;

    [SerializeField]
    GameObject plantSortingPanel;

    [SerializeField]
    GameObject exploringIndicatorPanel;

    [SerializeField]
    GameObject eventsStartPanel;

    [SerializeField]
    GameObject eventsObjectivesPanel;

    [SerializeField]
    GameObject lowerBankObjective;

    [SerializeField]
    GameObject midBankObjective;

    [SerializeField]
    GameObject upperBankObjective;

    [SerializeField]
    GameObject plantSortingObjective;

    [SerializeField]
    GameObject correctPlantSwappedPanel;

    [SerializeField]
    GameObject incorrectPlantSwappedPanel;

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
    GameObject objectiveSubText8;

    [SerializeField] 
    GameObject objectiveSubText9;

    [SerializeField]
    GameObject objectiveSubText10;

    [SerializeField]
    GameObject objectiveSubText11;

    [SerializeField]
    GameObject objectiveSubText12;

    [SerializeField]
    GameObject objectiveSubText13;

    [SerializeField]
    GameObject objectiveSubText14;

    [SerializeField]
    GameObject objectiveSubText15;

    [SerializeField]
    GameObject animalsFoundCounterText1;

    [SerializeField]
    GameObject bradfordPearsSwappedCounterText1;

    [SerializeField]
    GameObject purpleLoosestrifesSwappedCounterText1;

    [SerializeField]
    GameObject plantsCorrectlySwappedCounterText1;

    [SerializeField]
    GameObject startButton;

    [SerializeField]
    GameObject returnButton;

    [SerializeField]
    GameObject pauseButton;

    [SerializeField]
    GameObject replaceWithSycamoreButton;

    [SerializeField]
    GameObject replaceWithBoxElderButton;

    [SerializeField]
    GameObject replaceWithAmericanLotusButton;

    [SerializeField]
    GameObject replaceWithCordgrassButton;

    [SerializeField] 
    GameObject replaceWithSwampMilkweedButton;

    [SerializeField]
    GameObject replaceWithYellowConeflowerButton;

    [SerializeField]
    GameObject clickCounter;

    [SerializeField]
    GameObject deerEventZone;

    [SerializeField]
    GameObject birdEventZone;

    [SerializeField]
    GameObject fishEventZone;

    [SerializeField]
    GameObject deerEventZoneArrow;

    [SerializeField]
    GameObject birdEventZoneArrow;

    [SerializeField]
    GameObject fishEventZoneArrow;

    [SerializeField]
    private GameObject groupOfDeer;

    [SerializeField]
    private GameObject groupOfStarlings;

    [SerializeField]
    private GameObject groupOfCarp;

    [Header("Arrays and Lists")]
    public GameObject[] dialoguePanels = new GameObject[12];
    public GameObject[] eventPanels = new GameObject[3];
    public List<GameObject> asianCarp;
    public List<GameObject> baldEagle;
    public List<GameObject> bandedPennantDragonfly;
    public List<GameObject> beaver;
    public List<GameObject> europeanStarling;
    public List<GameObject> garterSnake;
    public List<GameObject> muskrat;
    public List<GameObject> northernMapTurtle;
    public List<GameObject> paintedLadyButterfly;
    public List<GameObject> raccoon;
    public List<GameObject> snappingTurtle;
    public List<GameObject> whiteTailedDeer;
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
    private float deerClickIncrement = 0.2f;
    private float birdClickIncrement = 0.2f; // Increment for bird event clicks
    private float fishClickIncrement = 0.25f; // Increment for fish event clicks
    private float dialogueStateTimer = 0.5f; // Timer for dialogue state set
    private float dialoguePanelCooldown = 0f;
    private const float dialoguePanelCooldownDuration = 0.2f; // Cooldown time for dialogue panels

    [Header("Interger Variables")]
    private int animalsFound = 0;
    private int bradfordPearsSwapped = 0;
    private int purpleLoosestrifesSwapped = 0;
    private int plantsCorrectlySwapped = 0; 

    [Header("Booleans")]
    public static bool trappingCompleted = false; // Global variable to check if trapping is completed
    public static bool dialogueIsActive = false;
    public static bool objectivesShown = false;
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
    public static bool purpleLoosestrifePanelActive = false;
    public static bool eventsStartPanelActive = false;
    public static bool plantSortingPanelActive = false;
    public static bool deerEventZoneComplete = false;
    public static bool birdEventZoneComplete = false;
    public static bool fishEventZoneComplete = false;
    public static bool playButtonClick = false;
    private bool hasResetDialogueState = false;
    private bool hasResetEventPanelState = false;
    private bool hasResetPlantSortingPanelState = false;
    private bool canClosePanel = false;
    private bool waitForNextEPress = false;
    private bool eKeyWasDownLastFrame = false;
    private bool plantSortingPanelShown = false;
    public bool lowerBankObjectivesActive = false;
    public bool midBankObjectivesActive = false;
    public bool upperBankObjectivesActive = false;
    public bool lowerBankObjectivesComplete = false;
    public bool midBankObjectivesComplete = false;
    public bool upperBankObjectivesComplete = false;
    public bool objectivesComplete = false;
    public bool eventZonesComplete = false;
    public bool plantSortingComplete = false;
    public bool deerEventActive = false;
    public bool birdEventActive = false;
    public bool fishEventActive = false;
    public bool deerEventPanelActive = false;
    public bool birdEventPanelActive = false;
    public bool fishEventPanelActive = false;
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
    public bool wasPurpleLoosestrifeClicked = false;
    public bool startPlantSorting = false;
    public bool wasReplaceWithSycamoreButtonClicked = false;
    public bool wasReplaceWithBoxElderButtonClicked = false;
    public bool wasReplaceWithAmericanLotusButtonClicked = false;
    public bool wasReplaceWithCordgrassButtonClicked = false;
    public bool wasReplaceWithSwampMilkweedButtonClicked = false;
    public bool wasReplaceWithYellowConeflowerButtonClicked = false;
    public bool readyToSpawnSycamoreTree = false;
    public bool isPressed = false;

    [Header("Raycast Variables")]
    private Ray ray;
    private RaycastHit hit;

    [Header("Script References")]
    public ClickCounter clickCounterScript;
    public ChangeablePlant changeablePlant;
    public TypewriterUi typewriterUiScript;
    public ConservationInteractionObject conservationInteractionObjectScript;
    public AsianCarpClickHandler asianCarpClickHandler;
    public BaldEagleClickHandler baldEagleClickHandler;
    public BandedPennantDragonflyClickHandler bandedPennantDragonflyClickHandler;
    public BeaverClickHandler beaverClickHandler;
    public EasternStarlingClickHandler easternStarlingClickHandler;
    public GarterSnakeClickHandler garterSnakeClickHandler;
    public MuskratClickHandler muskratClickHandler;
    public NorthernMapTurtleClickHandler northernMapTurtleClickHandler;
    public PaintedLadyButterflyClickHandler paintedLadyButterflyClickHandler;
    public RaccoonClickHandler raccoonClickHandler;
    public SnappingTurtleClickHandler snappingTurtleClickHandler;
    public WhiteTailedDeerClickHandler whiteTailedDeerClickHandler;
    public EventsStartPanelClickHandler eventsStartPanelClickHandler;
    public DeerEventZonePanelClickHandler deerEventZonePanelClickHandler;
    public BirdEventZonePanelClickHandler birdEventZonePanelClickHandler;
    public FishEventZonePanelClickHandler fishEventZonePanelClickHandler;
    public PlantSortingPanelClickHandler plantSortingPanelClickHandler;
    public IncorrectPlantSwappedPanelClickHandler incorrectPlantSwappedPanelClickHandler;
    public CorrectPlantSwappedPanelClickHandler correctPlantSwappedPanelClickHandler;
    public PausMenuManager pauseMenuManager;

    [Header("Player Input")]
    public StarterAssetsInputs playerInput;

    [Header("Audio")]
    [SerializeField]
    private SFXMaker interactButton;

    [SerializeField]
    private SFXMaker invasiveSpeciesDisposed;

    [SerializeField]
    private SFXMaker animalClickedSound;

    [SerializeField]
    private SFXMaker correctPlantSwapped;

    [SerializeField]
    private SFXMaker incorrectPlantSwapped;

    [SerializeField]
    private SFXMaker plantGrabbed;

    [Header("Materials")]
    private Material deerInteraction;
    private Material birdInteraction;
    private Material fishInteraction;

    private void Awake()
    {
        //UpdateAnimalCounter();
        UpdateBradfordPearsSwappedCounter(); // Update the bradford pears swapped counter on awake
        UpdatePurpleLoosestrifesSwappedCounter(); // Update the purple loosestrifes swapped counter on awake
        UpdatePlantsCorrectlySwappedCounter(); // Update the plants swapped counter on awake
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
        conservationInteractionObjectScript.LowerBankObjectiveObjectsNotReady(); // Call method LowerBankObjectiveObjectsNotReady
        conservationInteractionObjectScript.MidBankObjectiveObjectsNotReady(); // Call method MidBankObjectiveObjectsNotReady
        conservationInteractionObjectScript.UpperBankObjectiveObjectsNotReady(); // Call method UpperBankObjectiveObjectsNotReady
        conservationInteractionObjectScript.PlantSortingObjectiveObjectsNotReady(); // Call method PlantSortingObjectiveObjectsNotReady

        Time.timeScale = 0; // Freeze time at start of game
    }

    // Update is called once per frame
    void Update()
    {
        if (dialoguePanelCooldown > 0f)
        {
            dialoguePanelCooldown -= Time.deltaTime;
        } 

        if (pauseMenuManager.isPaused)
        {
            objectivesPanel.SetActive(false); // Hide objectives panel
            exploringIndicatorPanel.SetActive(false); // Hide exploring indicator panel
        }
        else
        {
            if (lowerBankObjectivesActive || midBankObjectivesActive || upperBankObjectivesActive)
            {
                objectivesPanel.SetActive(true); // Show objectives panel
            }

            if (objectivesShown)
            {
                exploringIndicatorPanel.SetActive(true); // Show exploring indicator panel
            }                              
        }

        AnimalClicked(); // Check if animal is clicked

        CheckForEPress(); // Check if E key is pressed

        CheckForPreviouslyClickedAnimals(); // Check if any animals were previously clicked

        StrikethroughText(); // Strikethrough text

        LowerBankObjectivesComplete(); // Check if lower bank objectives are complete

        EnableFishEventZone(); // Enable event zones if conditions are met

        FishEventZoneEntered(); // Check if fish event zone is entered

        EventPanelClicked(); // Call method EventPanelClicked

        // If bool fishEventActive is true && bool eventZonePanelActive is false and fish EventObjectiveSet is true...
        if (fishEventActive && !eventZonePanelActive && fishEventObjectiveSet)
        {
            foreach (FishBackAndForthMovement fish in groupOfCarp.GetComponentsInChildren<FishBackAndForthMovement>())
            {
                if (fish != null)
                {
                    fish.SetOutline(true); // Set outline for each bird in the group
                }
            }

            foreach (FishForthAndBackMovement fish in groupOfCarp.GetComponentsInChildren<FishForthAndBackMovement>())
            {
                if (fish != null)
                {
                    fish.SetOutline(true); // Set outline for each bird in the group
                }
            }

            GetAnimalClicks(); // Call method GetAnimalClicks
        }

        if (fishEventZoneComplete)
        {
            foreach (FishBackAndForthMovement fish in groupOfCarp.GetComponentsInChildren<FishBackAndForthMovement>())
            {
                if (fish != null)
                {
                    fish.SetOutline(false); // Set outline for each bird in the group
                }
            }

            foreach (FishForthAndBackMovement fish in groupOfCarp.GetComponentsInChildren<FishForthAndBackMovement>())
            {
                if (fish != null)
                {
                    fish.SetOutline(false); // Hide outline for each bird in the group
                }
            }

            MidBankObjectives(); // Call method MidBankObjectives
            conservationInteractionObjectScript.LowerBankObjectiveObjectsNotReady(); // Call method LowerBankObjectiveObjectsNotReady
            conservationInteractionObjectScript.MidBankObjectiveObjectsReady(); // Call method MidBankObjectiveObjectsReady
        }

        MidBankObjectivesComplete(); // Check if mid bank objectives are complete

        EnableBirdEventZone(); // Enable bird event zone if conditions are met

        BirdEventZoneEntered(); // Check if bird event zone is entered

        // If bool birdEventActive and bool eventZonePanelActive is false and bool bool birdEventObjectiveSet is true...
        if (birdEventActive && !eventZonePanelActive && birdEventObjectiveSet)
        {
            foreach (BirdFlight bird in groupOfStarlings.GetComponentsInChildren<BirdFlight>())
            {
                if (bird != null)
                {
                    bird.SetOutline(true); // Set outline for each bird in the group
                }
            }

            GetAnimalClicks(); // Call method GetAnimalClicks
        }

        if (birdEventZoneComplete)
        {
            foreach (BirdFlight bird in groupOfStarlings.GetComponentsInChildren<BirdFlight>())
            {
                if (bird != null)
                {
                    bird.SetOutline(false); // hide outline for each bird in the group
                }
            }

            UpperBankObjectives(); // Call method UpperBankObjectives
            conservationInteractionObjectScript.MidBankObjectiveObjectsNotReady(); // Call method MidBankObjectiveObjectsNotReady
            conservationInteractionObjectScript.UpperBankObjectiveObjectsReady(); // Call method UpperBankObjectiveObjectsReady
        }

        UpperBankObjectivesComplete(); // Check if upper bank objectives are complete

        EnableDeerEventZone(); // Enable deer event zone if conditions are met

        // If bool deerEventActive and bool eventZonePanelActive is false and bool deerObjectiveSet is true...
        if (deerEventActive && !eventZonePanelActive && deerEventObjectiveSet)
        {
            foreach (ChangeNavAgentSpeed deer in groupOfDeer.GetComponentsInChildren<ChangeNavAgentSpeed>())
            {
                if (deer != null)
                {
                    deer.SetOutline(true); // Set outline for each bird in the group
                }
            }

            GetAnimalClicks(); // Call method GetAnimalClicks
        } 

        EventsComplete(); // Check if all events are complete

        ShowPlantSortingPanel(); // Show plant sorting panel if conditions are met

        PlantSortingPanelClicked(); // Check if plant sorting panel is clicked

        ObjectivesComplete(); // Check if all objectives are complete

        PlantSortingComplete(); // Check if plant sorting is complete

        RunEndGameCycle();
    }

    // Method that triggers on start button press
    public void StartButton()
    {
        PlayButtonClick(); // Play button click sound
        exploringIndicatorPanel.SetActive(true); // show exploring indicator panel
        objectivesPanel.SetActive(true); // show objectives panel
        LowerBankObjectives(); //  Call method LowerBankEntered
        conservationInteractionObjectScript.LowerBankObjectiveObjectsReady(); // Call method LowerBankObjectiveObjectsReady
        objectivesShown = true; // set objectivesShown to true
        startButton.SetActive(false); // hide start button
        startPanel.SetActive(false); // hide start panel
        pauseButton.SetActive(true); // show pause button
        Time.timeScale = 1; // Unfreeze time
    }

    //Method to set UI 
    public void InitializeUI()
    {
        objectivesPanel.SetActive(false); // hide objectives panel
        plantSortingPanel.SetActive(false); // hide plant sorting panel
        exploringIndicatorPanel.SetActive(false); // hide exploring indicator panel
        eventsObjectivesPanel.SetActive(false); // hide events objectives panel
        plantSortingObjective.SetActive(false); // hide plant sorting objective
        eventsStartPanel.SetActive(false); // hide events start panel
        correctPlantSwappedPanel.SetActive(false); // hide correct plant swapped panel
        incorrectPlantSwappedPanel.SetActive(false); // hide incorrect plant swapped panel
        returnButton.SetActive(false); // hide return button
        startButton.SetActive(true); // show start button
        startPanel.SetActive(true); // show start panel
        pauseButton.SetActive(false); // hide pause button
        endOfGamePanel.SetActive(false); // hide end of game panel
        clickCounter.SetActive(false); // hide click counter slider
        bradfordPearsSwappedCounterText1.SetActive(false); // hide bradford pears swapped counter
        purpleLoosestrifesSwappedCounterText1.SetActive(false); // hide purple loosestrifes swapped counter
        plantsCorrectlySwappedCounterText1.SetActive(false); // hide plants swapped counter
        replaceWithAmericanLotusButton.SetActive(false); // hide choice 0 button
        replaceWithCordgrassButton.SetActive(false); // hide choice 1 button
        replaceWithSwampMilkweedButton.SetActive(false); // hide choice 2 button
        replaceWithYellowConeflowerButton.SetActive(false); // hide choice 3 button
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

    // Method to play button click sound
    public void PlayButtonClick()
    {
        interactButton.PlaySound(); // Play button click sound
    }

    // Method to play animal clicked sound
    public void PlayAnimalClickedSound()
    {
        animalClickedSound.PlaySound(); // Play animal clicked sound
    }

    // Method to play invasive species disposed sound
    public void PlayInvasiveSpeciesDisposedSound()
    {
        invasiveSpeciesDisposed.PlaySound(); // Play invasive species disposed sound
    }

    // Method to play correct plant swapped sound
    public void PlayCorrectPlantSwappedSound()
    {
        correctPlantSwapped.PlaySound(); // Play correct plant swapped sound
    }

    // Method to play incorrect plant swapped sound
    public void PlayIncorrectPlantSwappedSound()
    {
        incorrectPlantSwapped.PlaySound(); // Play incorrect plant swapped sound
    }

    // Method to play plant grabbed sound
    public void PlayPlantGrabbedSound()
    {
        plantGrabbed.PlaySound(); // Play plant grabbed sound
    }

    // Method to get changeable plant script
    private void GetChangeablePlants()
    {
        if (changeablePlant == null)
        {
            //Debug.LogWarning("changeablePlant is null. Attempting to find it..."); // Debug.Log
            changeablePlant = FindFirstObjectByType<ChangeablePlant>(); // Attempt to find the ChangeablePlant script in the scene
        }

        if (changeablePlant != null)
        {
            //Debug.Log("changeablePlant successfully assigned."); // Debug.Log
        }
        else
        {
            //Debug.LogError("changeablePlant is still null! Ensure the ChangeablePlant script is attached to a GameObject in the scene."); // Debug.Log

        }
    }

    // Method to get click counter
    private void GetClickCounter()
    {
        clickCounter = GameObject.FindGameObjectWithTag("Click Counter"); // clickCounter is equal to gameObject with tag "Click Counter"
        slider = clickCounter.GetComponent<Slider>(); // slider equals Slider component
    }
    
    // Integer properties to manage animals found
    public int AnimalsFound
    {
        get { return animalsFound; } // Getter for animalsFound
        set
        {
            animalsFound = value; // Setter for animalsFound
            //UpdateAnimalCounter(); // Update the animals found counter
        }
    }

    public void CheckForEPress()
    {
        bool eKeyDown = Input.GetKeyDown(KeyCode.E);

        // If the E key is pressed and waitForNextEPress is true...
        if (eKeyDown && waitForNextEPress)
        {
            waitForNextEPress = false; // Allow new panel to open after E is pressed again
            return;
        }

        //If the E key is pressed...
        if (eKeyDown && !eKeyWasDownLastFrame)
        {
            //Debug.Log("E key pressed. Checking for interaction..."); // Debug.Log
            //If dialogue is active and cannot close panel...
            if (dialogueIsActive && !canClosePanel)
            {
                return; // return
            }

            if (dialogueIsActive && canClosePanel && TypewriterUi.isTypingComplete)
            {
                if (asianCarpPanelActive && !hasResetDialogueState)
                {
                    //Debug.Log("Asian Carp panel is active. Interacting..."); // Debug.Log
                    asianCarpClickHandler.OnPointerClick(null); // Call OnPointerClick method in AsianCarpClickHandler
                    hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
                    PlayButtonClick(); // Play button click sound
                    DeactivateDialoguePanel(0); // Deactivate Asian Carp panel
                    ResetDialogueState();
                }
                else if (baldEaglePanelActive && !hasResetDialogueState)
                {
                    //Debug.Log("Bald Eagle panel is active. Interacting..."); // Debug.Log
                    baldEagleClickHandler.OnPointerClick(null); // Call OnPointerClick method in BaldEagleClickHandler
                    hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
                    PlayButtonClick(); // Play button click sound
                    DeactivateDialoguePanel(3); // Deactivate Bald Eagle panel
                    ResetDialogueState(); // Reset dialogue state
                }
                else if (bandedPennantDragonflyPanelActive && !hasResetDialogueState)
                {
                    //Debug.Log("Banded Pennant Dragonfly panel is active. Interacting..."); // Debug.Log
                    bandedPennantDragonflyClickHandler.OnPointerClick(null); // Call OnPointerClick method in BandedPennantDragonflyClickHandler
                    hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
                    PlayButtonClick(); // Play button click sound
                    DeactivateDialoguePanel(10); // Deactivate Banded Pennant Dragonfly panel
                    ResetDialogueState(); // Reset dialogue state
                }
                else if (beaverPanelActive && !hasResetDialogueState)
                {
                    //Debug.Log("Beaver panel is active. Interacting..."); // Debug.Log
                    beaverClickHandler.OnPointerClick(null); // Call OnPointerClick method in BeaverClickHandler
                    hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
                    PlayButtonClick(); // Play button click sound
                    DeactivateDialoguePanel(4); // Deactivate Beaver panel
                    ResetDialogueState(); // Reset dialogue state
                }
                else if (easternStarlingPanelActive && !hasResetDialogueState)
                {
                    //Debug.Log("Eastern Starling panel is active. Interacting..."); // Debug.Log
                    easternStarlingClickHandler.OnPointerClick(null); // Call OnPointerClick method in EasternStarlingClickHandler
                    hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
                    PlayButtonClick(); // Play button click sound
                    DeactivateDialoguePanel(1); // Deactivate Eastern Starling panel
                    ResetDialogueState();
                }
                else if (garterSnakePanelActive && !hasResetDialogueState)
                {
                    //Debug.Log("Garter Snake panel is active. Interacting..."); // Debug.Log
                    garterSnakeClickHandler.OnPointerClick(null); // Call OnPointerClick method in GarterSnakeClickHandler
                    hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
                    PlayButtonClick(); // Play button click sound
                    DeactivateDialoguePanel(8); // Deactivate Garter Snake panel
                    ResetDialogueState(); // Reset dialogue state
                }
                else if (muskratPanelActive && !hasResetDialogueState)
                {
                    //Debug.Log("Muskrat panel is active. Interacting..."); // Debug.Log
                    muskratClickHandler.OnPointerClick(null); // Call OnPointerClick method in MuskratClickHandler
                    hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
                    PlayButtonClick(); // Play button click sound
                    DeactivateDialoguePanel(6); // Deactivate Muskrat panel
                    ResetDialogueState(); // Reset dialogue state
                }
                else if (northernMapTurtlePanelActive && !hasResetDialogueState)
                {
                    //Debug.Log("Northern Map Turtle panel is active. Interacting..."); // Debug.Log
                    northernMapTurtleClickHandler.OnPointerClick(null); // Call OnPointerClick method in NorthernMapTurtleClickHandler
                    hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
                    PlayButtonClick(); // Play button click sound
                    DeactivateDialoguePanel(9); // Deactivate Northern Map Turtle panel
                    ResetDialogueState(); // Reset dialogue state
                }
                else if (paintedLadyButterflyPanelActive && !hasResetDialogueState)
                {
                    //Debug.Log("Painted Lady Butterfly panel is active. Interacting..."); // Debug.Log
                    paintedLadyButterflyClickHandler.OnPointerClick(null); // Call OnPointerClick method in PaintedLadyButterflyClickHandler
                    hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
                    PlayButtonClick(); // Play button click sound
                    DeactivateDialoguePanel(11); // Deactivate Painted Lady Butterfly panel
                    ResetDialogueState(); // Reset dialogue state
                }
                else if (raccoonPanelActive && !hasResetDialogueState)
                {
                    //Debug.Log("Raccoon panel is active. Interacting..."); // Debug.Log
                    raccoonClickHandler.OnPointerClick(null); // Call OnPointerClick method in RaccoonClickHandler
                    hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
                    PlayButtonClick(); // Play button click sound
                    DeactivateDialoguePanel(5); // Deactivate Raccoon panel
                    ResetDialogueState(); // Reset dialogue state
                }
                else if (snappingTurtlePanelActive && !hasResetDialogueState)
                {
                    //Debug.Log("Snapping Turtle panel is active. Interacting..."); // Debug.Log
                    snappingTurtleClickHandler.OnPointerClick(null); // Call OnPointerClick method in SnappingTurtleClickHandler
                    hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
                    PlayButtonClick(); // Play button click sound
                    DeactivateDialoguePanel(7); // Deactivate Snapping Turtle panel
                    ResetDialogueState(); // Reset dialogue state
                }
                else if (whiteTailedDeerPanelActive && !hasResetDialogueState)
                {
                    //Debug.Log("White-Tailed Deer panel is active. Interacting..."); // Debug.Log
                    whiteTailedDeerClickHandler.OnPointerClick(null); // Call OnPointerClick method in WhiteTailedDeerClickHandler
                    hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
                    PlayButtonClick(); // Play button click sound
                    DeactivateDialoguePanel(2); // Deactivate White-Tailed Deer panel
                    ResetDialogueState(); // Reset dialogue state
                }
            }
            else if (eventZonePanelActive)
            {
                if (fishEventPanelActive)
                {
                    fishEventZonePanelClickHandler.OnPointerClick(null); // Call OnPointerClick method in FishEventZonePanelClickHandler
                    PlayButtonClick(); // Play button click sound
                    fishEventZonePanel.SetActive(false); // Hide fish event zone
                    fishEventPanelActive = false; // Set fish event panel active to false
                }
                else if (birdEventPanelActive)
                {
                    birdEventZonePanelClickHandler.OnPointerClick(null); // Call OnPointerClick method in BirdEventZonePanelClickHandler
                    PlayButtonClick(); // Play button click sound
                    birdEventZonePanel.SetActive(false); // Hide bird event zone
                    birdEventPanelActive = false; // Set bird event panel active to false
                }
                else if (deerEventPanelActive)
                {
                    deerEventZonePanelClickHandler.OnPointerClick(null); // Call OnPointerClick method in DeerEventZonePanelClickHandler
                    PlayButtonClick(); // Play button click sound
                    deerEventZonePanel.SetActive(false); // Hide deer event zone
                    deerEventPanelActive = false; // Set deer event panel active to false
                }
            }
            else if (plantSortingPanelActive)
            {
                plantSortingPanelClickHandler.OnPointerClick(null); // Call OnPointerClick method in PlantSortingPanelClickHandler
                PlayButtonClick(); // Play button click sound
                plantSortingPanel.SetActive(false); // Hide plant sorting panel
                plantSortingPanelActive = false; // Set plant sorting panel active to false
            }
            else if (ChangeablePlant.incorrectPlantSwappedPanelShown && TypewriterUi.isTypingComplete)
            {
                //Debug.Log("Incorrect plant swapped panel is shown. Interacting..."); // Debug.Log
                incorrectPlantSwappedPanelClickHandler.OnPointerClick(null); // Call OnPointerClick method in IncorrectPlantSwappedPanelClickHandler
                PlayButtonClick(); // Play button click sound
            }
            else if (ChangeablePlant.correctPlantSwappedPanelShown && TypewriterUi.isTypingComplete)
            {
                //Debug.Log("Correct plant swapped panel is shown. Interacting..."); // Debug.Log
                correctPlantSwappedPanelClickHandler.OnPointerClick(null); // Call OnPointerClick method in CorrectPlantSwappedPanelClickHandler
                PlayButtonClick(); // Play button click sound
            }
        }

        eKeyWasDownLastFrame = eKeyDown; // Update the state of eKeyWasDownLastFrame
    }

    // Integer properties to manage bradford pears swapped
    public int BradfordPearsSwapped
    {
        get { return bradfordPearsSwapped; } // Getter for bradfordPearsSwapped
        set
        {
            bradfordPearsSwapped = value; // Setter for bradfordPearsSwapped
            UpdateBradfordPearsSwappedCounter(); // Update the bradford pears swapped counter
        }
    }

    // Integer properties to manage purple loosestrifes swapped
    public int PurpleLoosestrifesSwapped
    {
        get { return purpleLoosestrifesSwapped; } // Getter for purpleLoosestrifesSwapped
        set
        {
            purpleLoosestrifesSwapped = value; // Setter for purpleLoosestrifesSwapped
            UpdatePurpleLoosestrifesSwappedCounter(); // Update the purple loosestrifes swapped counter
        }
    }

    // Integer properties to manage plants swapped
    public int PlantsCorrectlySwapped
    {
        get { return plantsCorrectlySwapped; } // Getter for plantsSwapped
        set
        {
            plantsCorrectlySwapped = value; // Setter for plantsSwapped
            UpdatePlantsCorrectlySwappedCounter(); // Update the plants swapped counter
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
            //Debug.Log("Animal clicked..."); // Debug.Log
        }

        // If left mouse button is down and isPressed is true...
        if (Input.GetMouseButtonUp(0) && isPressed)
        {
            isPressed = false; // Set bool isPressed to false
            RaycastScript.eventAnimalClicked = false; // Set bool testTubeClicked to false
        }
    }

    // Method to update bradford pears swapped counter text
    public void UpdateBradfordPearsSwappedCounter()
    {
        bradfordPearsSwappedCounterText.text = $"Bradford Pears Swapped: {bradfordPearsSwapped.ToString()} / 6"; // Update the text to show bradford pears swapped
    }

    // Method to update purple loosestrifes swapped counter text
    public void UpdatePurpleLoosestrifesSwappedCounter()
    {
        purpleLoosestrifesSwappedCounterText.text = $"Purple Loosestrifes Swapped: {purpleLoosestrifesSwapped.ToString()} / 6"; // Update the text to show purple loosestrifes swapped
    }

    // Method to update plants swapped counter text
    public void UpdatePlantsCorrectlySwappedCounter()
    {
        plantsCorrectlySwappedCounterText.text = $"Plants Correctly Swapped: {plantsCorrectlySwapped.ToString()} / 12"; // Update the text to show plants swapped
    }

    // Method to increment progress on click counter
    private void InvokeClickCounter()
    {
        //Debug.Log("Click counted."); // Debug.Log

        if (deerEventActive)
        {
            PlayInvasiveSpeciesDisposedSound(); // Play invasive species disposed sound
            clickCounterScript.IncrementProgress(deerClickIncrement); // Increment progress by variable progressIncrement
        }

        if (birdEventActive)
        {
            PlayInvasiveSpeciesDisposedSound(); // Play invasive species disposed sound
            clickCounterScript.IncrementProgress(birdClickIncrement); // Increment progress by variable progressIncrement
        }

        if (fishEventActive)
        {
            PlayInvasiveSpeciesDisposedSound(); // Play invasive species disposed sound
            clickCounterScript.IncrementProgress(fishClickIncrement); // Increment progress by variable progressIncrement
        }
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
                
                //Debug.Log("Checking for animal clicks."); // Debug.Log
                CheckForAnimalClicks(); // Call method to check for animal clicks in event zones
            }
        }

        // If slider value is equal to variable targetProgress 
        if (slider.value >= targetProgress)
        {
            // If bool isDeerEventEntered is true and bool deerEventZoneComplete is false and bool deerEventActive is true...
            if (DeerEventZone.isDeerEventEntered && !deerEventZoneComplete && deerEventActive)
            {
                deerEventZoneComplete = true; // Set bool deerEventZoneComplete to true
                //Debug.Log("Deer Event Complete."); // Debug.Log
                clickCounter.SetActive(false); // Disable click counter
                deerEventActive = false; // Set bool deerEventActive to false
                deerEventZone.gameObject.SetActive(false); // Disable deer event zone
                deerEventZoneArrow.gameObject.SetActive(false); // Disable deer event waypoint arrow
                deerEventZonePanel.SetActive(true); // Enable deer event zone panel
                deerEventPanelActive = true; // Set bool deerEventPanelActive to true
                eventZonePanelActive = true; // Set bool eventZonePanelActive to true
                playerInput.controlsLocked = true; // Lock player controls
                deerEventZoneText.text = "Great Job! You dispersed that group of deer! Now native plants can grow!"; // Set text for deer event zone
                DeerEventZone.isDeerEventEntered = false; // Set bool isDeerEventEntered to false
                slider.value = 0f; // Reset slider value to 0
                CancelInvoke(); // Cancel any ongoing invocations

                eventsObjectivesPanel.SetActive(false); // Hide events objectives panel
                upperBankObjective.SetActive(false); // Hide upper bank objective
            }

            // If bool isBirdEventEntered is true and bool birdEventZoneComplete is false and bool birdEventActive is true...
            if (BirdEventZone.isBirdEventEntered && !birdEventZoneComplete && birdEventActive)
            {
                birdEventZoneComplete = true; // Set bool birdEventZoneComplete to true
                //Debug.Log("Bird Event Complete."); // Debug.Log
                clickCounter.SetActive(false); // Disable click counter
                birdEventActive = false; // Set bool birdEventActive to false
                birdEventZone.gameObject.SetActive(false); // Disable bird event zone
                birdEventZoneArrow.gameObject.SetActive(false); // Disable bird event waypoint arrow
                birdEventZonePanel.SetActive(true); // Enable bird event zone panel
                birdEventPanelActive = true; // Set bool birdEventPanelActive to true
                eventZonePanelActive = true; // Set bool eventZonePanelActive to true
                playerInput.controlsLocked = true; // Lock player controls
                birdEventZoneText.text = "Great Job! The starlings have flown away! Now native birds can repopulate!"; // Set text for bird event zone
                BirdEventZone.isBirdEventEntered = false; // Set bool isBirdEventEntered to false 
                slider.value = 0f; // Reset slider value to 0
                CancelInvoke(); // Cancel any ongoing invocations

                eventsObjectivesPanel.SetActive(false); // Hide events objectives panel
                midBankObjective.SetActive(false); // Hide mid bank objective
            }

            // If bool isFishEventEntered is true and bool fishEventZoneComplete is false and bool fishEventActive is true...
            if (FishEventZone.isFishEventEntered && !fishEventZoneComplete && fishEventActive)
            {
                fishEventZoneComplete = true; // Set bool fishEventZoneComplete to true
                //Debug.Log("Fish Event Complete."); // Debug.Log
                clickCounter.SetActive(false); // Disable click counter
                fishEventActive = false; // Set bool fishEventActive to false
                fishEventZone.gameObject.SetActive(false); // Disable fish event zone
                fishEventZoneArrow.gameObject.SetActive(false); // Disable fish event waypoint arrow
                fishEventZonePanel.SetActive(true); // Enable fish event zone panel
                fishEventPanelActive = true; // Set bool fishEventPanelActive to true
                eventZonePanelActive = true; // Set bool eventZonePanelActive to true
                playerInput.controlsLocked = true; // Lock player controls
                fishEventZoneText.text = "Great Job! You caught the Asian Carp! Now Now the native fish are safe!"; // Set text for fish event zone
                FishEventZone.isFishEventEntered = false; // Set bool isFishEventEntered to false
                slider.value = 0f; // Reset slider value to 0
                CancelInvoke(); // Cancel any ongoing invocations

                eventsObjectivesPanel.SetActive(false); // Hide events objectives panel
                lowerBankObjective.SetActive(false); // Hide lower bank objective
            }
        }
    }

    // Method to disable all text objects
    public void DisableObjectives()
    {
        objectiveText1.gameObject.SetActive(false); //hide objective text 1
        objectiveText2.gameObject.SetActive(false); //hide objective text 2
        objectiveText3.gameObject.SetActive(false); //hide objective text 3
        objectiveSubtext1.gameObject.SetActive(false); //hide objective subtext 1
        objectiveSubtext2.gameObject.SetActive(false); //hide objective subtext 2
        objectiveSubtext3.gameObject.SetActive(false); //hide objective subtext 3
        objectiveSubtext4.gameObject.SetActive(false); //hide objective subtext 4
        objectiveSubtext5.gameObject.SetActive(false); //hide objective subtext 5
        objectiveSubtext6.gameObject.SetActive(false); //hide objective subtext 6
        objectiveSubtext7.gameObject.SetActive(false); //hide objective subtext 7
        objectiveSubtext8.gameObject.SetActive(false); //hide objective subtext 8
        objectiveSubtext9.gameObject.SetActive(false); //hide objective subtext 9
        objectiveSubtext10.gameObject.SetActive(false); //hide objective subtext 10
        objectiveSubtext11.gameObject.SetActive(false); //hide objective subtext 11
        objectiveSubtext12.gameObject.SetActive(false); //hide objective subtext 12
        objectiveSubtext13.gameObject.SetActive(false); //hide objective subtext 13
        objectiveSubtext14.gameObject.SetActive(false); //hide objective subtext 14
        objectiveSubtext15.gameObject.SetActive(false); //hide objective subtext 15
        eventsObjectivesPanel.SetActive(false); // Hide events objectives panel
        lowerBankObjective.SetActive(false); // Hide lower bank objective
        midBankObjective.SetActive(false); // Hide mid bank objective
        upperBankObjective.SetActive(false); // Hide upper bank objective
    }

    public void EnableDeerEventZone()
    {
        if (upperBankObjectivesComplete && !deerEventZoneComplete)
        {
            deerEventZone.gameObject.SetActive(true); // Enable deer event zone
            deerEventZoneArrow.gameObject.SetActive(true); // Enable deer event waypoint arrow
            groupOfDeer.SetActive(true); // Enable group of deer
            //Debug.Log("Deer Event Zone Enabled."); // Debug.Log
        }
    }

    public void EnableBirdEventZone()
    {
        if (midBankObjectivesComplete && !birdEventZoneComplete)
        {
            birdEventZone.gameObject.SetActive(true); // Enable bird event zone
            birdEventZoneArrow.gameObject.SetActive(true); // Enable bird event waypoint arrow
            groupOfStarlings.SetActive(true); // Enable group of starlings
            //Debug.Log("Bird Event Zone Enabled."); // Debug.Log
        }
    }

    public void EnableFishEventZone()
    {
        if (lowerBankObjectivesComplete && !fishEventZoneComplete)
        {
            fishEventZone.gameObject.SetActive(true); // Enable fish event zone
            fishEventZoneArrow.gameObject.SetActive(true); // Enable fish event waypoint arrow
            //Debug.Log("Fish Event Zone Enabled."); // Debug.Log
        }
    }

    // Method to disable event zones
    public void DisableEventZones()
    {
        deerEventZone.gameObject.SetActive(false); // Disable deer event zone
        birdEventZone.gameObject.SetActive(false); // Disable bird event zone
        fishEventZone.gameObject.SetActive(false); // Disable fish event zone
        deerEventZoneArrow.gameObject.SetActive(false); // Disable deer event waypoint arrow
        birdEventZoneArrow.gameObject.SetActive(false); // Disable bird event waypoint arrow
        fishEventZoneArrow.gameObject.SetActive(false); // Disable fish event waypoint arrow
        groupOfDeer.SetActive(false); // Disable group of deer
        groupOfStarlings.SetActive(false); // Disable group of starlings
    }

    // Method to activate specific panel in index
    public void ActivateDialoguePanel(int dialoguePanelIndex)
    {
        // Check if the dialoguePanelIndex is within the bounds of the dialoguePanels array
        if (dialoguePanelIndex >= 0 && dialoguePanelIndex < dialoguePanels.Length)
        {
            dialoguePanels[dialoguePanelIndex].SetActive(true); // Activate the specified panel
            dialogueIsActive = true; // Set the active panel flag to true
            canClosePanel = false; // Set canClosePanel to false to prevent instant closing of panel
            playerInput.controlsLocked = true; // Lock player controls
            hasResetDialogueState = false; // Reset the flag for dialogue state
            Invoke("EnableDialoguePanelClose", dialogueStateTimer); // Invoke EnableDialoguePanelClose after a delay
            //Debug.Log("Panel " + dialoguePanelIndex + " activated."); // Debug.Log
        }
        else
        {
            //Debug.LogError("Invalid panel index: " + dialoguePanelIndex); // Log an error if the index is invalid
        }
    }

    private void EnableDialoguePanelClose()
    {
        canClosePanel = true;
    }

    // Method to deactivate a specific panel
    public void DeactivateDialoguePanel(int dialoguePanelIndex)
    {
        dialoguePanels[dialoguePanelIndex].SetActive(false); // Activate the specified panel
        playerInput.controlsLocked = false; // unlock player controls
        dialogueIsActive = false;
        hasResetDialogueState = false; // Reset the flag for dialogue state
        dialoguePanelCooldown = dialoguePanelCooldownDuration; // Start cooldown
        waitForNextEPress = true; // Require a new E press before opening another panel
        PlayButtonClick(); // Play button click sound
        //Debug.Log("Panel " + dialoguePanelIndex + " deactivated."); // Debug.Log
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
        playerInput.controlsLocked = false; // unlock player controls
    }

    // Method to Activate event zone panels
    public void ActivateEventPanel(int eventPanelIndex)
    {
        eventPanels[eventPanelIndex].SetActive(true); // Activate the specified event panel
        eventZonePanelActive = true; // Set the event zone panel active flag to true
        playerInput.controlsLocked = true; // Lock player controls
    }

    // Method to deactivate all event panels
    public void DeactivateAllEventZonePanels()
    {
        deerEventZonePanel.SetActive(false); // Deactivate deer event zone panel
        birdEventZonePanel.SetActive(false); // Deactivate bird event zone panel
        fishEventZonePanel.SetActive(false); // Deactivate fish event zone panel
    }

    // Method to deactivate all plant sorting panels
    public void DeactivatePlantSortingPanels()
    {
        bradfordPearTreePanel.SetActive(false); // Deactivate Bradford Pear Tree panel
        purpleLoosestrifePanel.SetActive(false); // Deactivate Purple Loosestrife panel
    }

    // Method to reset dialogue state
    public void ResetDialogueState()
    {
        //Debug.Log("Resetting dialogue state..."); // Debug.Log
        hasResetDialogueState = false; // Reset the flag to allow future dialogue interactions
    }

    // Method to reset event zone panel state
    public void ResetEventPanelState()
    {
        //Debug.Log("Resetting event panel state..."); // Debug.Log
        DeactivateAllEventZonePanels(); // Deactivate all event zone panels
        hasResetEventPanelState = false; // Reset the flag for event panel state
    }

    // Method to reset plant sorting panel state
    public void ResetPlantSortingPanelState()
    {
        //Debug.Log("Resetting plant sorting panel state..."); // Debug.Log
        plantSortingPanel.SetActive(false); // Hide plant sorting panel
        //hasResetPlantSortingPanelState = false; // Reset the flag for plant sorting panel
    }

    // Method to return to the main scene
    public void ReturnButton()
    {
        PlayButtonClick(); // Play button click sound
        playerInput.controlsLocked = false; // Lock player controls
        Time.timeScale = 1;
        SceneManager.LoadScene("Overworld"); //load main scene
    }

    private void AnimalClicked()
    {
        // If dialogue is active and dialogue panel cooldown is greater than 0...
        if (dialoguePanelCooldown > 0f || waitForNextEPress)
        {
            return; // Return if dialogue panel cooldown is active
        }

        // If eastern starling is clicked and dialogue is not active...
        if (ConservationInteractionObject.easternStarlingClicked && !dialogueIsActive)
        {
            EasternStarlingClicked(); // Handle eastern starling click
        }

        // If white-tailed deer is clicked and dialogue is not active...
        if (ConservationInteractionObject.whiteTailedDeerClicked && !dialogueIsActive)
        {
            WhiteTailedDeerClicked(); // Handle white-tailed deer click
        }

        // If banded pennant dragonfly is clicked and dialogue is not active...
        if (ConservationInteractionObject.bandedPennantDragonflyClicked && !dialogueIsActive)
        {
            BandedPennantDragonflyClicked(); // Handle banded pennant dragonfly click
        }

        // If common garter snake is clicked and dialogue is not active...
        if (ConservationInteractionObject.garterSnakeClicked && !dialogueIsActive)
        {
            CommonGarterSnakeClicked(); // Handle common garter snake click
        }

        // If bald eagle is clicked and dialogue is not active...
        if (ConservationInteractionObject.baldEagleClicked && !dialogueIsActive)
        {
            BaldEagleClicked(); // Handle bald eagle click
        }

        // If painted lady butterfly is clicked and dialogue is not active...
        if (ConservationInteractionObject.paintedLadyButterflyClicked && !dialogueIsActive)
        {
            PaintedLadyButterflyClicked();
        }

        // If muskrat is clicked and dialogue is not active...
        if (ConservationInteractionObject.muskratClicked && !dialogueIsActive) 
        {
            MuskratClicked();
        }

        // If snapping turtle is clicked and dialogue is not active...
        if (ConservationInteractionObject.snappingTurtleClicked && !dialogueIsActive)
        {
            SnappingTurtleClicked();
        }

        // If beaver is clicked and dialogue is not active...
        if (ConservationInteractionObject.beaverClicked && !dialogueIsActive) 
        {
            BeaverClicked();
        }

        // If raccoon is clicked and dialogue is not active...
        if (ConservationInteractionObject.raccoonClicked && !dialogueIsActive) 
        {
            RaccoonClicked();
        }

        // If northern map turtle is clicked and dialogue is not active...
        if (ConservationInteractionObject.northernMapTurtleClicked && !dialogueIsActive) 
        {
            NorthernMapTurtleClicked();
        }

        // If asian carp is clicked and dialogue is not active...
        if (ConservationInteractionObject.asianCarpClicked && !dialogueIsActive) 
        {
            AsianCarpClicked();
        }
    }

    // Method to check if event panell is clicked
    private void EventPanelClicked()
    {
        // If bool isEventZonePanelClicked is true and hasResetEventPanelState is false...
        if (EventZonePanelClickHandler.isEventZonePanelClicked && !hasResetEventPanelState)
        {
            ResetEventPanelState(); // Reset the event panel state
            hasResetEventPanelState = true; // Set the flag to true to prevent multiple calls
        }
    }

    // Method toshow plant sorting panel
    private void ShowPlantSortingPanel()
    {
        // If bool eventZonesComplete is true and bool eventZonePanelActive is false and bool objectivesShown is true and bool plantSortingPanelActive is false...
        if (eventZonesComplete && !eventZonePanelActive && objectivesShown && !plantSortingPanelActive && startPlantSorting)
        {
            objectivesPanel.SetActive(true); // show objectives panel
            plantSortingPanel.SetActive(true); // Show plant sorting panel
            plantSortingPanelActive = true; // Set the flag to true to indicate the panel is active
            startPlantSorting = false; // Set the flag to false to prevent multiple calls
            conservationInteractionObjectScript.UpperBankObjectiveObjectsNotReady(); // Call method to set upper bank objective objects not ready
            conservationInteractionObjectScript.PlantSortingObjectiveObjectsReady(); // Call method to set plant sorting objective objects ready
            //playerInput.controlsLocked = true; // Lock player controls
        }
    }

    // Method to check if plant sorting panel is clicked
    private void PlantSortingPanelClicked()
    {
        // If bool isPlantSortingPanelClicked is true and hasResetPlantSortingPanelState is false...
        if (PlantSortingPanelClickHandler.isPlantSortingPanelClicked && !hasResetPlantSortingPanelState)
        {
            ResetPlantSortingPanelState(); // Reset the plant sorting panel state
            hasResetPlantSortingPanelState = true; // Set the flag to true to prevent multiple calls
        }
    }

    // Handle clicks on eastern starling
    private void EasternStarlingClicked()
    {
        //PlayAnimalClickedSound(); // Play animal clicked sound
        ActivateDialoguePanel(1); // Activate the eastern starling dialogue panel
        ConservationInteractionObject.easternStarlingClicked = false; // Reset the click handler for eastern starling
        easternStarlingPanelActive = true; // Set the flag for eastern starling panel active
        isEasternStarlingFound = true; // Set the flag for eastern starling found
        animalNames.Remove("Eastern Starling"); // Remove eastern starling from the list of animal names
    }

    // Handle clicks on white-tailed deer
    private void WhiteTailedDeerClicked()
    {
        //PlayAnimalClickedSound(); // Play animal clicked sound
        ActivateDialoguePanel(2); // Activate the white-tailed deer dialogue panel
        ConservationInteractionObject.whiteTailedDeerClicked = false; // Reset the click handler for white-tailed deer
        whiteTailedDeerPanelActive = true; // Set the flag for white-tailed deer panel active
        isWhiteTailedDeerFound = true; // Set the flag for white-tailed deer found
        animalNames.Remove("White-Tailed Deer"); // Remove white-tailed deer from the list of animal names
    }

    // Handle clicks on banded pennant dragonfly
    private void BandedPennantDragonflyClicked()
    {
        //PlayAnimalClickedSound(); // Play animal clicked sound
        ActivateDialoguePanel(10); // Activate the banded pennant dragonfly dialogue panel
        ConservationInteractionObject.bandedPennantDragonflyClicked = false; // Reset the click handler for banded pennant dragonfly
        bandedPennantDragonflyPanelActive = true; // Set the flag for banded pennant dragonfly panel active
        isBandedPennantDragonflyFound = true; // Set the flag for banded pennant dragonfly found
        animalNames.Remove("Banded Pennant Dragonfly"); // Remove banded pennant dragonfly from the list of animal names
    }

    // Handle clicks on garter snake
    private void CommonGarterSnakeClicked()
    {
        //PlayAnimalClickedSound(); // Play animal clicked sound
        ActivateDialoguePanel(8); // Activate the common garter snake dialogue panel
        ConservationInteractionObject.garterSnakeClicked = false; // Reset the click handler for common garter snake
        garterSnakePanelActive = true; // Set the flag for common garter snake panel active
        isCommonGarterSnakeFound = true; // Set the flag for common garter snake found
        animalNames.Remove("Common Garter Snake"); // Remove common garter snake from the list of animal names
    }

    // Handle clicks on bald eagle
    private void BaldEagleClicked()
    {
        //PlayAnimalClickedSound(); // Play animal clicked sound
        ActivateDialoguePanel(3); // Activate the bald eagle dialogue panel
        ConservationInteractionObject.baldEagleClicked = false; // Reset the click handler for bald eagle
        baldEaglePanelActive = true; // Set the flag for bald eagle panel active
        isBaldEagleFound = true; // Set the flag for bald eagle found
        animalNames.Remove("Bald Eagle"); // Remove bald eagle from the list of animal names
    }

    // Handle clicks on muskrat
    private void MuskratClicked()
    {
        //PlayAnimalClickedSound(); // Play animal clicked sound
        ActivateDialoguePanel(6); // Activate the muskrat dialogue panel
        ConservationInteractionObject.muskratClicked = false; // Reset the click handler for muskrat
        muskratPanelActive = true; // Set the flag for muskrat panel active
        isMuskratFound = true; // Set the flag for muskrat found
        animalNames.Remove("Muskrat"); // Remove muskrat from the list of animal names
    }

    // Handle clicks on snapping turtle
    private void SnappingTurtleClicked()
    {
        //PlayAnimalClickedSound(); // Play animal clicked sound
        ActivateDialoguePanel(7); // Activate the snapping turtle dialogue panel
        ConservationInteractionObject.snappingTurtleClicked = false; // Reset the click handler for snapping turtle
        snappingTurtlePanelActive = true; // Set the flag for snapping turtle panel active
        isSnappingTurtleFound = true; // Set the flag for snapping turtle found
        animalNames.Remove("Snapping Turtle"); // Remove snapping turtle from the list of animal names
    }

    // Handle clicks on beaver
    private void BeaverClicked()
    {
        //PlayAnimalClickedSound(); // Play animal clicked sound
        ActivateDialoguePanel(4); // Activate the beaver dialogue panel
        ConservationInteractionObject.beaverClicked = false; // Reset the click handler for beaver
        beaverPanelActive = true; // Set the flag for beaver panel active
        isBeaverFound = true; // Set the flag for beaver found
        animalNames.Remove("Beaver"); // Remove beaver from the list of animal names
    }

    // Handle clicks on raccoon
    private void RaccoonClicked()
    {
        //PlayAnimalClickedSound(); // Play animal clicked sound
        ActivateDialoguePanel(5); // Activate the raccoon dialogue panel
        ConservationInteractionObject.raccoonClicked = false; // Reset the click handler for raccoon
        raccoonPanelActive = true; // Set the flag for raccoon panel active
        isRaccoonFound = true; // Set the flag for raccoon found
        animalNames.Remove("Raccoon"); // Remove raccoon from the list of animal names
    }

    // Handle clicks on northern map turtle
    private void NorthernMapTurtleClicked()
    {
        //PlayAnimalClickedSound(); // Play animal clicked sound
        ActivateDialoguePanel(9); // Activate the northern map turtle dialogue panel
        ConservationInteractionObject.northernMapTurtleClicked = false; // Reset the click handler for northern map turtle
        northernMapTurtlePanelActive = true; // Set the flag for northern map turtle panel active
        isNorthernMapTurtleFound = true; // Set the flag for northern map turtle found
        animalNames.Remove("Northern Map Turtle"); // Remove northern map turtle from the list of animal names
    }

    // Handle clicks on asian carp
    private void AsianCarpClicked()
    {
        //PlayAnimalClickedSound(); // Play animal clicked sound
        ActivateDialoguePanel(0); // Activate the asian carp dialogue panel
        ConservationInteractionObject.asianCarpClicked = false; // Reset the click handler for asian carp
        asianCarpPanelActive = true; // Set the flag for asian carp panel active
        isAsianCarpFound = true; // Set the flag for asian carp found
        animalNames.Remove("Asian Carp"); // Remove asian carp from the list of animal names
    }

    // Handle clicks on painted lady butterfly
    private void PaintedLadyButterflyClicked()
    {
        //PlayAnimalClickedSound(); // Play animal clicked sound
        ActivateDialoguePanel(11); // Activate the painted lady butterfly dialogue panel
        ConservationInteractionObject.paintedLadyButterflyClicked = false; // Reset the click handler for painted lady butterfly
        paintedLadyButterflyPanelActive = true; // Set the flag for painted lady butterfly panel active
        isPaintedLadyButterflyFound = true; // Set the flag for painted lady butterfly found
        animalNames.Remove("Painted Lady Butterfly"); // Remove painted lady butterfly from the list of animal names
    }

    // Set subtext for objective 1
    private void LowerBankObjectives()
    {
        lowerBankObjectivesActive = true; //set bool lowerBankObjectivesActive to true
        //Debug.Log("Player is exploring the lower bank."); //Debug.Log
        objectiveText1.gameObject.SetActive(true); //show objective text 1
        objectiveSubtext1.gameObject.SetActive(true); //show objective subtext 1
        objectiveSubtext1.text = "Find and click on:"; //set objective subtext 1 text
        objectiveSubtext2.gameObject.SetActive(true); //show objective subtext 2
        objectiveSubtext2.text = "Snapping Turtle"; //set objective subtext 2 text
        objectiveSubtext3.gameObject.SetActive(true); //show objective subtext 3
        objectiveSubtext3.text = "Garter Snake"; //set objective subtext 3 text
        objectiveSubtext4.gameObject.SetActive(true); //show objective subtext 4
        objectiveSubtext4.text = "Northern Map Turtle"; //set objective subtext 4 text
        objectiveSubtext5.gameObject.SetActive(true); //show objective subtext 5
        objectiveSubtext5.text = "Asian Carp"; //set objective subtext 5 text
        objectiveSubtext6.gameObject.SetActive(true); //show objective subtext 6
        objectiveSubtext6.text = "Beaver"; //set objective subtext 6 text
    }

    private void LowerBankObjectivesComplete()
    {
        if (isCommonGarterSnakeFound && isSnappingTurtleFound && isNorthernMapTurtleFound && isAsianCarpFound && isBeaverFound)
        {
            //Debug.Log("Lower Bank objectives complete!"); //Debug.Log
            lowerBankObjectivesComplete = true; //set bool lowerBankObjectivesComplete to true
            lowerBankObjectivesActive = false; //set bool lowerBankObjectivesActive to false
            objectivesPanel.SetActive(false); //hide objectives panel
            objectiveText1.gameObject.SetActive(false); //hide objective text 1
            objectiveSubtext1.gameObject.SetActive(false); //hide objective subtext 1
            objectiveSubtext2.gameObject.SetActive(false); //hide objective subtext 2
            objectiveSubtext3.gameObject.SetActive(false); //hide objective subtext 3
            objectiveSubtext4.gameObject.SetActive(false); //hide objective subtext 4
            objectiveSubtext5.gameObject.SetActive(false); //hide objective subtext 5
            objectiveSubtext6.gameObject.SetActive(false); //hide objective subtext 6
            LowerBankEventObjective(); //call method to set lower bank event objective
        }
    }

    // Set subtext for objective 2
    private void MidBankObjectives()
    {
        midBankObjectivesActive = true; //set bool midBankObjectivesActive to true
        //Debug.Log("Player is exploring the Mid Bank!"); //Debug.Log
        objectivesPanel.SetActive(true); //show objectives panel
        objectiveText2.gameObject.SetActive(true); //show objective text 2
        objectiveSubtext7.gameObject.SetActive(true); //show objective subtext 7
        objectiveSubtext7.text = "Find and click on:"; //set objective subtext 7 text
        objectiveSubtext8.gameObject.SetActive(true); //show objective subtext 8
        objectiveSubtext8.text = "European Starling"; //set objective subtext 8 text
        objectiveSubtext9.gameObject.SetActive(true); //show objective subtext 9
        objectiveSubtext9.text = "Banded Pennant Dragonfly"; //set objective subtext 9 text
        objectiveSubtext10.gameObject.SetActive(true); //show objective subtext 10
        objectiveSubtext10.text = "Muskrat"; //set objective subtext 10 text
    }

    private void MidBankObjectivesComplete()
    {
        if (isEasternStarlingFound && isBandedPennantDragonflyFound && isMuskratFound)
        {
            //Debug.Log("Mid Bank objectives complete!"); //Debug.Log
            midBankObjectivesComplete = true; //set bool midBankObjectivesComplete to true
            midBankObjectivesActive = false; //set bool midBankObjectivesActive to false
            objectivesPanel.SetActive(false); //hide objectives panel
            objectiveText2.gameObject.SetActive(false); //hide objective text 2
            objectiveSubtext7.gameObject.SetActive(false); //hide objective subtext 7
            objectiveSubtext8.gameObject.SetActive(false); //hide objective subtext 8
            objectiveSubtext9.gameObject.SetActive(false); //hide objective subtext 9
            objectiveSubtext10.gameObject.SetActive(false); //hide objective subtext 10
            MidBankEventObjective(); //call method to set mid bank event objective
        }
    }

    // Set subtext for objective 3
    private void UpperBankObjectives()
    {
        upperBankObjectivesActive = true; //set bool upperBankObjectivesActive to true
        //Debug.Log("Player is exploring the Upper Bank!"); //Debug.Log
        objectivesPanel.SetActive(true); //show objectives panel
        objectiveText3.gameObject.SetActive(true); //show objective text 3
        objectiveSubtext11.gameObject.SetActive(true); //show objective subtext 11
        objectiveSubtext11.text = "Find and click on:"; //set objective subtext 11 text
        objectiveSubtext12.gameObject.SetActive(true); //show objective subtext 12
        objectiveSubtext12.text = "White-Tailed Deer"; //set objective subtext 12 text
        objectiveSubtext13.gameObject.SetActive(true); //show objective subtext 13
        objectiveSubtext13.text = "Painted Lady Butterfly"; //set objective subtext 13 text
        objectiveSubtext14.gameObject.SetActive(true); //show objective subtext 14
        objectiveSubtext14.text = "Raccoon"; //set objective subtext 14 text
        objectiveSubtext15.gameObject.SetActive(true); //show objective subtext 15
        objectiveSubtext15.text = "Bald Eagle"; //set objective subtext 15 text
    }

    private void UpperBankObjectivesComplete()
    {
        if (isWhiteTailedDeerFound && isPaintedLadyButterflyFound && isRaccoonFound && isBaldEagleFound)
        {
            //Debug.Log("Upper Bank objectives complete!"); //Debug.Log
            upperBankObjectivesComplete = true; //set bool upperBankObjectivesComplete to true
            upperBankObjectivesActive = false; //set bool upperBankObjectivesActive to false
            objectivesPanel.SetActive(false); //hide objectives panel
            objectiveText3.gameObject.SetActive(false); //hide objective text 3
            objectiveSubtext11.gameObject.SetActive(false); //hide objective subtext 11
            objectiveSubtext12.gameObject.SetActive(false); //hide objective subtext 12
            objectiveSubtext13.gameObject.SetActive(false); //hide objective subtext 13
            objectiveSubtext14.gameObject.SetActive(false); //hide objective subtext 14
            objectiveSubtext15.gameObject.SetActive(false); //hide objective subtext 15
            UpperBankEventObjective(); //call method to set upper bank event objective
        }
    }

    // Set subtext for event objectives
    private void LowerBankEventObjective()
    {
        if (!fishEventZoneComplete)
        {
            //Debug.Log("Player is completing the lower bank event objective!"); //Debug.Log
            eventsObjectivesPanel.SetActive(true); // Show event objectives panel
            lowerBankObjective.SetActive(true); // Show lower bank objective
            eventObjectiveSubtext1.gameObject.SetActive(true); //show objective subtext 1
            eventObjectiveSubtext1.text = "Investigate the fish."; //set objective subtext 1 text
        }
    }

    private void MidBankEventObjective()
    {
        if (!birdEventZoneComplete)
        {
            //Debug.Log("Player is completing the mid bank event objective!"); //Debug.Log
            eventsObjectivesPanel.SetActive(true); // Show event objectives panel
            midBankObjective.SetActive(true); // Show mid bank objective
            eventObjectiveSubtext2.gameObject.SetActive(true); //show objective subtext 2
            eventObjectiveSubtext2.text = "Investigate the birds."; //set objective subtext 2 text
        }     
    }

    private void UpperBankEventObjective()
    {
        if (!deerEventZoneComplete)
        {
            //Debug.Log("Player is completing the upper bank event objective!"); //Debug.Log
            eventsObjectivesPanel.SetActive(true); // Show event objectives panel
            upperBankObjective.SetActive(true); // Show upper bank objective
            eventObjectiveSubtext3.gameObject.SetActive(true); //show objective subtext 3
            eventObjectiveSubtext3.text = "Investigate the deer."; //set objective subtext 3 text
        }    
    }

    //  Method to called when deer event zone is entered
    public void DeerEventZoneEntered()
    {
        // If bool deerEventTriggered  is true and bool deerEventZoneComplete is fale
        if (DeerEventZone.deerEventTriggered && !deerEventZoneComplete && !deerEventActive && !dialogueIsActive)
        {
            //Debug.Log("Deer event zone condition met. Triggering DeerEventZone."); // Debug.Log

            //Debug.Log("Start deer event."); // Debug.Log
            deerEventZonePanel.SetActive(true); // Show event zone panel
            deerEventPanelActive = true; // Set bool deer event zone panel active
            eventZonePanelActive = true; // Set bool event zone panel active
            playerInput.controlsLocked = true; // Lock player controls
            deerEventZoneText.text = "Oh no! There are too many White-Tailed deer here. Look at how they have destroyed the plants. Click to drive them off."; // Set event zone text
            deerEventActive = true; // Set deer event active
            deerEventObjectiveSet = true; // Set bool deerEventObjectiveSet to true
        }
    }

    // Method to called when bird event zone is entered
    public void BirdEventZoneEntered()
    {
        // If bool birdEventTriggered and bool BirdEventZoneComplete is false...
        if (BirdEventZone.birdEventTriggered && !birdEventZoneComplete && !birdEventActive && !dialogueIsActive)
        {
            //Debug.Log("Bird event zone condition met. Triggering BirdEventZone."); // Debug.Log

            //Debug.Log("Start bird event."); // Debug.Log
            birdEventZonePanel.SetActive(true); // Show event zone panel
            birdEventPanelActive = true; // Set bool bird event zone panel active
            eventZonePanelActive = true; // Set bool event zone panel active
            playerInput.controlsLocked = true; // Lock player controls
            birdEventZoneText.text = "Check out those European Starling. They seem to have taken over those trees driving away native birds. Hurry! Click to shoo them away."; // Set event zone text
            birdEventActive = true; // Set bird event active
            birdEventObjectiveSet = true; // Set bool birdEventObjectiveSet to true
        }
    }

    // Method to called when fish event zone is entered
    public void FishEventZoneEntered()
    {
        // If bool fishEventTriggered is true and fishEventComplete is false...
        if (FishEventZone.fishEventTriggered && !fishEventZoneComplete && !fishEventActive && !dialogueIsActive)
        {
            //Debug.Log("Fish event zone condition met. Triggering FishEventZone."); // Debug.Log

            //Debug.Log("Start fish event."); // Debug.Log
            fishEventZonePanel.SetActive(true); // Show event zone panel
            fishEventPanelActive = true; // Set bool fish event zone panel active
            eventZonePanelActive = true; // Set bool event zone panel active
            playerInput.controlsLocked = true; // Lock player controls
            fishEventZoneText.text = "Look at the river. There seems to be a disturbance. Large Asian Carp are attacking the native fish. We must relocate them. Quickly click on them to catch them."; // Set event zone text
            fishEventActive = true; // Set fish event active
            fishEventObjectiveSet = true; // Set bool fishEventObjectiveSet
        }
    }

    // Method to handle Bradford Pear Tree click
    public void BradfordPearTreeClicked(ChangeablePlant clickedPlant)
    {
        //Debug.Log($"Bradford Pear Tree Clicked: {clickedPlant.plantID}"); // Debug.Log
        wasBradfordPearTreeClicked = true; // Set bool wasBradfordPearTreeClicked to true
        bradfordPearTreePanel.SetActive(true); // Show the Bradford Pear Tree panel
        bradfordPearTreePanelActive = true; // Set bool bradfordPearTreePanelActive to true
        playerInput.controlsLocked = true; // Lock player controls
        changeablePlant = clickedPlant; // Assign the clicked plant to changeablePlant
    }

    // Method to handle Purple Loosestrife click
    public void PurpleLoosestrifeClicked(ChangeablePlant clickedPlant)
    {
        //Debug.Log($"Purple Loosestrife Clicked: {clickedPlant.plantID}"); // Debug.Log
        wasPurpleLoosestrifeClicked = true; // Set bool wasPurpleLoosestrifeClicked to true
        purpleLoosestrifePanel.SetActive(true); // Show the Purple Loosestrife panel
        purpleLoosestrifePanelActive = true; // Set bool purpleLoosestrifePanelActive to true
        playerInput.controlsLocked = true; // Lock player controls
        replaceWithAmericanLotusButton.SetActive(true); // Show the replace with American Lotus button
        replaceWithCordgrassButton.SetActive(true); // Show the replace with Cordgrass button
        replaceWithSwampMilkweedButton.SetActive(true); // Show the replace with Swamp Milkweed button
        replaceWithYellowConeflowerButton.SetActive(true); // Show the replace with Yellow Coneflower button
        changeablePlant = clickedPlant; // Assign the clicked plant to changeablePlant
    }

    // Method to replace Bradford Pear Tree with Sycamore Tree
    public void ReplaceWithSycamoreButtonClicked()
    {
        //Debug.Log($"Swapping plant: {changeablePlant.plantID}"); // Debug.Log
        wasBradfordPearTreeClicked = false; // Set bool wasBradfordPearTreeClicked to false
        bradfordPearTreePanel.SetActive(false); // Hide the Bradford Pear Tree panel
        bradfordPearTreePanelActive = false; // Set bool bradfordPearTreePanelActive to false
        playerInput.controlsLocked = false; // unlock player controls
        replaceWithSycamoreButton.SetActive(false); // Hide the replace with sycamore button
        replaceWithBoxElderButton.SetActive(false); // Hide the replace with box elder button
        wasReplaceWithSycamoreButtonClicked = true; // Set bool wasReplaceWithSycamoreButtonClicked to true
        Invoke("ResetTreeButtonsState", 2f); ; // Call ResetTreeButtonsState 
        changeablePlant.SwapPlants(); // Call the SwapPlants method to swap the plant to Sycamore Tree

    }

    // Method to replace Bradford Pear Tree with Box Elder Tree
    public void ReplaceWithBoxElderButtonClicked()
    {
        //Debug.Log($"Swapping plant: {changeablePlant.plantID}"); // Debug.Log
        wasBradfordPearTreeClicked = false; // Set bool wasBradfordPearTreeClicked to false
        bradfordPearTreePanel.SetActive(false); // Hide the Bradford Pear Tree panel
        bradfordPearTreePanelActive = false; // Set bool bradfordPearTreePanelActive to false
        playerInput.controlsLocked = false; // unlock player controls
        replaceWithSycamoreButton.SetActive(false); // Hide the replace with sycamore button
        replaceWithBoxElderButton.SetActive(false); // Hide the replace with box elder button
        wasReplaceWithBoxElderButtonClicked = true; // Set bool wasReplaceWithBoxElderButtonClicked to true
        Invoke("ResetTreeButtonsState", 2f); ; // Call ResetTreeButtonsState 
        changeablePlant.SwapPlants(); // Call the SwapPlants method to swap the plant to Box Elder Tree

    }

    // Method to replace Purple Loosestrife with American Lotus
    public void ReplaceWithAmericanLotusButtonClicked()
    {
        purpleLoosestrifePanel.SetActive(false); // Hide the Purple Loosestrife panel
        purpleLoosestrifePanelActive = false; // Set bool purpleLoosestrifePanelActive to false
        playerInput.controlsLocked = false; // unlock player controls
        replaceWithAmericanLotusButton.SetActive(false); // Hide the replace with American Lotus button
        replaceWithCordgrassButton.SetActive(false); // Hide the replace with Cordgrass button
        replaceWithSwampMilkweedButton.SetActive(false); // Hide the replace with Swamp Milkweed button
        replaceWithYellowConeflowerButton.SetActive(false); // Hide the replace with Yellow Coneflower button
        wasReplaceWithAmericanLotusButtonClicked = true; // Set bool wasReplaceWithAmericanLotusButtonClicked to true
        Invoke("ResetPlantButtonsState", 2f); ; // Call ResetPlantButtonsState
        changeablePlant.SwapPlants(); // Call the SwapPlants method to swap the plant to American Lotus
    }

    // Method to replace Purple Loosestrife with Cordgrass
    public void ReplaceWithCordgrassButtonClicked()
    {
        purpleLoosestrifePanel.SetActive(false); // Hide the Purple Loosestrife panel
        purpleLoosestrifePanelActive = false; // Set bool purpleLoosestrifePanelActive to false
        playerInput.controlsLocked = false; // unlock player controls
        replaceWithAmericanLotusButton.SetActive(false); // Hide the replace with American Lotus button
        replaceWithCordgrassButton.SetActive(false); // Hide the replace with Cordgrass button
        replaceWithSwampMilkweedButton.SetActive(false); // Hide the replace with Swamp Milkweed button
        replaceWithYellowConeflowerButton.SetActive(false); // Hide the replace with Yellow Coneflower button
        wasReplaceWithCordgrassButtonClicked = true; // Set bool wasReplaceWithCordgrassButtonClicked to true
        Invoke("ResetPlantButtonsState", 2f); ; // Call ResetPlantButtonsState
        changeablePlant.SwapPlants(); // Call the SwapPlants method to swap the plant to Cordgrass
    }

    // Method to replace Purple Loosestrife with Swamp Milkweed
    public void ReplaceWithSwampMilkweedButtonClicked()
    {
        purpleLoosestrifePanel.SetActive(false); // Hide the Purple Loosestrife panel
        purpleLoosestrifePanelActive = false; // Set bool purpleLoosestrifePanelActive to false
        playerInput.controlsLocked = false; // unlock player controls
        replaceWithAmericanLotusButton.SetActive(false); // Hide the replace with American Lotus button
        replaceWithCordgrassButton.SetActive(false); // Hide the replace with Cordgrass button
        replaceWithSwampMilkweedButton.SetActive(false); // Hide the replace with Swamp Milkweed button 
        replaceWithYellowConeflowerButton.SetActive(false); // Hide the replace with Yellow Coneflower button
        wasReplaceWithSwampMilkweedButtonClicked = true; // Set bool wasReplaceWithSwampMilkweedButtonClicked to true
        Invoke("ResetPlantButtonsState", 2f); ; // Call ResetPlantButtonsState
        changeablePlant.SwapPlants(); // Call the SwapPlants method to swap the plant to Swamp Milkweed
    }

    // Method to replace Purple Loosestrife with Yellow Coneflower
    public void ReplaceWithYellowConeflowerButtonClicked()
    {
        purpleLoosestrifePanel.SetActive(false); // Hide the Purple Loosestrife panel
        purpleLoosestrifePanelActive = false; // Set bool purpleLoosestrifePanelActive to false
        playerInput.controlsLocked = false; // unlock player controls
        replaceWithAmericanLotusButton.SetActive(false); // Hide the replace with American Lotus button
        replaceWithCordgrassButton.SetActive(false); // Hide the replace with Cordgrass button
        replaceWithSwampMilkweedButton.SetActive(false); // Hide the replace with Swamp Milkweed button
        replaceWithYellowConeflowerButton.SetActive(false); // Hide the replace with Yellow Coneflower button
        wasReplaceWithYellowConeflowerButtonClicked = true; // Set bool wasReplaceWithYellowConeflowerButtonClicked to true
        Invoke("ResetPlantButtonsState", 2f); ; // Call ResetPlantButtonsState
        changeablePlant.SwapPlants(); // Call the SwapPlants method to swap the plant to Yellow Coneflower
    }

    // Method to reset the state of tree buttons
    public void ResetTreeButtonsState()
    {
        wasReplaceWithSycamoreButtonClicked = false; // Set bool wasReplaceWithSycamoreButtonClicked to false
        wasReplaceWithBoxElderButtonClicked = false; // Set bool wasReplaceWithBoxElderButtonClicked to false
        replaceWithSycamoreButton.SetActive(true); // Show the replace plant button
        replaceWithBoxElderButton.SetActive(true); // Show the replace with box elder button
    }

    // Method to reset the state of plant buttons
    public void ResetPlantButtonsState()
    {
        wasReplaceWithAmericanLotusButtonClicked = false; // Set bool wasReplaceWithAmericanLotusButtonClicked to false
        wasReplaceWithCordgrassButtonClicked = false; // Set bool wasReplaceWithCordgrassButtonClicked to false
        wasReplaceWithSwampMilkweedButtonClicked = false; // Set bool wasReplaceWithSwampMilkweedButtonClicked to false
        wasReplaceWithYellowConeflowerButtonClicked = false; // Set bool wasReplaceWithYellowConeflowerButtonClicked to false
    }

    // Method to strikethrough text
    private void StrikethroughText()
    {
        // If bool isSnappingTurtleFound is true and bool objectivesComplete is false...
        if (isSnappingTurtleFound && !objectivesComplete)
        {
            objectiveSubtext2.fontStyle = FontStyles.Strikethrough; // Strikethrough snapping turtle text
        }

        // If bool isCommonGarterSnakeFound is true and bool objectivesComplete is false...
        if (isCommonGarterSnakeFound && !objectivesComplete)
        {
            objectiveSubtext3.fontStyle = FontStyles.Strikethrough; // Strikethrough common garter snake text
        }

        // If bool isNorthernMapTurtleFound is true and bool objectivesComplete is false...
        if (isNorthernMapTurtleFound && !objectivesComplete)
        {
            objectiveSubtext4.fontStyle = FontStyles.Strikethrough; // Strikethrough northern map turtle text
        }

        // If bool isAsianCarpFound is true and bool objectivesComplete is false...
        if (isAsianCarpFound && !objectivesComplete)
        {
            objectiveSubtext5.fontStyle = FontStyles.Strikethrough; // Strikethrough asian carp text
        }

        // If bool isBeaverFound is true and bool objectivesComplete is false...
        if (isBeaverFound && !objectivesComplete)
        {
            objectiveSubtext6.fontStyle = FontStyles.Strikethrough; // Strikethrough beaver text
        }

        // If bool isWhiteTailedDeerFound is true and bool objectivesComplete is false...
        if (isWhiteTailedDeerFound && !objectivesComplete)
        {
            objectiveSubtext12.fontStyle = FontStyles.Strikethrough; // Strikethrough white-tailed deer text
        }

        // If bool isBandedPennantDragonflyFound is true and bool objectivesComplete is false...
        if (isBandedPennantDragonflyFound && !objectivesComplete)
        {
            objectiveSubtext9.fontStyle = FontStyles.Strikethrough; // Strikethrough banded pennant dragonfly text
        }

        // If bool isMuskratFound is true and bool objectivesComplete is false...
        if (isMuskratFound && !objectivesComplete)
        {
            objectiveSubtext10.fontStyle = FontStyles.Strikethrough; // Strikethrough muskrat text
        }

        // If bool isEasternStarlingFound is true and bool objectivesComplete is false...
        if (isEasternStarlingFound && !objectivesComplete)
        {
            objectiveSubtext8.fontStyle = FontStyles.Strikethrough; // Strikethrough eastern starling text
        }

        // If bool isPaintedLadyButterflyFound is true and bool objectivesComplete is false...
        if (isPaintedLadyButterflyFound && !objectivesComplete)
        {
            objectiveSubtext13.fontStyle = FontStyles.Strikethrough; // Strikethrough painted lady butterfly text
        }

        // If bool isRaccoonFound is true and bool objectivesComplete is false...
        if (isRaccoonFound && !objectivesComplete)
        {
            objectiveSubtext14.fontStyle = FontStyles.Strikethrough; // Strikethrough raccoon text
        }

        // If bool isBaldEagleFound is true and bool objectivesComplete is false...
        if (isBaldEagleFound && !objectivesComplete)
        {
            objectiveSubtext15.fontStyle = FontStyles.Strikethrough; // Strikethrough bald eagle text
        }

        // If bool deerEventZoneComplete is true and bool objectivesComplete is false...
        if (deerEventZoneComplete && !objectivesComplete)
        {
            eventObjectiveSubtext3.fontStyle = FontStyles.Strikethrough; // Strikethrough deer event text
        }
        // If bool birdEventZoneComplete is true and bool objectivesComplete is false...
        if (birdEventZoneComplete && !objectivesComplete)
        {
            eventObjectiveSubtext2.fontStyle = FontStyles.Strikethrough; // Strikethrough bird event text
        }
        // If bool fishEventZoneComplete is true and bool objectivesComplete is false...
        if (fishEventZoneComplete && !objectivesComplete)
        {
            eventObjectiveSubtext1.fontStyle = FontStyles.Strikethrough; // Strikethrough fish event text
        }
    }


    // Method to check if all event zones are complete
    private void EventsComplete()
    {
        // If bool deerEventZoneComplete is true, bool birdEventZoneComplete is true, and bool fishEventZoneComplete is true...
        if (deerEventZoneComplete && birdEventZoneComplete && fishEventZoneComplete && !eventZonePanelActive && !plantSortingPanelShown)
        {
            foreach (ChangeNavAgentSpeed deer in groupOfDeer.GetComponentsInChildren<ChangeNavAgentSpeed>())
            {
                if (deer != null)
                {
                    deer.SetOutline(false); // Hide outline for each bird in the group
                }
            }

            eventZonesComplete = true; // Set bool eventZonesComplete to true
            //Debug.Log("All event zones are complete!"); // Debug.Log
            eventsObjectivesPanel.SetActive(false); // Hide event objectives panel
            objectivesPanel.SetActive(true); // Show objectives panel
            plantSortingObjective.SetActive(true); // Show plant sorting objective
            plantSortingObjectiveText.text = "Find and click on invasive plants.";
            objectivesShown = true; // Set objectivesShown to true
            bradfordPearsSwappedCounterText1.SetActive(true); // Show plants swapped counter text 1
            purpleLoosestrifesSwappedCounterText1.SetActive(true); // Show plants swapped counter text 1
            //plantsCorrectlySwappedCounterText1.SetActive(true); // Show plants swapped counter text 1
            StartPlantSorting(); // Call StartPlantSorting to allow plant sorting panel to be shown
        }
    }

    public void StartPlantSorting()
    {
        startPlantSorting = true; // Set startPlantSorting to true to allow plant sorting panel to be shown
        plantSortingPanelShown = true; // Set plantSortingPanelShown to true to allow plant sorting panel to be shown
    }

    // Method to check if all objectives are complete
    private void ObjectivesComplete()
    {
        // Check if all objectives are met
        if (isAsianCarpFound && isBaldEagleFound && isBandedPennantDragonflyFound && isBeaverFound && isCommonGarterSnakeFound
            && isEasternStarlingFound && isBeaverFound && isMuskratFound && isNorthernMapTurtleFound && isPaintedLadyButterflyFound
            && isRaccoonFound && isSnappingTurtleFound && isWhiteTailedDeerFound)
            { 
                objectivesComplete = true; // Set objectivesComplete to true if all objectives are met
            }
        
    }

    private void PlantSortingComplete()
    {
        int plantsSwapped = bradfordPearsSwapped + purpleLoosestrifesSwapped; // Calculate total plants swapped

        if (plantsSwapped == 12 && !plantSortingComplete)
        {
            plantSortingComplete = true;
        }
    }

    // Method to check if any animal has been previously clicked
    private void CheckForPreviouslyClickedAnimals()
    {
        if (ConservationInteractionObject.wasAsianCarpPreviouslyClicked && !isAsianCarpFound)
        {
            isAsianCarpFound = true; // Set isAsianCarpFound to true if Asian Carp was previously clicked
        }
        if (ConservationInteractionObject.wasBaldEaglePreviouslyClicked && !isBaldEagleFound)
        {
            isBaldEagleFound = true; // Set isBaldEagleFound to true if Bald Eagle was previously clicked
        }
        if (ConservationInteractionObject.wasBandedPennantDragonflyPreviouslyClicked && !isBandedPennantDragonflyFound)
        {
            isBandedPennantDragonflyFound = true; // Set isBandedPennantDragonflyFound to true if Banded Pennant Dragonfly was previously clicked
        }
        if (ConservationInteractionObject.wasBeaverPreviouslyClicked && !isBeaverFound)
        {
            isBeaverFound = true; // Set isBeaverFound to true if Beaver was previously clicked
        }
        if (ConservationInteractionObject.wasGarterSnakePreviouslyClicked && !isCommonGarterSnakeFound)
        {
            isCommonGarterSnakeFound = true; // Set isCommonGarterSnakeFound to true if Common Garter Snake was previously clicked
        }
        if (ConservationInteractionObject.wasEasternStarlingPreviouslyClicked && !isEasternStarlingFound)
        {
            isEasternStarlingFound = true; // Set isEasternStarlingFound to true if Eastern Starling was previously clicked
        }
        if (ConservationInteractionObject.wasMuskratPreviouslyClicked && !isMuskratFound)
        {
            isMuskratFound = true; // Set isMuskratFound to true if Muskrat was previously clicked
        }
        if (ConservationInteractionObject.wasSnappingTurtlePreviouslyClicked && !isSnappingTurtleFound)
        {
            isSnappingTurtleFound = true; // Set isSnappingTurtleFound to true if Snapping Turtle was previously clicked
        }
        if (ConservationInteractionObject.wasRaccoonPreviouslyClicked && !isRaccoonFound)
        {
            isRaccoonFound = true; // Set isRaccoonFound to true if Raccoon was previously clicked
        }
        if (ConservationInteractionObject.wasNorthernMapTurtlePreviouslyClicked && !isNorthernMapTurtleFound)
        {
            isNorthernMapTurtleFound = true; // Set isNorthernMapTurtleFound to true if Northern Map Turtle was previously clicked
        }
        if (ConservationInteractionObject.wasPaintedLadyButterflyPreviouslyClicked && !isPaintedLadyButterflyFound)
        {
            isPaintedLadyButterflyFound = true; // Set isPaintedLadyButterflyFound to true if Painted Lady Butterfly was previously clicked
        }
        if (ConservationInteractionObject.wasWhiteTailedDeerPreviouslyClicked && !isWhiteTailedDeerFound)
        {
            isWhiteTailedDeerFound = true; // Set isWhiteTailedDeerFound to true if White-Tailed Deer was previously clicked
        }
    }

    private void RunEndGameCycle()
    {
        bool anyPanelOpen = false;
        foreach (var plant in Object.FindObjectsByType<ChangeablePlant>(FindObjectsSortMode.None))
        {
            if ((plant.correctPlantSwappedPanel != null && plant.correctPlantSwappedPanel.activeSelf) ||
                (plant.incorrectPlantSwappedPanel != null && plant.incorrectPlantSwappedPanel.activeSelf))
            {
                anyPanelOpen = true;
                break;
            }
        }

        if (!anyPanelOpen)
        {
            if (plantSortingComplete && objectivesComplete && eventZonesComplete && !trappingCompleted)
            {
                //Debug.Log("All objectives are complete!"); // Debug.Log
                endOfGamePanel.SetActive(true); // Show end of game panel
                endOfGamePanelActive = true; // Set end of game panel active
                playerInput.controlsLocked = true; // Lock player controls
                returnButton.SetActive(true); // Show return button
                trappingCompleted = true; // Set trappingCompleted to true   
                TriggerMiniGameCompleteEvent(0);
            }
        }
    }

     // For dev use. Skips game
     public void DebugCompleteGame()
     {
          //Debug.Log("All objectives are complete!"); // Debug.Log
          endOfGamePanel.SetActive(true); // Show end of game panel
          endOfGamePanelActive = true; // Set end of game panel active
          TriggerMiniGameCompleteEvent(0);
          trappingCompleted = true; //set global variable to true
          SceneManager.LoadScene("Overworld");
     }
}
