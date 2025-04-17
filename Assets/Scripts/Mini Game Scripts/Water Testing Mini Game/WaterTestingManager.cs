using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;

public class WaterTestingManager : BaseMiniGameManager
{
    [SerializeField]
    private GameObject progressBar;

    [SerializeField]
    private GameObject StartButton;

    [SerializeField] 
    private GameObject PauseButton;

    [SerializeField]
    private GameObject firstWaterTestObjectives;

    [SerializeField]
    private GameObject secondWaterTestObjectives;

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

     private Slider slider;
    public Button StartBtn;

    public GameObject[] panels = new GameObject[16]; // Array of panels to manage
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

    private float targetProgress = 1f;
    private float loadingTime = 0f;
    private float showPanel = 3f;
    private float progressIncrement = 0.1f;
    private float panelTimer = 0.1f;
    //private float enableTime = 5f;

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

    public GameObject PauseUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PauseButton.SetActive(false); // Set PauseButton to not active
        progressBar = GameObject.Find("Progress Bar"); // Get Slider component on Progress Bar
        slider = progressBar.GetComponent<Slider>(); // slider is equal to the slider component
        GetPanels(); // Call the GetPanels function to initialize the panels
        Cursor.visible = true; // Set Cursor to be visible
        Time.timeScale = 0f; // Freezes time

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
        // If bool testTubeClicked is true...
        if (raycastScript.testTubeClicked) 
        {
            // If the value on the slider component is less than the targetProgress variable...
            if (slider.value < targetProgress)
            {
                CollectWater(); // call CollectWater method

                // If bool instructionsShown and testingInstructionsActive are true...
                if (instructionsShown && testingInstructionsActive)
                {
                    DisableTestingInstructions(); // call DisableTestingInstructions method
                }
            }
        }

        if (pauseMenuScript.isPaused == true)
        {
            PauseGame();
        }

        if (pauseMenuScript.isPaused == false)
        {
            ResumeGame();
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

    // Start game
    private void StartGame()
    {
        Time.timeScale = 1f; // Unfreeze time
        StartButton.SetActive(false); // Set StartButton to not active
        progressBar.SetActive(true); // Enable Progress bar
        PauseButton.SetActive(true); // Set PauseButton to active
        Raycast.isClickable = true; // Set bool isClickable to true
        gameStarted = true; // Set bool gameStarted to true
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

    //// Method to deactivate all panels
    //public void DeactivateAllPanels()
    //{
    //    // Loop through each panel in the panels array
    //    foreach (GameObject panel in panels)
    //    {
    //        panel.SetActive(false); // Deactivate all panels
    //    }

    //    aPanelIsActive = false; // Set the active panel flag to false
    //}

    // Method to deactivate a specific panel
    public void DeactivatePanel(int panelIndex)
    {
        panels[panelIndex].SetActive(false); // Activate the specified panel
        aPanelIsActive = false; // Set the active panel flag to false
        Debug.Log("Panel " + panelIndex + " deactivated."); // Debug.Log
    }

    // Method to activate a specific panel
    public void ActivatePanel(int panelIndex)
    {
        if (panelIndex >= 0 && panelIndex < panels.Length)
        {
            panels[panelIndex].SetActive(true); // Activate the specified panel
            aPanelIsActive = true; // Set the active panel flag to true
            Debug.Log("Panel " + panelIndex + " activated."); // Debug.Log
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
            firstWaterTestObjectives.SetActive(true); // Enable objectives
            firstWaterTestObjectivesVisible = true; // Set bool objectivesVisible to true
        }

        // If bool secondWaterTestObjectivesVisible is false...
        if (!secondWaterTestObjectivesVisible && biodiversityPanelScript.isBiodiversityPanelClicked)
        {
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
                ShowInstructions(); // Call ShowInstructions method
            }
        }
    }

    // Method for showing instructions
    public void ShowInstructions()
    {
        // if bool objectivesComplete is true & bool aPanelIsActive is false & bool !instructionsShown is false...
        if (!instructionsShown)
        {
            ActivatePanel(7); // Activate testing instructions panel
            testingInstructionsActive = true; // Set bool testingInstructionsActive to true
            instructionsShown = true; // Set bool instructionsShown to true
        }
    }

    // Method for disabling testing instructions
    private void DisableTestingInstructions()
    {
        DeactivatePanel(7); // Deactivate testing instructions panel
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
            raycastScript.testTubeClicked = false; // Set bool testTubeClicked to false
        }
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
            progressBar.SetActive(false);// Disable progress bar
            isMiniGameOver = true; // Set bool isMiniGameOver to true
            isFirstWaterTestComplete = true; // Set bool isFirstWaterTestComplete to true

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
            progressBar.SetActive(false);// Disable progress bar
            isMiniGameOver = true; // Set bool isMiniGameOver to true
            isWaterQualityGood = true; // Set bool isWaterQualityGood to true
            isSecondWaterTestComplete = true; // Set bool isSecondWaterTestComplete to true

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

