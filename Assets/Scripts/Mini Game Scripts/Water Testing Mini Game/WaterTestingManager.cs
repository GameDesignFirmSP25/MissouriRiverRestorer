using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class WaterTestingManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject testingInstructions;

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

    private Slider slider;
    public Button StartBtn;
    public Raycast riverScript;
    public ProgressBar progressBarScript;
    public CleanWaterPanelClickHandler cleanWaterPanelScript;
    public GreatJobPanelClickHandler greatJobPanelScript;

    private float startTime;
    private float endTime;
    private float pressDuration;
    private float targetProgress = 1f;
    private float loadingTime = 3f;
    private float showPanel = 3f;
    private float waitTime = 0f;
    private float repeatRate = 1.0f;
    private float progressIncrement = 0.05f;
    
    public bool isPressed = false;
    public bool isWaterCollected = false;
    public bool isMiniGameOver = false;
    public bool readyToTransition = false;
    public static bool cleanWaterPanelActive = false;
    public static bool greatJobPanelActive = false;
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
    public void Update()
    {   
        // If bool riverClicked is true...
        if (riverScript.riverClicked) 
        {
            // If the value on the slider component is less than the targetProgress variable...
            if (slider.value < targetProgress)
            {
                CollectWater(); // call CollectWater method
            }
        }

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
    public void StartGame()
    {
        Time.timeScale = 1f; // Unfreeze time
        StartButton.SetActive(false); // Set StartButton to not active
        testingInstructions.SetActive(true); // Enable water testing instructions text
        progressBar.SetActive(true); // Enable Progress bar
        Raycast.isClickable = true; // Set bool isClickable to true

        // If isFirstWaterTestComplete is false...
        if (!isFirstWaterTestComplete)
        {
            firstIntroductionPanel.SetActive(false); // Disable firstIntroductionPanel
        }

        // If isFirstWaterTestComplete is true and isSecondWaterTestComplete is false...
        if (isFirstWaterTestComplete && !isSecondWaterTestComplete)
        {
            secondIntroductionPanel.SetActive(false); // Disable seocndIntroductionPanel
        }
    }


    // On destroy
    private void OnDestroy()
    {
        StartBtn.onClick.RemoveListener(StartGame); // when panel is clicked remove listener StartGame
    }

    private void SwitchUI()
    {
        testingInstructions.SetActive(false); // Disable testing intsructions
        progressBar.SetActive(false); // Disable Progress bar
        poorWaterQualityPanel.SetActive(false); // Disable poor water quality panel
        goodWaterQualityPanel.SetActive(false); // Disable good water quality panel
        cleanWaterIsEssentialPanel.SetActive(false); // Disable clean water is essential panel
        greatJobPanel.SetActive(false); // Disable great job panel
        firstIntroductionPanel.SetActive(false); // Disable first intro panel 
        secondIntroductionPanel.SetActive(false); // Disable first intro panel 

    }

    // Collect water
    public void CollectWater()
    {
        // If left mouse button is down and isPressed is false...
        if (Input.GetMouseButtonDown(0) && !isPressed) 
        {
            isWaterCollected = true; // Set bool waterCollected to true
            isPressed = true; // Set bool isPressed to true
            startTime = Time.time; // startTime is stored
            InvokeRepeating("InvokeProgressBar", waitTime , repeatRate); // add progress to bar at repeatRate (in seconds) after waitTime (in seconds)
            testingInstructions.SetActive(false); // Disable water testing instructions text
        }

        // If left mouse button is down and isPressed is true...
        if (Input.GetMouseButtonUp(0) && isPressed) 
        {
            endTime = Time.time; // endTime is stored
            pressDuration = endTime - startTime; // Calculate duration of each mouse button press
            Debug.Log("Press Duration: " + pressDuration + "seconds"); // Debug.Log the press duration
            isPressed = false; // Set bool isPressed to false
            isWaterCollected = false; // Set bool isWaterCollected to false
            riverScript.riverClicked = false; // Set bool riverClicked to false
            testingInstructions.SetActive(true); // Enable water testing instructions text
            CancelInvoke(); // Cancel all invokes
        }
    }

    // Invoke Progress Bar
    public void InvokeProgressBar()
    {
        progressBarScript.IncrementProgress(progressIncrement); // Add progress to progress bar in increments of progressIncrement
    }

    //// Load trash collection mini game scene
    //public void LoadToTrashCollection()
    //{
    //    SceneManager.LoadScene("Test Transitions (water testing mini game)"); //Load scene "Test Transitions (water testing mini game)"
    //}

    // Load main scene
    public void LoadToMainScene()
    {
        SceneManager.LoadScene("Main Scene"); // Load scene "Main Scene"
    }


    // Method for if water quality us poor
    public void PoorWaterQuality()
    {
        if (!isMiniGameOver)
        {
            poorWaterQualityPanel.SetActive(true);// Enable poor water quality text
            progressBar.SetActive(false);// Disable progress bar
            testingInstructions.SetActive(false); // Disable water testing instructions text
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
    public void GoodWaterQuality()
    {
        // If isMiniGameOver is false...
        if (!isMiniGameOver)
        {
            goodWaterQualityPanel.SetActive(true);// Enable poor water quality panel
            progressBar.SetActive(false);// Disable progress bar
            testingInstructions.SetActive(false); // Disable water testing instructions text
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
    public void ShowCleanWaterPanel()
    {
        cleanWaterIsEssentialPanel.SetActive(true); // Enable clean water is essential panel 
        cleanWaterPanelActive = true; // Set bool cleanWaterPanelActive to true
        poorWaterQualityPanel.SetActive(false); // Disable poor water quality panel
    }

    // Method to show great job panel
    public void ShowGreatJobPanel()
    {
        greatJobPanel.SetActive(true); // Enable great job panel
        greatJobPanelActive = true; // Set bool greatJobPanel to true
        goodWaterQualityPanel.SetActive(false); // Disable good water quality panel
    }

    // Method to end minigame
    public void EndMiniGame()
    {
        // If cleanWaterPanelActive is false and isFirstWaterTestComplete is true...
        if (!cleanWaterPanelActive && isFirstWaterTestComplete)
        {
            Invoke("LoadToMainScene", loadingTime); // Invoke method LoadToMainScene after loadingTime (in seconds)
            Debug.Log("Load to Main Scene"); // Debug.Log message "Load to Main Scene"
            readyToTransition = true;
        }

        // If greatJobActive is false and isSecondWaterTestComplete is true
        if (!greatJobPanelActive && isSecondWaterTestComplete)
        {
            Invoke("LoadToMainScene", loadingTime); // Invoke method LoadToMainScene after loadingTime (in seconds)
            Debug.Log("Load to Main Scene"); // Debug.Log message "Load to Main Scene"
            readyToTransition = true;
        }
    }
}

