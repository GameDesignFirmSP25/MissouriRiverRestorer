using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class WaterTestingManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject testingInstructionsPanel;

    [SerializeField]
    private GameObject poorWaterQualityPanel;

    [SerializeField]
    private GameObject goodWaterQualityPanel;

    [SerializeField]
    private GameObject progressBar;

    [SerializeField]
    private GameObject firstIntroductionPanel;

    [SerializeField]
    private GameObject secondIntroductionPanel;

    [SerializeField]
    private GameObject StartButton;

    [SerializeField]
    private GameObject cleanWaterIsEssentialPanel;

    [SerializeField]
    private GameObject greatJobPanel;

    [SerializeField]
    private GameObject biodiversityPanel;

    [SerializeField]
    private GameObject lookAtTrashPanel;

    [SerializeField]
    private GameObject effectsOfGasPanel;

    [SerializeField]
    private GameObject effectsOfTrashPanel;

    [SerializeField]
    private GameObject effectsOfTirePanel;

    [SerializeField]
    private GameObject effectsOfAluminumPanel;

    [SerializeField]
    private GameObject effectsOfBiodiversityPanel1;

    [SerializeField]
    private GameObject effectsOfBiodiversityPanel2;

    [SerializeField]
    private GameObject effectsOfBiodiversityPanel3;

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
    public Raycast raycastScript;
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
    private float enableTime = 5f;

    public bool isPressed = false;
    public bool isMiniGameOver = false;
    public bool readyToTransition = false;
    public bool firstWaterTestObjectivesVisible = false;
    public bool secondWaterTestObjectivesVisible = false;
    public bool instructionsShown = false;
    public bool objectivesComplete = false;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        progressBar = GameObject.Find("Progress Bar"); // Get Slider component on Progress Bar
        slider = progressBar.GetComponent<Slider>(); // slider is equal to the slider component

        Cursor.visible = true; // Set Cursor to be visible
        SwitchUI();
        Time.timeScale = 0f; // Freezes time

        // If isFirstWaterTestComplete is false...
        if (!isFirstWaterTestComplete)
        {
            firstIntroductionPanel.SetActive(true); // Set set first intro panel to active
            StartButton.SetActive(true); // Set StartButton to active
            StartBtn.onClick.AddListener(StartGame); // when panel is clicked, add listener StartGame
        }

        // Else if isFirstWaterTestComplete is true...
        else
        {
            secondIntroductionPanel.SetActive(true); // Set second intro panel to active
            StartButton.SetActive(true); // Set StartButton to active
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
                
                if (instructionsShown)
                {
                    DisableTestingInstructions(); // call DisableTestingInstructions method
                }
            }
        }

        ShowObjectives();

        StrikethroughText();

        AreObjectivesComplete();

        // if bool objectivesComplete is true & bool aPanelIsActive is false & bool !instructionsShown is false...
        if (objectivesComplete && !raycastScript.aPanelIsActive && !instructionsShown)
        {
            Invoke("EnableTestingInstructions", enableTime); // Invoke method EnableTestingInstructions after enableTime (in seconds)
        }

        IsAPanelActive();

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

    // Start game
    private void StartGame()
    {
        Time.timeScale = 1f; // Unfreeze time
        StartButton.SetActive(false); // Set StartButton to not active
        progressBar.SetActive(true); // Enable Progress bar
        Raycast.isClickable = true; // Set bool isClickable to true
        StartCoroutine(TimeDelay()); // Start coroutine TimeDelay()

        // If isFirstWaterTestComplete is false...
        if (!isFirstWaterTestComplete)
        {
            firstIntroductionPanel.SetActive(false); // Disable firstIntroductionPanel
        }

        // If isFirstWaterTestComplete is true and isSecondWaterTestComplete is false...
        if (isFirstWaterTestComplete && !isSecondWaterTestComplete)
        {
            secondIntroductionPanel.SetActive(false); // Disable secondIntroductionPanel
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

    // Method for checking if a panel is active
    private void IsAPanelActive()
    {
        // If bool effectsOfAluminumPanelActive, effectsOfGasPanelActive, effectsOfTirePanelActive, and effectsOfTrashPanelActive are all false...
        if (!effectsOfAluminumPanelActive && !effectsOfGasPanelActive && !effectsOfTirePanelActive && !effectsOfTrashPanelActive)
        {
            // If bool aPanelIsActive is true...
            if (raycastScript.aPanelIsActive)
            {
                raycastScript.aPanelIsActive = false; // Set bool aPanelIsActive to false
            }
        }

        // If bool effectsOfBiodiversityPanel1Active, effectsOfBiodiversityPanel2Active, and effectsOfBiodiversityPanel3Active are all false...
        if (!effectsOfBiodiversity1PanelActive && !effectsOfBiodiversity2PanelActive && !effectsOfBiodiversity3PanelActive)
        {
            // If bool aPanelIsActive is true...
            if (raycastScript.aPanelIsActive)
            {
                raycastScript.aPanelIsActive = false; // Set bool aPanelIsActive to false
            }
        }
    }

    // Method for checking if objectives are complete
    private void AreObjectivesComplete()
    {
        // If bool isFirstWaterTestComplete is false and bool isSecondWaterTestComplete is false...
        if (!isFirstWaterTestComplete && !isSecondWaterTestComplete)
        {
            // If bool isAluminumCanObjectiveComplete, isGasCanObjectiveComplete, isTireObjectiveComplete, and isTrashBagObjectiveComplete are all true...
            if (isAluminumCanObjectiveComplete && isGasCanObjectiveComplete && isTireObjectiveComplete && isTrashBagObjectiveComplete && !objectivesComplete)
            {
                Debug.Log("All objectives complete!"); // Debug.Log
                objectivesComplete = true; // Set bool objectivesComplete to true
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
            }
        }
    }

    // Method for enabling testing instructions
    private void EnableTestingInstructions()
    {
        testingInstructionsPanel.SetActive(true); // Enable testingInstructions
        testingInstructionsActive = true; // Set bool testingInstructionsActive to true
        instructionsShown = true; // Set bool instructionsShown to true
    }

    // Method for disabling testing instructions
    private void DisableTestingInstructions()
    {
        testingInstructionsPanel.SetActive(false); // Disable testing instructions
        testingInstructionsActive = false; // Set bool testingInstructionsActive to false
    }

    // Method  called On destroy
    private void OnDestroy()
    {
        StartBtn.onClick.RemoveListener(StartGame); // when panel is clicked remove listener StartGame
    }

    private void SwitchUI()
    {
        testingInstructionsPanel.SetActive(false); // Disable testing intsructions
        progressBar.SetActive(false); // Disable Progress bar
        poorWaterQualityPanel.SetActive(false); // Disable poor water quality panel
        goodWaterQualityPanel.SetActive(false); // Disable good water quality panel
        cleanWaterIsEssentialPanel.SetActive(false); // Disable clean water is essential panel
        greatJobPanel.SetActive(false); // Disable great job panel
        firstIntroductionPanel.SetActive(false); // Disable first intro panel 
        secondIntroductionPanel.SetActive(false); // Disable first intro panel 
        lookAtTrashPanel.SetActive(false); // Disable look at trash panel
        biodiversityPanel.SetActive(false); // Disable biodiversity panel
        effectsOfGasPanel.SetActive(false); // Disable affects of gas panel
        effectsOfTrashPanel.SetActive(false); // Disable affects of trash panel
        effectsOfTirePanel.SetActive(false); // Disable affects of tire panel
        effectsOfAluminumPanel.SetActive(false); // Disable affects of aluminum panel
        firstWaterTestObjectives.SetActive(false); // Disable objectives on screen
        secondWaterTestObjectives.SetActive(false); // Disable objectives on screen
        effectsOfBiodiversityPanel1.SetActive(false); // Disable affects of biodiversity panel 1
        effectsOfBiodiversityPanel2.SetActive(false); // Disable affects of biodiversity panel 2
        effectsOfBiodiversityPanel3.SetActive(false); // Disable affects of biodiversity panel 3
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
        SceneManager.LoadScene(0); // Load scene first in build order. Should point to overworld scene
    }

    // Method for if water quality is poor
    private void PoorWaterQuality()
    {
        // If bool isMiniGameOver is false...
        if (!isMiniGameOver)
        {
            poorWaterQualityPanel.SetActive(true);// Enable poor water quality text
            progressBar.SetActive(false);// Disable progress bar
            isMiniGameOver = true; // Set bool isMiniGameOver to true
            isFirstWaterTestComplete = true; // Set bool isFirstWaterTestComplete to true

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
            goodWaterQualityPanel.SetActive(true);// Enable poor water quality panel
            progressBar.SetActive(false);// Disable progress bar
            isMiniGameOver = true; // Set bool isMiniGameOver to true
            isWaterQualityGood = true; // Set bool isWaterQualityGood to true
            isSecondWaterTestComplete = true; // Set bool isSecondWaterTestComplete to true

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
        cleanWaterIsEssentialPanel.SetActive(true); // Enable clean water is essential panel 
        cleanWaterPanelActive = true; // Set bool cleanWaterPanelActive to true
        poorWaterQualityPanel.SetActive(false); // Disable poor water quality panel
    }

    // Method to show great job panel
    private void ShowGreatJobPanel()
    {
        greatJobPanel.SetActive(true); // Enable great job panel
        greatJobPanelActive = true; // Set bool greatJobPanel to true
        goodWaterQualityPanel.SetActive(false); // Disable good water quality panel
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
            lookAtTrashPanel.SetActive(true); // Enable look at trash panel
            lookAtTrashPanelActive = true; // Set bool lookAtTrashPanelActive to true
        }

        // If isFirstWaterTestComplete is true...
        if (isFirstWaterTestComplete)
        {
            biodiversityPanel.SetActive(true); // Enable biodiversity panel
            biodiversityPanelActive = true; // Set bool biodiversityPanelActive to true
        }
    }
}

