using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Unity.Cinemachine;

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
    TextMeshProUGUI ScoreText;

    [SerializeField]
    TextMeshProUGUI EndText;

    [SerializeField]
    GameObject dialoguePanel;

    [SerializeField]
    GameObject asianCarpText;

    [SerializeField]
    GameObject easternStarlingText;

    [SerializeField]
    GameObject whiteTailedDeerText;

    [SerializeField]
    GameObject baldEagleText;

    [SerializeField]
    GameObject beaverText;

    [SerializeField]
    GameObject RacconText;

    [SerializeField]
    GameObject MuskeratText;

    [SerializeField]
    GameObject snappingTurtleText;

    [SerializeField]
    GameObject commonGarterSnakeText;

    [SerializeField]
    GameObject northernMapTurtleText;

    [SerializeField]
    GameObject bandedPennantDragonflyText;

    [SerializeField]
    GameObject paintedLadyButterflyText;

    [Header("Game Objects")]
    [SerializeField]
    GameObject startPanel;

    [SerializeField]
    GameObject startButton;

    [SerializeField]
    GameObject returnButton;

    [SerializeField]
    GameObject pauseButton;

    [Header("Float Variables")]
    private float ScoreThreshold = 25f;
    public float Score = 0f;

    [Header("Booleans")]
    public static bool trappingCompleted = false; // Global variable to check if trapping is completed
    public bool easternStarlingClicked = false;
    public bool whiteTailedDeerClicked = false;
    public bool dialogueIsActive = false;

    [Header("Singleton")]
    public static AnimalGameManager Instance { get; private set; } // Singleton instance

    [Header("Fish List")]
    public List<GameObject> fishList = new List<GameObject>(); // List to store fish objects

    [Header("Target Objects")]
    [SerializeField] 
    private string[] easternStarlingNames;

    [SerializeField] 
    private string[] whiteTailedDeerNames;

    [Header("Cinemachine")]
    [SerializeField]
    private CinemachineVirtualCameraBase virtualCamera; // Reference to the Cinemachine virtual camera

    [Header("Scripts")]
    DialoguePanelClickHandler dialoguePanelClickHandler; // Reference to the dialogue panel click handler


    private void Awake()
    {
        // Ensure this is a singleton
        // If an instance is null...
        if (Instance == null)
        {
            Instance = this; // Set this instance as the singleton
        }
        else
        {
            Destroy(gameObject); // If an instance already exists, destroy this one to enforce singleton pattern
        }
    }

    void Start()
    {
        // Initialize UI elements
        dialoguePanel.SetActive(false); //hide dialogue panel
        objectivesPanel.SetActive(false); //hide objectives panel
        returnButton.SetActive(false); //hide return button
        startButton.SetActive(true); //show start button
        startPanel.SetActive(true); //show start panel
        pauseButton.SetActive(false); //hide pause button
        DisableText(); // Disable all text objects at the start
        Time.timeScale = 0; // Freeze time at start of game

        // Set list of eastern starling names
        easternStarlingNames = new string[]
        {
            "Eastern Starling", "Eastern Starling (1)", "Eastern Starling (2)",
            "Eastern Starling (3)", "Eastern Starling (4)", "Eastern Starling (5)",
            "Eastern Starling (6)", "Eastern Starling (7)", "Eastern Starling (8)",
            "Eastern Starling (9)", "Eastern Starling (10)"
        };

        // Set list of white-tailed deer names
        whiteTailedDeerNames = new string[]
        {
            "White-tailed Deer", "White-tailed Deer (1)", "White-tailed Deer (2)",
            "White-tailed Deer (3)"
        };
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

        // If dialogue is active and the dialogue panel is open...
        if (dialoguePanelClickHandler.isDialoguePanelClicked)
        {
            dialoguePanel.SetActive(false); // Hide dialogue panel
            DisableText(); // Disable all text objects
            dialogueIsActive = false; // Set dialogue inactive
        }

    }

    private void LateUpdate()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {

            if (virtualCamera != null && virtualCamera.Priority > 0)
            {
                Vector3 cameraPosition = virtualCamera.State.RawPosition + virtualCamera.State.PositionCorrection; // Get the camera position
                Vector3 cameraForward = (virtualCamera.State.RawOrientation * virtualCamera.State.OrientationCorrection) * Vector3.forward; // Get the camera forward direction
                Debug.DrawRay(cameraPosition, cameraForward * 10, Color.red, 2f); // Draw a ray in the scene view for debugging

                Ray ray = new Ray(cameraPosition, cameraForward);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
                {
                    // If the clicked GameObject's name matches the target name for eastern starling...
                    if (System.Array.Exists(easternStarlingNames, name => name == hit.collider.gameObject.name))
                    {
                        HandleEasternStarlingClick(hit.collider.gameObject); // Handle the click on the eastern starling GameObject
                    }
                    // Check if the clicked GameObject's name matches any white-tailed deer name
                    else if (System.Array.Exists(whiteTailedDeerNames, name => name == hit.collider.gameObject.name))
                    {
                        HandleWhiteTailedDeerClick(hit.collider.gameObject);
                    }
                }

            }
        }
    }

    public void StartButton() //triggers on start button press
    {
        ScoreText.text = "Score: " + Score.ToString() + "/" + ScoreThreshold.ToString();
        objectivesPanel.SetActive(true); //show objectives panel
        startButton.SetActive(false); //hide start button
        startPanel.SetActive(false); //hide start panel
        pauseButton.SetActive(true); //show pause button
        Time.timeScale = 1; // Unfreeze time
    }

    // Method to disable all text objects
    public void DisableText()
    {
        easternStarlingText.SetActive(false); //hide eastern starling text
        whiteTailedDeerText.SetActive(false); //hide white-tailed deer text
        baldEagleText.SetActive(false); //hide bald eagle text
        beaverText.SetActive(false); //hide beaver text
        RacconText.SetActive(false); //hide raccon text
        MuskeratText.SetActive(false); //hide muskrat text
        snappingTurtleText.SetActive(false); //hide snapping turtle text
        commonGarterSnakeText.SetActive(false); //hide common garter snake text
        northernMapTurtleText.SetActive(false); //hide northern map turtle text
        bandedPennantDragonflyText.SetActive(false); //hide banded pennant dragonfly text
        paintedLadyButterflyText.SetActive(false); //hide painted lady butterfly text
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

    // Method to add a new fish to the list
    public void AddToFishList(GameObject fish)
    {
        // If fish is not null...
        if (fish != null)
        {
            fishList.Add(fish); // Add the fish to the list
            Debug.Log($"Fish added to list: {fish.name}"); // Debug.Log
        }
        else
        {
            Debug.LogWarning("Attempted to add a null fish to the list."); // Debug.Log
        }
    }

    // Handle clicks on eastern starling
    private void HandleEasternStarlingClick(GameObject clickedObject)
    {
        if (!easternStarlingClicked)
        {
            Debug.Log($"GameObject {clickedObject.name} was clicked!");
            dialoguePanel.SetActive(true); // Show dialogue panel
            easternStarlingText.SetActive(true); // Show eastern starling text
            easternStarlingClicked = true; // Mark as clicked
            dialogueIsActive = true; // Set dialogue active
        }
        else
        {
            Debug.Log("Eastern Starling has already been clicked.");
        }
    }

    // Handle clicks on white-tailed deer
    private void HandleWhiteTailedDeerClick(GameObject clickedObject)
    {
        if (!whiteTailedDeerClicked)
        {
            Debug.Log($"GameObject {clickedObject.name} was clicked!");
            dialoguePanel.SetActive(true); // Show dialogue panel
            whiteTailedDeerText.SetActive(true); // Show white-tailed deer text
            whiteTailedDeerClicked = true; // Mark as clicked
            dialogueIsActive = true; // Set dialogue active
        }
        else
        {
            Debug.Log("White-tailed Deer has already been clicked.");
        }
    }
}
