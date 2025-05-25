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
    private List<GameObject> trashBags; // List to hold trash bag instances
    private List<GameObject> gasCans; // List to hold gas can instances
    private List<GameObject> aluminumCans; // List to hold aluminum can instances
    private List<GameObject> tires; // List to hold tire instances

    [SerializeField]
    private List<GameObject> surfaceWaves = new List<GameObject>(); // List to hold surface wave instances

    [SerializeField]
    private List<Vector3> surfaceWaveWaypoints = new List<Vector3>(); // List to hold preplanned positions for the surface wave

    [Header("Scripts")]
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
    public BiodiversityEffects1PanelClickHandler biodiversityEffects1PanelScript;
    public BiodiversityEffects2PanelClickHandler biodiversityEffects2PanelScript;
    public BiodiversityEffects3PanelClickHandler biodiversityEffects3PanelScript;

    [Header("Float Variables")]
    private float targetProgress = 1f;
    private float loadingTime = 0f;
    private float showPanel = 3f;
    private float progressIncrement = 0.2f;
    private float panelTimer = 0.1f;
    private float endGameTimer = .75f;

    [Header("Booleans")]
    public bool isPressed = false;
    public bool isMiniGameOver = false;
    public bool readyToTransition = false;
    public bool firstWaterTestObjectivesVisible = false;
    public bool secondWaterTestObjectivesVisible = false;
    public bool firstWaterTestReady = true;
    public bool secondWaterTestReady = false;
    public bool instructionsShown = false;
    public bool objectivesComplete = false;
    public bool gameStarted = false;
    public bool pauseButtonClicked = false;
    public bool testIsRunning = false;

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
    private SFXMaker trashClicked;

    [SerializeField]
    private SFXMaker fishClicked;

    [SerializeField]
    private SFXMaker mammalClicked;

    [SerializeField]
    private SFXMaker riverbankClicked;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Reset static variables in InteractionObject
        InteractionObject.ResetStaticVariables();

        Time.timeScale = 0f; // Freezes time
        GetProgressBar(); // Call the GetProgressBar function to initialize the progress bar
        GetPanels(); // Call the GetPanels function to initialize the panels
        GetLists(); // Call the GetLists function to initialize the lists
        additionalAnimals.SetActive(false); // Set additional animals to not active at the start
        objectivesPanel.SetActive(false); // Set objectivesPanel to not active at the start
        waterTestObjective.SetActive(false); // Set waterTestObjective to not active at the start
        surfaceWaves[surfaceWaves.Count - 3].SetActive(false); // Disable the third to last surface wave
        surfaceWaves[surfaceWaves.Count - 2].SetActive(false); // Disable the second to last surface wave
        surfaceWaves[surfaceWaves.Count - 1].SetActive(false); // Disable the last surface wave
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
        // If bool aPanelIsActive is true...
        if (aPanelIsActive)
        {
            CheckForEPress(); // Call CheckForEPress method
        }

        // If bool surfaceWaveClicked is true...
        if (InteractionObject.surfaceWaveClicked) 
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

        // If bool isPressed is true & bool pauseButtonClicked is false...
        if (pauseMenuScript.isPaused == true && !pauseButtonClicked)
        {
            PauseGame(); // Call PauseGame method
            pauseButtonClicked = true; // Set bool pauseButtonClicked to true
            Invoke("ResetPauseBool", 0.1f); // Invoke ResetPauseBool method after 0.1 seconds
        }

        // If bool isPressed is false & bool pauseButtonClicked is false & bool gameStarted is true...
        if (pauseMenuScript.isPaused == false && !pauseButtonClicked && gameStarted)
        {
            ResumeGame(); // Call ResumeGame method
            pauseButtonClicked = true; // Set bool pauseButtonClicked to true
            Invoke("ResetPauseBool", 0.1f); // Invoke ResetPauseBool method after 0.1 seconds
            PauseButton.SetActive(true); // Set PauseButton to active
        }

        ShowObjectives(); // Call ShowObjectives method

        ShowObjectivesPanels(); // Call ShowObjectivesPanels method

        StrikethroughText(); // Call StrikethroughText method

        // If slider value is equal to targetProgress variable and bool isWaterQualityGood is false...
        if (slider.value == targetProgress && !isWaterQualityGood)
        {
            // If isMiniGameOver is false...
            if (!isMiniGameOver)
            {
                testIsRunning = false; // Set bool testIsRunning to false
                isMiniGameOver = true; // Set bool isMiniGameOver to true
                Invoke("PoorWaterQuality", endGameTimer); // Invoke method PoorWaterQuality after panelTimer (in seconds)
            }
            else
            {
                // If isMiniGameOver and isCleanWaterPanelClicked is true...
                if (cleanWaterPanelScript.isCleanWaterPanelClicked && !readyToTransition)
                {
                    EndMiniGame(); // Call EndMiniGame method
                }
            }
        }

        // If slider value is equal to targetProgress variable and bool isWaterQuality...
        if (slider.value == targetProgress && isWaterQualityGood)
        {
            // If isMiniGameOver is false...
            if (!isMiniGameOver)
            {
                testIsRunning = false; // Set bool testIsRunning to false
                isMiniGameOver = true; // Set bool isMiniGameOver to true
                Invoke("GoodWaterQuality", endGameTimer); // Invoke method GoodWaterQuality after panelTimer (in seconds)
            }
            else
            {
                // If isMiniGameOver and isPanelGreatJobClicked is true...
                if (greatJobPanelScript.isGreatJobPanelClicked && !readyToTransition)
                {
                    EndMiniGame(); // Call EndMiniGame method
                }
            }
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

    // Function to get lists
    public void GetLists()
    {
        SpawnManager spawnManager = FindAnyObjectByType<SpawnManager>(); // Get SpawnManager component
        if (spawnManager != null)
        {
            trashBags = spawnManager.GetSpawnedTrashBags(); // Get spawned trash bags from SpawnManager
            Debug.Log($"Retrieved {trashBags.Count} trash objects from SpawnManager."); // Debug.Log
            gasCans = spawnManager.GetSpawnedGasCans(); // Get spawned gas cans from SpawnManager
            Debug.Log($"Retrieved {gasCans.Count} gas objects from SpawnManager."); // Debug.Log
            aluminumCans = spawnManager.GetSpawnedAluminumCans(); // Get spawned aluminum cans from SpawnManager
            Debug.Log($"Retrieved {aluminumCans.Count} aluminum objects from SpawnManager."); // Debug.Log
            tires = spawnManager.GetSpawnedTires(); // Get spawned tires from SpawnManager
            Debug.Log($"Retrieved {tires.Count} tire objects from SpawnManager."); // Debug.Log
        }
        else
        {
            Debug.LogWarning("SpawnManager not found in the scene."); // Debug.LogWarning
        }
    }

    // Methood to play button click sound
    public void PlayButtonClick()
    {
        interactButton.PlaySound(); // Play button click sound
    }

    // Method to play good water test sound
    public void PlayGoodWaterTestSound()
    {
        goodWaterTest.PlaySound(); // Play good water test sound
    }

    // Method to play bad water test sound
    public void PlayPoorWaterTestSound()
    {
        badWaterTest.PlaySound(); // Play bad water test sound
    }

    // Start game
    private void StartGame()
    {
        Time.timeScale = 1f; // Unfreeze time
        StartButton.SetActive(false); // Set StartButton to not active
        PlayButtonClick(); // Call PlayButtonClick method
        //InteractionObject.isClickable = true; // Set bool isClickable to true
        gameStarted = true; // Set bool gameStarted to true
        testIsRunning = true; // Set bool testIsRunning to true
        StartCoroutine(TimeDelay()); // Start coroutine TimeDelay()

        // If isFirstWaterTestComplete is false...
        if (!isFirstWaterTestComplete)
        {
            DeactivatePanel(0); // Disable firstIntroductionPanel
        }

        // If isFirstWaterTestComplete is true and isSecondWaterTestComplete is false...
        if (isFirstWaterTestComplete && !isSecondWaterTestComplete)
        {
            DeactivatePanel(1); // Disable secondIntroductionPanel
            additionalAnimals.SetActive(true); // Activate additional animals in the scene
        }
    }

    // Method to pause the game
    void PauseGame()
    {
        Time.timeScale = 0f; // Freeze time
    }

    // Method to resume the game
    void ResumeGame()
    {
        Time.timeScale = 1f; // Unfreeze time
    }

    //Methood to reset pause button clicked bool
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

    // Method to set bool aPanelIsActive to false
    public void SetAPanelIsActiveFalse()
    {
        aPanelIsActive = false; // Set the active panel flag to false
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
            Debug.LogError("Invalid panel index: " + panelIndex); // Debug.LogError
        }
    }

    // Method to check for E key press
    private void CheckForEPress()
    {
        //If the E key is pressed...
        if (Input.GetKeyDown(KeyCode.E))
        {
            // If bool effectsOfTitePanelActive...
            if (effectsOfTirePanelActive)
            {
                tireEffectsPanelScript.OnPointerClick(null); // Call OnPointerClick method
                PlayButtonClick(); // Call PlayButtonClick method
                DeactivatePanel(5); // Deactivate tireEffectsPanel
            }

            // If bool effectsOfGasPanelActive...
            else if (effectsOfGasPanelActive)
            {
                gasEffectsPanelScript.OnPointerClick(null); // Call OnPointerClick method
                PlayButtonClick(); // Call PlayButtonClick method
                DeactivatePanel(3); // Deactivate gasEffectsPanel
            }

            // If bool effectsOfTrashPanelActive...
            else if (effectsOfTrashPanelActive)
            {
                trashEffectsPanelScript.OnPointerClick(null); // Call OnPointerClick method
                PlayButtonClick(); // Call PlayButtonClick method
                DeactivatePanel(4); // Deactivate trashEffectsPanel
            }

            // If bool effectsOfAluminumPanelActive...
            else if (effectsOfAluminumPanelActive)
            {
                aluminumEffectsPanelScript.OnPointerClick(null); // Call OnPointerClick method
                PlayButtonClick(); // Call PlayButtonClick method
                DeactivatePanel(6); // Deactivate aluminumEffectsPanel
            }

            // If bool effectsOfBiodiversity1PanelActive...
            else if (effectsOfBiodiversity1PanelActive)
            {
                biodiversityEffects1PanelScript.OnPointerClick(null); // Call OnPointerClick method
                PlayButtonClick(); // Call PlayButtonClick method
                DeactivatePanel(11); // Deactivate biodiversityEffects1Panel
            }

            // If bool effectsOfBiodiversity2PanelActive...
            else if (effectsOfBiodiversity2PanelActive)
            {
                biodiversityEffects2PanelScript.OnPointerClick(null); // Call OnPointerClick method
                PlayButtonClick(); // Call PlayButtonClick method
                DeactivatePanel(13); // Deactivate biodiversityEffects2Panel
            }

            // If bool effectsOfBiodiversity3PanelActive...
            else if (effectsOfBiodiversity3PanelActive)
            {
                biodiversityEffects3PanelScript.OnPointerClick(null); // Call OnPointerClick method
                PlayButtonClick(); // Call PlayButtonClick method
                DeactivatePanel(12); // Deactivate biodiversityEffects3Panel
            }

            // If bool lookAtTrashPanelActive...
            else if (lookAtTrashPanelActive)
            {
                lookAtTrashPanelScript.OnPointerClick(null); // Call OnPointerClick method
                PlayButtonClick(); // Call PlayButtonClick method
                DeactivatePanel(2); // Deactivate lookAtTrashPanel
            }

            // If bool biodiversityPanelActive...
            else if (biodiversityPanelActive)
            {
                biodiversityPanelScript.OnPointerClick(null); // Call OnPointerClick method
                PlayButtonClick(); // Call PlayButtonClick method
                DeactivatePanel(10); // Deactivate biodiversityPanel
            }

            // If bool cleanWaterPanelActive...
            else if (cleanWaterPanelActive)
            {
                cleanWaterPanelScript.OnPointerClick(null); // Call OnPointerClick method
                PlayButtonClick(); // Call PlayButtonClick method
                DeactivatePanel(9); // Deactivate cleanWaterPanel
            }

            // If bool greatJobPanelActive...
            else if (greatJobPanelActive)
            {
                greatJobPanelScript.OnPointerClick(null); // Call OnPointerClick method
                PlayButtonClick(); // Call PlayButtonClick method
                DeactivatePanel(15); // Deactivate greatJobPanel
            }
        }
    }

    // Method for showing objectives
    private void ShowObjectives()
    {
        // If bool firstWaterTestObjectivesVisible is false & bool isLookAtTrashPanelClicked is true...
        if (!firstWaterTestObjectivesVisible && lookAtTrashPanelScript.isLookAtTrashPanelClicked)
        {
            objectivesPanel.SetActive(true); // Enable objectives panel
            firstWaterTestObjectives.SetActive(true); // Enable objectives
            firstWaterTestObjectivesVisible = true; // Set bool objectivesVisible to true
        }

        // If bool secondWaterTestObjectivesVisible is false & bool isBiodiversityPanelClicked is true...
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
        // If bool isFirstWaterTestComplete is false and bool firstWaterTestObjectivesVisible is true...
        if (!isFirstWaterTestComplete && firstWaterTestObjectivesVisible)
        {
            // If bool effectsOfTirePanelActive...
            if (effectsOfTirePanelActive)
            {
                ActivatePanel(5); // Activate the tire panel if effectsOfTirePanelActive is true
            }

            // If bool effectsOfGasPanelActive...
            else if (effectsOfGasPanelActive)
            {
                ActivatePanel(3); // Activate the gas panel if effectsOfGasPanelActive is true
            }

            // If bool effectsOfTrashPanelActive...
            else if (effectsOfTrashPanelActive)
            {
                ActivatePanel(4); // Activate the trash panel if effectsOfTrashPanelActive is true
            }

            // If bool effectsOfAluminumPanelActive...
            else if (effectsOfAluminumPanelActive)
            {
                ActivatePanel(6); // Activate the aluminum panel if effectsOfAluminumPanelActive is true
            }
        }

        // If bool isFirstWaterTestComplete is true, bool isSecondWaterTestComplete is false, & bool secondWaterTestObjectivesVisible is true...
        if (isFirstWaterTestComplete && !isSecondWaterTestComplete && secondWaterTestObjectivesVisible)
        {
            // If bool effectsOfGasPanelActive...
            if (effectsOfBiodiversity1PanelActive)
            {
                ActivatePanel(11); // Activate the biodiversity panel1 if effectsOfBiodiversity1PanelActive is true
            }

            // If bool effectsOfBiodiversity2PanelActive...
            else if (effectsOfBiodiversity2PanelActive)
            {
                ActivatePanel(13); // Activate the biodiversity panel2 if effectsOfBiodiversity2PanelActive is true
            }

            // If bool effectsOfBiodiversity3PanelActive...
            else if (effectsOfBiodiversity3PanelActive)
            {
                ActivatePanel(12); // Activate the biodiversity panel3 if effectsOfBiodiversity3PanelActive is true
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
            // If bool isFishObjectiveComplete and isMammalObjectiveComplete are all true and objectivesComplete is false...
            if (isFishObjectiveComplete && isMammalObjectiveComplete && !objectivesComplete)
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
        // If bool objectivesComplete is true and bool firstWaterTestObjectivesVisible is true...
        if (objectivesComplete && firstWaterTestObjectivesVisible)
        {
            firstWaterTestObjectives.SetActive(false); // Disable firstWaterTestObjectives
            waterTestObjective.SetActive(true); // Disable waterTestObjective
            surfaceWaves[surfaceWaves.Count - 3].SetActive(true); // Enable the third to last surface wave
            surfaceWaves[surfaceWaves.Count - 2].SetActive(true); // Enable the second to last surface wave
            surfaceWaves[surfaceWaves.Count - 1].SetActive(true); // Enable the last surface wave
        }

        // If bool objectivesComplete is true and bool secondWaterTestObjectivesVisible is true...
        if (objectivesComplete && secondWaterTestObjectivesVisible)
        {
            secondWaterTestObjectives.SetActive(false); // Disable secondWaterTestObjectives
            waterTestObjective.SetActive(true); // Disable waterTestObjective
            surfaceWaves[surfaceWaves.Count - 3].SetActive(true); // Enable the third to last surface wave
            surfaceWaves[surfaceWaves.Count - 2].SetActive(true); // Enable the second to last surface wave
            surfaceWaves[surfaceWaves.Count - 1].SetActive(true); // Enable the last surface wave
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
        }

        // If bool isMiniGameOver is true...
        if (isMiniGameOver)
        {
            waterTestObjectiveText.fontStyle = FontStyles.Strikethrough; // Set font style to strikethrough
        }
    }

    // Method to move surface wave
    private void MoveSurfaceWave()
    {
        // If the surface wave was clicked...
        if (InteractionObject.surfaceWaveClicked)
        {
            HandleSurfaceWaveClick(InteractionObject.clickedSurfaceWave); // Call HandleSurfaceWaveClick method   
        }
    }

    // Method to handle surface wave click
    private void HandleSurfaceWaveClick(InteractionObject clickedSurfaceWave)
    {
        // If clickedSurfaceWave is null...
        if (clickedSurfaceWave == null)
        {
            Debug.LogWarning("Clicked object is not a valid surface wave."); // Debug.LogWarning
            return;
        }

        InvokeProgressBar(); // Call method InvokeProgressBar

        // Hide the surface wave by disabling its renderer and collider
        Renderer renderer = clickedSurfaceWave.GetComponent<Renderer>(); // Get the renderer component of the clicked surface wave
        Collider collider = clickedSurfaceWave.GetComponent<Collider>(); // Get the collider component of the clicked surface wave

        // If renderer is not null...
        if (renderer != null)
        {
            renderer.enabled = false; // Disable the renderer to hide the surface wave
        }

        // If collider is not null...
        if (collider != null)
        {
            collider.enabled = false; // Disable the collider to make the surface wave non-interactable
        }

        Debug.Log($"SurfaceWave {clickedSurfaceWave.name} hidden."); // Debug.Log

        // Move the surface wave to a random preplanned position
        if (surfaceWaveWaypoints.Count > 0)
        {
            int randomIndex = Random.Range(0, surfaceWaveWaypoints.Count); // Pick a random index
            Vector3 newPosition = surfaceWaveWaypoints[randomIndex]; // Get the new position from the preplanned positions list
            clickedSurfaceWave.transform.position = newPosition; // Move the surface wave to the new position

            Debug.Log($"SurfaceWave {clickedSurfaceWave.name} moved to random position: {newPosition}"); // Debug.Log
        }
        else
        {
            Debug.LogError("No preplanned positions defined for SurfaceWave."); // Debug.LogError
        }

        // Make the surface wave visible again after a short delay
        StartCoroutine(ShowSurfaceWaveAfterDelay(clickedSurfaceWave.gameObject, 1f)); // 1-second delay

        InteractionObject.surfaceWaveClicked = false; // Reset the surface wave clicked flag
    }

    // Coroutine to show the surface wave after a delay
    private IEnumerator ShowSurfaceWaveAfterDelay(GameObject surfaceWave, float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay before showing the surface wave again

        // Re-enable the renderer and collider to make the surface wave visible and interactable
        Renderer renderer = surfaceWave.GetComponent<Renderer>(); // Get the renderer component of the surface wave
        Collider collider = surfaceWave.GetComponent<Collider>(); // Get the collider component of the surface wave

        // If renderer is not null...
        if (renderer != null)
        {
            renderer.enabled = true; // Enable the renderer to make the surface wave visible
        }

        // If collider is not null...
        if (collider != null)
        {
            collider.enabled = true; // Enable the collider to make the surface wave interactable
        }

        Debug.Log($"SurfaceWave {surfaceWave.name} is now visible and interactable."); // Debug.Log
    }

    // Invoke Progress Bar
    private void InvokeProgressBar()
    {
        progressBarScript.IncrementProgress(progressIncrement); // Add progress to progress bar in increments of progressIncrement
    }

    // Method to load the main scene
    private void LoadToMainScene()
    {
        SceneManager.LoadScene("Overworld"); // Load Overworld
    }

    // Method for if water quality is poor
    private void PoorWaterQuality()
    {
        // If bool isMiniGameOver is false...
        if (isMiniGameOver)
        {
            ActivatePanel(8); // Activate poor water quality panel
            PlayPoorWaterTestSound(); // Play bad water test sound
            progressBar.SetActive(false);// Disable progress bar
            isFirstWaterTestComplete = true; // Set bool isFirstWaterTestComplete to true
            objectivesPanel.SetActive(false); // Disable objectives panel
            waterTestObjective.SetActive(false); // Disable waterTestObjective

            TriggerMiniGameCompleteEvent(0);   // Update game progress. Can add a score to pass through

            // If isMiniGameOver is true and isCleanWaterPanelClicked is false...
            if (!cleanWaterPanelScript.isCleanWaterPanelClicked)
            {
                Invoke("ShowCleanWaterPanel", showPanel); // Invoke method ShowCleanWaterPanel after showPanel (in seconds)
            }
        }
    }

    // Method for if water quality is good
    private void GoodWaterQuality()
    {
        // If isMiniGameOver is false...
        if (isMiniGameOver)
        {
            ActivatePanel(14); // Activate good water quality panel
            PlayGoodWaterTestSound(); // Play good water test sound
            progressBar.SetActive(false);// Disable progress bar
            isWaterQualityGood = true; // Set bool isWaterQualityGood to true
            isSecondWaterTestComplete = true; // Set bool isSecondWaterTestComplete to true
            objectivesPanel.SetActive(false); // Disable objectives panel
            waterTestObjective.SetActive(false); // Disable waterTestObjective

            TriggerMiniGameCompleteEvent(0);   // Update game progress. Can add a score to pass through

            // If isMiniGameOver is true and isGreatJobPanelClicked is false...
            if (!greatJobPanelScript.isGreatJobPanelClicked)
            {
                Invoke("ShowGreatJobPanel", showPanel); // Invoke method ShowCleanWaterPanel after showPanel (in seconds)
            }
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
        readyToTransition = true; // Set bool readyToTransition to true
    }

    // Coroutine to delay showing the panel
    private IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(panelTimer);
        ShowPanel();
    }

    // Method to show the panel based on the water test completion status
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

    // Method to handle trash bag interaction
    public void HandleTrashBagInteraction()
    {
        // Iterate through each trash bag in the list
        foreach (GameObject trashBag in trashBags)
        {
            // If the trash bag is not null...
            if (trashBag != null)
            {
                SpriteRenderer iconSprite = trashBag.GetComponentInChildren<SpriteRenderer>(); // Get the SpriteRenderer component of the trash bag

                iconSprite.enabled = false; // Disable the icon sprite
            }
        }
    }

    // Method to handle gas canister interaction
    public void HandleGasCanisterInteraction()
    {
        // Iterate through each gas can in the list
        foreach (GameObject gasCan in gasCans)
        {
            // If the gas can is not null...
            if (gasCan != null)
            {
                SpriteRenderer iconSprite = gasCan.GetComponentInChildren<SpriteRenderer>(); // Get the SpriteRenderer component of the gas can

                iconSprite.enabled = false; // Disable the icon sprite
            }
        }
    }

    // Method to handle aluminum can interaction
    public void HandleAluminumCanInteraction()
    {
        // Iterate through each aluminum can in the list
        foreach (GameObject aluminumCan in aluminumCans)
        {
            // If the aluminum can is not null...
            if (aluminumCan != null)
            {
                SpriteRenderer iconSprite = aluminumCan.GetComponentInChildren<SpriteRenderer>(); // Get the SpriteRenderer component of the aluminum can

                iconSprite.enabled = false; // Disable the icon sprite
            }
        }
    }

    // Method to handle tire interaction
    public void HandleTireInteraction()
    {
        // Iterate through each tire in the list
        foreach (GameObject tire in tires)
        {
            // If the tire is not null...
            if (tire != null)
            {
                SpriteRenderer iconSprite = tire.GetComponentInChildren<SpriteRenderer>(); // Get the SpriteRenderer component of the tire

                iconSprite.enabled = false; // Disable the icon sprite
            }
        }
    }
}

