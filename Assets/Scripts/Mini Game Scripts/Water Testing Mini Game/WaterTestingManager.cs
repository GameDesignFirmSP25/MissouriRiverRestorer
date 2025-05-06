using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;
using StarterAssets;

public class WaterTestingManager : BaseMiniGameManager
{
    [Header("UI Elements")]
    [SerializeField]
    private GameObject progressBar;

    [SerializeField]
    private GameObject StartButton;

    [SerializeField] 
    private GameObject PauseButton;

    [SerializeField]
    private GameObject objectivesPanel;

    [SerializeField]
    private GameObject firstWaterTestObjectives;

    [SerializeField]
    private GameObject secondWaterTestObjectives;

    [SerializeField]
    private GameObject waterTestObjective;

    [SerializeField]
    TextMeshProUGUI gasCanObjectiveText;

    [SerializeField]
    TextMeshProUGUI trashBagObjectiveText;

    [SerializeField]
    TextMeshProUGUI tireObjectiveText;

    [SerializeField]
    TextMeshProUGUI aluminumCanObjectiveText;

    [SerializeField]
    TextMeshProUGUI fishObjectiveText;

    [SerializeField]
    TextMeshProUGUI mammalObjectiveText;

    [SerializeField]
    TextMeshProUGUI riverbankObjectiveText;

    [SerializeField]
    TextMeshProUGUI waterTestObjectiveText;

    private Slider slider;
    public Button StartBtn;

    [Header("Game Objects")]
    public GameObject surfaceWavePrefab; // Prefab for the surface wave
    public GameObject additionalAnimals;

    [Header("Arrays and Lists")]
    public GameObject[] panels = new GameObject[16]; // Array of panels to manage

    [SerializeField]
    private List<GameObject> surfaceWaves = new List<GameObject>(); // List to hold surface wave instances

    [SerializeField]
    private List<Vector3> surfaceWaveWaypoints = new List<Vector3>(); // List to hold preplanned positions for the surface wave

    [Header("Scripts")]
    public Raycast raycastScript;
    public PausMenuManager pauseMenuScript;
    public ProgressBar progressBarScript;
    public CleanWaterPanelClickHandler cleanWaterPanelScript;
    public GreatJobPanelClickHandler greatJobPanelScript;
    public LookAtTrashPanelClickHandler lookAtTrashPanelScript;
    public BiodiversityPanelClickHandler biodiversityPanelScript;
    public GasEffectsPanelClickHandler gasEffectsPanelScript;
    public TrashEffectsPanelClickHandler trashEffectsPanelScript;
    public TireEffectsPanelClickHandler tireEffectsPanelScript;
    public AluminumEffectsPanelClickHandler aluminumEffectsPanelScript;

    [Header("Float Variables")]
    private float targetProgress = 1f;
    private float loadingTime = 0f;
    private float showPanel = 3f;
    private float progressIncrement = 0.1f;
    private float panelTimer = 0.1f;

    [Header("Booleans")]
    public bool isPressed = false;
    public bool isMiniGameOver = false;
    public bool readyToTransition = false;
    public bool firstWaterTestObjectivesVisible = false;
    public bool secondWaterTestObjectivesVisible = false;
    public bool firstWaterTestReady = true;
    public bool panel3 = false;
    public bool panel4 = false;
    public bool panel5 = false;
    public bool panel6 = false;
    public bool panel11 = false;
    public bool panel12 = false;
    public bool panel13 = false;
    public bool secondWaterTestReady = false;
    public bool instructionsShown = false;
    public bool objectivesComplete = false;
    public bool gameStarted = false;
    public bool pauseButtonClicked = false;

    [Header("Global Variables")]
    public static bool aPanelIsActive = false;
    public static bool isTrashBagObjectiveComplete = false;
    public static bool isGasCanObjectiveComplete = false;
    public static bool isTireObjectiveComplete = false;
    public static bool isAluminumCanObjectiveComplete = false;
    public static bool isFishObjectiveComplete = false;
    public static bool isMammalObjectiveComplete = false;
    public static bool isRiverbankObjectiveComplete = false;
    public static bool cleanWaterPanelActive = false;
    public static bool greatJobPanelActive = false;
    public static bool testingInstructionsActive = false;
    public static bool lookAtTrashPanelActive = false;
    public static bool biodiversityPanelActive = false;
    public static bool effectsOfGasPanelActive = false;
    public static bool effectsOfTrashPanelActive = false;
    public static bool effectsOfTirePanelActive = false;
    public static bool effectsOfAluminumPanelActive = false;
    public static bool effectsOfBiodiversity1PanelActive = false;
    public static bool effectsOfBiodiversity2PanelActive = false;
    public static bool effectsOfBiodiversity3PanelActive = false;
    public static bool isWaterQualityGood = false;
    public static bool isFirstWaterTestComplete = false;
    public static bool isSecondWaterTestComplete = false;
    public static bool firstRunThrough = false;
    public static bool secondRunThrough = false;

    [Header("Player Input")]
    public StarterAssetsInputs playerInput;

    [Header("Audio")]
    [SerializeField]
    private SFXMaker interactButton;

    [SerializeField]
    private SFXMaker goodWaterTest;

    [SerializeField]
    private SFXMaker badWaterTest;

    [SerializeField]
    private SFXMaker surfaceWaveClick;

    [SerializeField]
    private SFXMaker trashGrabbed;

    [SerializeField]
    private SFXMaker fishClicked;

    [SerializeField]
    private SFXMaker mammalClicked;

    [SerializeField]
    private SFXMaker riverbankClicked;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0f; // Freezes time
        GetProgressBar(); // Call the GetProgressBar function to initialize the progress bar
        GetPanels(); // Call the GetPanels function to initialize the panels
        additionalAnimals.SetActive(false); // Set additional animals to not active at the start
        objectivesPanel.SetActive(false); // Set objectivesPanel to not active at the start
        waterTestObjective.SetActive(false); // Set waterTestObjective to not active at the start
        Cursor.visible = true; // Set Cursor to be visible

        // If isFirstWaterTestComplete is false...
        if (!isFirstWaterTestComplete)
        {
            ActivatePanel(0); // Activate the first panel
            StartButton.SetActive(true); // Set StartButton to active
            StartBtn.onClick.AddListener(StartGame); // when panel is clicked, add listener StartGame
        }

        // Else if bool isFirstWaterTestComplete is true & bool isSecondWaterTestComplete is false...
        else if (isFirstWaterTestComplete && !isSecondWaterTestComplete)
        {
            ActivatePanel(1); // Activate the second panel
            StartButton.SetActive(true);
            StartBtn.onClick.AddListener(StartGame); // when panel is clicked, add listener StartGame
        }
        
        // If bool isTrashCollected and isFloraPlanted are true...
        if (isFirstWaterTestComplete && !isSecondWaterTestComplete)
        {
            isWaterQualityGood = true; // Set bool isWaterQualityGood to true
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If bool surfaceWaveClicked is true...
        if (Raycast.surfaceWaveClicked) 
        {
            // If the value on the slider component is less than the targetProgress variable...
            if (slider.value < targetProgress)
            {
                //CollectWater(); // call CollectWater method
                MoveSurfaceWave(); // call MoveSurfaceWave method

                // If bool instructionsShown and testingInstructionsActive are true...
                if (instructionsShown && testingInstructionsActive)
                {
                    DisableTestingInstructions(); // call DisableTestingInstructions method
                }
            }
        }

        if (pauseMenuScript.isPaused == true && !pauseButtonClicked)
        {
            PauseGame();
            pauseButtonClicked = true; // Set bool pauseButtonClicked to true
            Invoke("ResetPauseBool", 0.1f); // Invoke ResetPauseBool method after 0.1 seconds
        }

        if (pauseMenuScript.isPaused == false && !pauseButtonClicked && gameStarted)
        {
            ResumeGame();
            pauseButtonClicked = true; // Set bool pauseButtonClicked to true
            Invoke("ResetPauseBool", 0.1f); // Invoke ResetPauseBool method after 0.1 seconds
            PauseButton.SetActive(true); // Set PauseButton to active
        }

        ShowObjectives();

        ShowObjectivesPanels();

        StrikethroughText();

        // If slider value is equal to targetProgress variable and bool isWaterQualityGood is false...
        if (slider.value == targetProgress && !isWaterQualityGood)
        {
            PoorWaterQuality();
        }

        // If slider value is equal to targetProgress variable and bool isWaterQuality...
        if (slider.value == targetProgress && isWaterQualityGood)
        {
            GoodWaterQuality();
        }
    }

    // Function to get the progress bar
    void GetProgressBar()
    {
        progressBar = GameObject.Find("Progress Bar"); // Get Slider component on Progress Bar
        slider = progressBar.GetComponent<Slider>(); // slider is equal to the slider component
    }

    // Function to get all panels in the scene
    void GetPanels()
    {
        GameObject panel1 = panels[0]; // Get the first introduction panel
        GameObject panel2 = panels[1]; // Get the second introduction panel
        GameObject panel3 = panels[2]; // Get the Look At Trash panel
        GameObject panel4 = panels[3]; // Get the Effects of Gas panel
        GameObject panel5 = panels[4]; // Get the Effects of Trash panel
        GameObject panel6 = panels[5]; // Get the effects of Tire panel
        GameObject panel7 = panels[6]; // Get the Effects of Aluminum panel
        GameObject panel8 = panels[7]; // Get the Testing Instructions panel
        GameObject panel9 = panels[8]; // Get the Poor Water Quality panel
        GameObject panel10 = panels[9]; // Get the Clean Water is Essential panel
        GameObject panel11 = panels[10]; // Get the Biodiversity panel
        GameObject panel12 = panels[11]; // Get the Effects of Biodiversity1 panel
        GameObject panel13 = panels[12]; // Get the Effects of Biodiversity2 panel
        GameObject panel14 = panels[13]; // Get the Effects of Biodiversity3 panel
        GameObject panel15 = panels[14]; // Get the Good Water Quality panel
        GameObject panel16 = panels[15]; // Get the Great Job panel
    }

    public void PlayButtonClick()
    {
        interactButton.PlaySound(); // Play button click sound
    }

    public void PlayGoodWaterTest()
    {
        goodWaterTest.PlaySound(); // Play good water test sound
    }

    public void PlayBadWaterTest()
    {
        badWaterTest.PlaySound(); // Play bad water test sound
    }

    public void PlaySurfaceWaveClick()
    {
        surfaceWaveClick.PlaySound(); // Play surface wave click sound
    }

    public void PlayTrashGrabbed()
    {
        trashGrabbed.PlaySound(); // Play trash grabbed sound
    }

    public void PlayFishClicked()
    {
        fishClicked.PlaySound(); // Play fish grabbed sound
    }

    public void PlayMammalClicked()
    {
        mammalClicked.PlaySound(); // Play mammals grabbed sound
    }

    public void PlayRiverbankClicked()
    {
        riverbankClicked.PlaySound(); // Play riverbank grabbed sound
    }

    // Start game
    private void StartGame()
    {
        Time.timeScale = 1f; // Unfreeze time
        StartButton.SetActive(false); // Set StartButton to not active
        PlayButtonClick(); // Call PlayButtonClick method
        Raycast.isClickable = true; // Set bool isClickable to true
        gameStarted = true; // Set bool gameStarted to true
        StartCoroutine(TimeDelay()); // Start coroutine TimeDelay()

        // If isFirstWaterTestComplete is false...
        if (!isFirstWaterTestComplete)
        {
            DeactivatePanel(0); // Disable firstIntroductionPanel
            firstRunThrough = true; // Set bool firstRunThrough to true
        }

        // If isFirstWaterTestComplete is true and isSecondWaterTestComplete is false...
        if (isFirstWaterTestComplete && !isSecondWaterTestComplete)
        {
            DeactivatePanel(1); // Disable secondIntroductionPanel
            additionalAnimals.SetActive(true); // Activate additional animals in the scene
            secondRunThrough = true; // Set bool secondRunThrough to true
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    void ResetPauseBool()
    {
        pauseButtonClicked = false; // Set bool pauseButtonClicked to false
    }

    // Method to deactivate a specific panel
    public void DeactivatePanel(int panelIndex)
    {
        panels[panelIndex].SetActive(false); // Activate the specified panel
        aPanelIsActive = false; // Set the active panel flag to false
        Debug.Log("Panel " + panelIndex + " deactivated."); // Debug.Log
        playerInput.controlsLocked = false; // Unlock player controls when no panel is active
    }

    // Method to activate a specific panel
    public void ActivatePanel(int panelIndex)
    {
        if (panelIndex >= 0 && panelIndex < panels.Length)
        {
            panels[panelIndex].SetActive(true); // Activate the specified panel
            aPanelIsActive = true; // Set the active panel flag to true
            Debug.Log("Panel " + panelIndex + " activated."); // Debug.Log
            playerInput.controlsLocked = true; // Lock player controls when a panel is active
        }
        else
        {
            Debug.LogError("Invalid panel index: " + panelIndex); // Log an error if the index is invalid
        }
    }

    // Method for showing objectives
    private void ShowObjectives()
    {
        // If bool firstWaterTestObjectivesVisible is false...
        if (!firstWaterTestObjectivesVisible && lookAtTrashPanelScript.isLookAtTrashPanelClicked)
        {
            objectivesPanel.SetActive(true); // Enable objectives panel
            firstWaterTestObjectives.SetActive(true); // Enable objectives
            firstWaterTestObjectivesVisible = true; // Set bool objectivesVisible to true
        }

        // If bool secondWaterTestObjectivesVisible is false...
        if (!secondWaterTestObjectivesVisible && biodiversityPanelScript.isBiodiversityPanelClicked)
        {
            objectivesPanel.SetActive(true); // Enable objectives panel
            secondWaterTestObjectives.SetActive(true); // Enable objectives
            secondWaterTestObjectivesVisible = true; // Set bool objectivesVisible to true
        }
    }

    // Method for showing the correct panel
    private void ShowObjectivesPanels()
    {
        if (!isFirstWaterTestComplete && firstWaterTestObjectivesVisible)
        {
            if (effectsOfTirePanelActive && !panel5)
            {
                ActivatePanel(5); // Activate the tire panel if effectsOfTirePanelActive is true
                panel5 = true; // Set bool effectsOfTirePanelActive to true
            }
            else if (effectsOfGasPanelActive && !panel3)
            {
                ActivatePanel(3); // Activate the gas panel if effectsOfGasPanelActive is true
                panel3 = true; // Set bool effectsOfGasPanelActive to true
            }
            else if (effectsOfTrashPanelActive && !panel4)
            {
                ActivatePanel(4); // Activate the trash panel if effectsOfTrashPanelActive is true
                panel4 = true; // Set bool effectsOfTrashPanelActive to true
            }
            else if (effectsOfAluminumPanelActive && !panel6)
            {
                ActivatePanel(6); // Activate the aluminum panel if effectsOfAluminumPanelActive is true
                panel6 = true; // Set bool effectsOfAluminumPanelActive to true
            }
        }

        if (isFirstWaterTestComplete && !isSecondWaterTestComplete && secondWaterTestObjectivesVisible)
        {
            if (effectsOfBiodiversity1PanelActive && !panel11)
            {
                ActivatePanel(11); // Activate the biodiversity panel1 if effectsOfBiodiversity1PanelActive is true
                panel11 = true; // Set bool effectsOfBiodiversity1PanelActive to true
            }
            else if (effectsOfBiodiversity3PanelActive && !panel13)
            {
                ActivatePanel(13); // Activate the biodiversity panel3 if effectsOfBiodiversity3PanelActive is true
                panel13 = true; // Set bool effectsOfBiodiversity3PanelActive to true
            }
            else if (effectsOfBiodiversity2PanelActive && !panel12)
            {
                ActivatePanel(12); // Activate the biodiversity panel2 if effectsOfBiodiversity2PanelActive is true
                panel12 = true; // Set bool effectsOfBiodiversity2PanelActive to true
            }
        }
    }

    // Method for checking if objectives are complete
    public void AreObjectivesComplete()
    {
        // If bool isFirstWaterTestComplete is false and bool isSecondWaterTestComplete is false...
        if (!isFirstWaterTestComplete && !isSecondWaterTestComplete)
        {
            // If bool isAluminumCanObjectiveComplete, isGasCanObjectiveComplete, isTireObjectiveComplete, and isTrashBagObjectiveComplete are all true...
            if (isAluminumCanObjectiveComplete && isGasCanObjectiveComplete && isTireObjectiveComplete && isTrashBagObjectiveComplete && !objectivesComplete)
            {
                Debug.Log("All objectives complete!"); // Debug.Log
                objectivesComplete = true; // Set bool objectivesComplete to true
                ChangeObjectives(); // Call ChangeObjectives method
                ShowInstructions(); // Call ShowInstructions method
            }
        }

        // If bool isFirstWaterTestComplete is true and bool isSecondWaterTestComplete is false...
        if (isFirstWaterTestComplete && !isSecondWaterTestComplete)
        {
            // If bool isFishObjectiveComplete, isMammalObjectiveComplete, and isRiverbankObjectiveComplete are all true and objectivesComplete is false...
            if (isFishObjectiveComplete && isMammalObjectiveComplete && isRiverbankObjectiveComplete && !objectivesComplete)
            {
                Debug.Log("All objectives complete!"); // Debug.Log
                objectivesComplete = true; // Set bool objectivesComplete to true
                ChangeObjectives(); // Call ChangeObjectives method
                ShowInstructions(); // Call ShowInstructions method
            }
        }
    }

    // Method to change objectives
    public void ChangeObjectives()
    {
        if (objectivesComplete && firstWaterTestObjectivesVisible)
        {
            firstWaterTestObjectives.SetActive(false); // Disable firstWaterTestObjectives
            waterTestObjective.SetActive(true); // Disable waterTestObjective
        }

        if (objectivesComplete && secondWaterTestObjectivesVisible)
        {
            secondWaterTestObjectives.SetActive(false); // Disable secondWaterTestObjectives
            waterTestObjective.SetActive(true); // Disable waterTestObjective
        }
    }

    // Method for showing instructions
    public void ShowInstructions()
    {
        // if bool objectivesComplete is true & bool aPanelIsActive is false & bool !instructionsShown is false...
        if (!instructionsShown)
        {
            //ActivatePanel(7); // Activate testing instructions panel
            testingInstructionsActive = true; // Set bool testingInstructionsActive to true
            instructionsShown = true; // Set bool instructionsShown to true
            progressBar.SetActive(true); // Enable progress bar
        }
    }

    // Method for disabling testing instructions
    private void DisableTestingInstructions()
    {
        //DeactivatePanel(7); // Deactivate testing instructions panel
        testingInstructionsActive = false; // Set bool testingInstructionsActive to false
    }

    // Method  called On destroy
    private void OnDestroy()
    {
        StartBtn.onClick.RemoveListener(StartGame); // when panel is clicked remove listener StartGame
    }

    // Method to strikethrough text
    private void StrikethroughText()
    {
        // If bool isFirstWaterTestComplete is false...
        if (!isFirstWaterTestComplete)
        {
            // If bool isAluminumCanObjectiveComplete is true and bool objectivesComplete is false...
            if (isAluminumCanObjectiveComplete && !objectivesComplete)
            {
                aluminumCanObjectiveText.fontStyle = FontStyles.Strikethrough; // Set font style to strikethrough
            }
            // If bool isGasCanObjectiveComplete is true and bool objectivesComplete is false...
            if (isGasCanObjectiveComplete && !objectivesComplete)
            {
                gasCanObjectiveText.fontStyle = FontStyles.Strikethrough; // Set font style to strikethrough
            }
            // If bool isTireObjectiveComplete is true and bool objectivesComplete is false...
            if (isTireObjectiveComplete && !objectivesComplete)
            {
                tireObjectiveText.fontStyle = FontStyles.Strikethrough; // Set font style to strikethrough
            }
            // If bool isTrashBagObjectiveComplete is true and bool objectivesComplete is false...
            if (isTrashBagObjectiveComplete && !objectivesComplete)
            {
                trashBagObjectiveText.fontStyle = FontStyles.Strikethrough; // Set font style to strikethrough
            }
        }

        // If bool isFirstWaterTestComplete is true...
        if (isFirstWaterTestComplete && !isSecondWaterTestComplete)
        {
            // If bool isFishObjectiveComplete is true and bool objectivesComplete is false...
            if (isFishObjectiveComplete && !objectivesComplete)
            {
                fishObjectiveText.fontStyle = FontStyles.Strikethrough; // Set font style to strikethrough
            }

            // If bool isMammalObjectiveComplete is true and bool objectivesComplete is false...
            if (isMammalObjectiveComplete && !objectivesComplete)
            {
                mammalObjectiveText.fontStyle = FontStyles.Strikethrough; // Set font style to strikethrough
            }

            // If bool isRiverbankObjectiveComplete is true and bool objectivesComplete is false...
            if (isRiverbankObjectiveComplete && !objectivesComplete)
            {
                riverbankObjectiveText.fontStyle = FontStyles.Strikethrough; // Set font style to strikethrough
            }
        }

        // If bool firstRunthrough is true and bool isFirstWaterTestComplete is true...
        if (firstRunThrough && isFirstWaterTestComplete)
        {
            waterTestObjectiveText.fontStyle = FontStyles.Strikethrough; // Set font style to strikethrough
        }

        // If bool secondRunThrough is true and bool isSecondWaterTestComplete is true...
        if (secondRunThrough && isSecondWaterTestComplete)
        {
            waterTestObjectiveText.fontStyle = FontStyles.Strikethrough; // Set font style to strikethrough
        }
    }

    // Collect water
    private void CollectWater()
    {
        // If left mouse button is down and isPressed is false...
        if (Input.GetMouseButtonDown(0) && !isPressed) 
        {
            isPressed = true; // Set bool isPressed to true
            InvokeProgressBar(); // Call method InvokeProgressBar
            Debug.Log("Collecting water..."); // Debug.Log
        }

        // If left mouse button is down and isPressed is true...
        if (Input.GetMouseButtonUp(0) && isPressed) 
        {
            isPressed = false; // Set bool isPressed to false
        }
    }

    private void MoveSurfaceWave()
    {
        // If the surface wave was clicked...
        if (Raycast.surfaceWaveClicked)
        {
            if (raycastScript.clickedObject == null)
            {
                Debug.LogError("No clicked object found. Ensure the Raycast is detecting the surface wave.");
                return;
            }

            GameObject clickedSurfaceWave = raycastScript.clickedObject; // Get the clicked surface wave

            if (clickedSurfaceWave == null || !surfaceWaves.Contains(clickedSurfaceWave))
            {
                Debug.LogWarning("Clicked object is not a valid surface wave.");
                return;
            }

            InvokeProgressBar(); // Call method InvokeProgressBar

            PlaySurfaceWaveClick(); // Play surface wave click sound

            // Hide the surface wave by disabling its renderer and collider
            Renderer renderer = clickedSurfaceWave.GetComponent<Renderer>();
            Collider collider = clickedSurfaceWave.GetComponent<Collider>();

            if (renderer != null)
            {
                renderer.enabled = false;
            }

            if (collider != null)
            {
                collider.enabled = false;
            }

            Debug.Log($"SurfaceWave {clickedSurfaceWave.name} hidden.");

            // Move the surface wave to a random preplanned position
            if (surfaceWaveWaypoints.Count > 0)
            {
                int randomIndex = Random.Range(0, surfaceWaveWaypoints.Count); // Pick a random index
                Vector3 newPosition = surfaceWaveWaypoints[randomIndex];
                clickedSurfaceWave.transform.position = newPosition;

                Debug.Log($"SurfaceWave {clickedSurfaceWave.name} moved to random position: {newPosition}");
            }
            else
            {
                Debug.LogError("No preplanned positions defined for SurfaceWave.");
            }

            // Make the surface wave visible again after a short delay
            StartCoroutine(ShowSurfaceWaveAfterDelay(clickedSurfaceWave, 1f)); // 1-second delay

            // Reset the raycast flag
            Raycast.surfaceWaveClicked = false;
        }
    }

    private IEnumerator ShowSurfaceWaveAfterDelay(GameObject surfaceWave, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Re-enable the renderer and collider to make the surface wave visible and interactable
        Renderer renderer = surfaceWave.GetComponent<Renderer>();
        Collider collider = surfaceWave.GetComponent<Collider>();

        if (renderer != null)
        {
            renderer.enabled = true;
        }

        if (collider != null)
        {
            collider.enabled = true;
        }

        Debug.Log($"SurfaceWave {surfaceWave.name} is now visible and interactable.");
    }

    // Invoke Progress Bar
    private void InvokeProgressBar()
    {
        progressBarScript.IncrementProgress(progressIncrement); // Add progress to progress bar in increments of progressIncrement
    }

    private void LoadToMainScene()
    {
        SceneManager.LoadScene("Overworld"); // Load Overworld
    }

    // Method for if water quality is poor
    private void PoorWaterQuality()
    {
        // If bool isMiniGameOver is false...
        if (!isMiniGameOver)
        {
            ActivatePanel(8); // Activate poor water quality panel
            PlayBadWaterTest(); // Play bad water test sound
            progressBar.SetActive(false);// Disable progress bar
            isMiniGameOver = true; // Set bool isMiniGameOver to true
            isFirstWaterTestComplete = true; // Set bool isFirstWaterTestComplete to true
            objectivesPanel.SetActive(false); // Disable objectives panel
            waterTestObjective.SetActive(false); // Disable waterTestObjective
            firstRunThrough = false; // Set bool firstRunThrough to false

            TriggerMiniGameCompleteEvent(0);   // Update game progress. Can add a score to pass through

            // If isMiniGameOver is true and isCleanWaterPanelClicked is false...
            if (isMiniGameOver && !cleanWaterPanelScript.isCleanWaterPanelClicked)
            {
                Invoke("ShowCleanWaterPanel", showPanel); // Invoke method ShowCleanWaterPanel after showPanel (in seconds)
            }
        }

        // If isMiniGameOver and isCleanWaterPanelClicked is true...
        if (isMiniGameOver && cleanWaterPanelScript.isCleanWaterPanelClicked && !readyToTransition)
        {
            EndMiniGame();
        }
    }

    // Method for if water quality is good
    private void GoodWaterQuality()
    {
        // If isMiniGameOver is false...
        if (!isMiniGameOver)
        {
            ActivatePanel(14); // Activate good water quality panel
            PlayGoodWaterTest(); // Play good water test sound
            progressBar.SetActive(false);// Disable progress bar
            isMiniGameOver = true; // Set bool isMiniGameOver to true
            isWaterQualityGood = true; // Set bool isWaterQualityGood to true
            isSecondWaterTestComplete = true; // Set bool isSecondWaterTestComplete to true
            objectivesPanel.SetActive(false); // Disable objectives panel
            waterTestObjective.SetActive(false); // Disable waterTestObjective
            secondRunThrough = false; // Set bool secondRunThrough to false

            TriggerMiniGameCompleteEvent(0);   // Update game progress. Can add a score to pass through

               // If isMiniGameOver is true and isGreatJobPanelClicked is false...
               if (isMiniGameOver && !greatJobPanelScript.isGreatJobPanelClicked)
            {
                Invoke("ShowGreatJobPanel", showPanel); // Invoke method ShowCleanWaterPanel after showPanel (in seconds)
            }
        }

        // If isMiniGameOver and isPanelGreatJobClicked is true...
        if (isMiniGameOver && greatJobPanelScript.isGreatJobPanelClicked && !readyToTransition)
        {
            EndMiniGame();
        }
    }

    // Method to show clean water is essential panel
    private void ShowCleanWaterPanel()
    {
        DeactivatePanel(8); // Disable poor water quality panel
        cleanWaterPanelActive = true; // Set bool cleanWaterPanelActive to true
        ActivatePanel(9); // Activate clean water is essential panel
    }

    // Method to show great job panel
    private void ShowGreatJobPanel()
    {
        DeactivatePanel(14); // Disable good water quality panel
        greatJobPanelActive = true; // Set bool greatJobPanel to true
        ActivatePanel(15); // Activate great job panel
    }

    // Method to end minigame
    private void EndMiniGame()
    {
        Invoke("LoadToMainScene", loadingTime); // Invoke method LoadToMainScene after loadingTime (in seconds)
        Debug.Log("Load to Overworld"); // Debug.Log
        readyToTransition = true;
    }

    private IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(panelTimer);
        ShowPanel();
    }

    private void ShowPanel()
    {
        // If bool isFirstWaterTestComplete is false...
        if (!isFirstWaterTestComplete)
        {
            ActivatePanel(2); // Activate Look At Trash Panel
            lookAtTrashPanelActive = true; // Set bool lookAtTrashPanelActive to true
        }

        // If isFirstWaterTestComplete is true...
        if (isFirstWaterTestComplete)
        {
            ActivatePanel(10); // Activate Biodiversity panel
            biodiversityPanelActive = true; // Set bool biodiversityPanelActive to true
        }
    }
}

