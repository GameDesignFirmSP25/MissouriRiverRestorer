using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor.Experimental.GraphView;

public class AnimalGameManager : BaseMiniGameManager
{
    [Header("UI Elements")]
    [SerializeField]
    GameObject objectivesPanel;

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
    TextMeshProUGUI ScoreText;

    [SerializeField]
    TextMeshProUGUI EndText;

    [Header("Game Objects")]
    [SerializeField]
    GameObject startPanel;

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
    GameObject startButton;

    [SerializeField]
    GameObject returnButton;

    [SerializeField]
    GameObject pauseButton;

    public GameObject[] dialoguePanels = new GameObject[12];

    [Header("Float Variables")]
    private float ScoreThreshold = 25f;
    public float Score = 0f;
    private float resetDelay = 0.1f; // Delay to reset dialogue panel click handler

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
    private bool hasResetDialogueState = false; // Flag to prevent multiple resets of dialogue state
    private bool objective1Active = false;
    private bool objective2Active = false;
    private bool objective3Active = false;


    [Header("Raycast Variables")]
    private Ray ray;
    private RaycastHit hit;

    [Header("Script References")]
    public LowerBankZoneTrigger lowerBankZoneTrigger;
    public MidBankZoneTrigger midBankZoneTrigger;
    public UpperBankZoneTrigger upperBankZoneTrigger;

    void Start()
    {
        // Initialize UI elements
        InitializeUI();
        GetPanels();
        DisableText(); // Disable all text objects at the start
        Time.timeScale = 0; // Freeze time at start of game
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Score: " + Score.ToString() + "/" + ScoreThreshold.ToString(); //update score text 
        
        // TODO: This shouls be a one time event, not continuous calls in Udpate()
        if (Score >= ScoreThreshold) //end game if score is at threshold
        {
            EndLevel(); // End the level
        }

        if (LowerBankZoneTrigger.lowerBankEntered && !objective1Active)
        {
            LowerBankEntered();
        }

        if (MidBankZoneTrigger.midBankEntered && !objective2Active)
        {
            MidBankEntered();
        }

        if (UpperBankZoneTrigger.upperBankEntered && !objective3Active)
        {
            UpperBankEntered();
        }

        AnimalClicked();

        // If dialogue is active and the dialogue panel is open...
        if (DialoguePanelClickHandler.isDialoguePanelClicked && !hasResetDialogueState)
        {
            Debug.Log("Dialogue panel clicked. Hiding panel.");
            DialoguePanelClickHandler.isDialoguePanelClicked = false; // Reset dialogue panel click handler
            ResetDialogueState();
            hasResetDialogueState = true; // Set the flag to true to prevent multiple calls
        }
    }

    public void StartButton() //triggers on start button press
    {
        objectivesPanel.SetActive(true); //show objectives panel
        startButton.SetActive(false); //hide start button
        startPanel.SetActive(false); //hide start panel
        pauseButton.SetActive(true); //show pause button
        Time.timeScale = 1; // Unfreeze time
    }

    public void InitializeUI()
    {
        objectivesPanel.SetActive(false); //hide objectives panel
        returnButton.SetActive(false); //hide return button
        startButton.SetActive(true); //show start button
        startPanel.SetActive(true); //show start panel
        pauseButton.SetActive(false); //hide pause button
    }

    private void GetPanels()
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

    // Method to disable all text objects
    public void DisableText()
    {
        DeactivateAllPanels(); // Deactivate all dialogue panels
        objectiveSubtext1.gameObject.SetActive(false); //hide objective subtext 1
        objectiveSubtext2.gameObject.SetActive(false); //hide objective subtext 2
        objectiveSubtext3.gameObject.SetActive(false); //hide objective subtext 3
        objectiveSubtext4.gameObject.SetActive(false); //hide objective subtext 4
        objectiveSubtext5.gameObject.SetActive(false); //hide objective subtext 5
        objectiveSubtext6.gameObject.SetActive(false); //hide objective subtext 6
    }

    // Method to activate specific panel in index
    public void ActivatePanel(int dialoguePanelIndex)
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
    public void DeactivatePanel(int dialoguePanelIndex)
    {
        dialoguePanels[dialoguePanelIndex].SetActive(false); // Activate the specified panel
        dialogueIsActive = false; // Set the active panel flag to false
        Debug.Log("Panel " + dialoguePanelIndex + " deactivated."); // Debug.Log
    }

    // Method to deactivate all panels
    public void DeactivateAllPanels()
    {
        // Loop through each dialoguePanel in the dialoguePanels array
        foreach (GameObject dialoguePanel in dialoguePanels)
        {
            dialoguePanel.SetActive(false); // Deactivate all panels
        }

        dialogueIsActive = false; // Set the active panel flag to false
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

        if (RaycastScript.paintedLadyButterflyClicked && !dialogueIsActive)
        {
            PaintedLadyButterflyClicked();
        }
        
        if (RaycastScript.muskeratClicked && !dialogueIsActive) // If muskrat is clicked and dialogue is not active
        {
            MuskeratClicked();
        }

        if (RaycastScript.snappingTurtleClicked && !dialogueIsActive) // If snapping turtle is clicked and dialogue is not active
        {
            SnappingTurtleClicked();
        }

        if (RaycastScript.beaverClicked && !dialogueIsActive) // If beaver is clicked and dialogue is not active
        {
            BeaverClicked();
        }

        if (!RaycastScript.raccoonClicked && !dialogueIsActive) // If raccoon is clicked and dialogue is not active
        {
            RaccoonClicked();
        }

        if (RaycastScript.northernMapTurtleClicked && !dialogueIsActive) // If northern map turtle is clicked and dialogue is not active
        {
            NorthernMapTurtleClicked();
        }

        if (RaycastScript.asianCarpClicked && !dialogueIsActive) // If asian carp is clicked and dialogue is not active
        {
            AsianCarpClicked();
        }
    }

    // Handle clicks on eastern starling
    private void EasternStarlingClicked()
    {
        ActivatePanel(1);
        dialogueIsActive = true; // Set dialogue active
    }

    // Handle clicks on white-tailed deer
    private void WhiteTailedDeerClicked()
    {
        ActivatePanel(2);
        dialogueIsActive = true; // Set dialogue active
    }

    // Handle clicks on banded pennant dragonfly
    private void BandedPennantDragonflyClicked()
    {
        ActivatePanel(10);
        dialogueIsActive = true; // Set dialogue active
    }

    // Handle clicks on garter snake
    private void CommonGarterSnakeClicked()
    {
        ActivatePanel(8);
        dialogueIsActive = true; // Set dialogue active
    }

    // Handle clicks on bald eagle
    private void BaldEagleClicked()
    {
        ActivatePanel(3);
        dialogueIsActive = true; // Set dialogue active
    }

    // Handle clicks on muskrat
    private void MuskeratClicked()
    {
        ActivatePanel(6);
        dialogueIsActive = true; // Set dialogue active
    }

    // Handle clicks on snapping turtle
    private void SnappingTurtleClicked()
    {
        ActivatePanel(7);
        dialogueIsActive = true; // Set dialogue active
    }

    // Handle clicks on beaver
    private void BeaverClicked()
    {
        ActivatePanel(4);
        dialogueIsActive = true; // Set dialogue active
    }

    // Handle clicks on raccoon
    private void RaccoonClicked()
    {
        ActivatePanel(5);
        dialogueIsActive = true; // Set dialogue active
    }

    // Handle clicks on northern map turtle
    private void NorthernMapTurtleClicked()
    {
        ActivatePanel(9);
        dialogueIsActive = true; // Set dialogue active
    }

    // Handle clicks on asian carp
    private void AsianCarpClicked()
    {
        ActivatePanel(0);
        dialogueIsActive = true; // Set dialogue active
    }

    // Handle clicks on painted lady butterfly
    private void PaintedLadyButterflyClicked()
    {
        ActivatePanel(11);
        dialogueIsActive = true; // Set dialogue active
    }

    // Method to reset dialogue state
    private void ResetDialogueState()
    {
        Debug.Log("Resetting dialogue state..."); // Debug.Log
        DisableText(); // Disable all text objects
        dialogueIsActive = false; // Set dialogue inactive
        hasResetDialogueState = false; // Reset the flag to allow future dialogue interactions
    }

    // Set subtext for objective 1
    private void LowerBankEntered()
    {
        Debug.Log("Player is exploring the lower bank."); //Debug.Log
        objective1Active = true; // Set bool objective1Active to true
        objectiveSubtext1.gameObject.SetActive(true); //show objective subtext 1
        objectiveSubtext1.text = "Interact with fauna."; //set objective subtext 1 text
        objectiveSubtext2.gameObject.SetActive(true); //show objective subtext 2
        objectiveSubtext2.text = "Find invasive species."; //set objective subtext 2 text
    }

    // Set subtext for objective 2
    private void MidBankEntered()
    {
        Debug.Log("Player is exploring the Mid Bank!"); //Debug.Log
        objective2Active = true; // Set bool objective2Active to true
        objectiveSubtext3.gameObject.SetActive(true); //show objective subtext 3
        objectiveSubtext3.text = "Interact with fauna."; //set objective subtext 3 text
        objectiveSubtext4.gameObject.SetActive(true); //show objective subtext 4
        objectiveSubtext4.text = "Find invasive species."; //set objective subtext 4 text
    }

    // Set subtext for oibjective 3
    private void UpperBankEntered()
    {
        Debug.Log("Player is exploring the Upper Bank!"); //Debug.Log
        objective3Active = true; // Set bool objective3Active to true
        objectiveSubtext5.gameObject.SetActive(true); //show objective subtext 5
        objectiveSubtext5.text = "Interact with fauna."; //set objective subtext 5 text
        objectiveSubtext6.gameObject.SetActive(true); //show objective subtext 6
        objectiveSubtext6.text = "Find invasive species."; //set objective subtext 6 text
    }
}
