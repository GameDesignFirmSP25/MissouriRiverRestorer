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
    private GameObject poorWaterQualityText;

    [SerializeField]
    private GameObject goodWaterQualityText;

    [SerializeField]
    private GameObject progressBar;

    private Slider slider;
    public Raycast riverScript;
    public ProgressBar progressBarScript;

    private float startTime;
    private float endTime;
    private float pressDuration;
    private float targetProgress = 1f;
    private float loadingTime = 3f;

    public bool isPressed = false;
    public static bool isWaterQualityGood = false;
    public static bool isWaterCollected = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = true; // Set Cursor to be visible
        testingInstructions.SetActive(true); // Enable water testing instructions text
        poorWaterQualityText.SetActive(false); // Disable poor water quality text
        goodWaterQualityText.SetActive(false); // Disable good water quality text

        // Get Slider component on Progress Bar
        progressBar = GameObject.Find("Progress Bar");
        slider = progressBar.GetComponent<Slider>();

        if (TestTransitionsGameManager.isTrashCollected && TestTransitionsGameManager.isFloraPlanted)
        {
            isWaterQualityGood = true;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        // call RiverClicked method from Raycast.cs
        riverScript.RiverClicked();
        
        // if RiverClicked method runs...
        if (riverScript.riverClicked) 
        {
            // if the value on the slider component is less than the targetProgress variable...
            if (slider.value < targetProgress)
            {
                CollectWater(); // call CollectWater method
            }
        }

        // if slider value is equal to targetProgress variable and bool isWaterQualityGood is false...
        if (slider.value == targetProgress && !isWaterQualityGood)
        {
            PoorWaterQuality();
        }

        // if slider value is equal to targetProgress variable and bool isWaterQuality...
        if (slider.value == targetProgress && isWaterQualityGood)
        {
            GoodWaterQuality();
        }
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
            InvokeRepeating("InvokeProgressBar", 0f , 1.0f); // add progress to bar every second 
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
        progressBarScript.IncrementProgress(0.1f); // Add progress to progress bar in increments of 0.1
    }

    // Load trash collection mini game scene
    public void LoadToTrashCollection()
    {
        SceneManager.LoadScene("Test Transitions (water testing mini game)"); //Load scene "Test Transitions (water testing mini game)"
    }

    // Load main scene
    public void LoadToMainScene()
    {
        SceneManager.LoadScene("Main Scene"); // Load scene "Main Scene"
    }

    public void PoorWaterQuality()
    {
        poorWaterQualityText.SetActive(true);// Enable poor water quality text
        progressBar.SetActive(false);// Disable progress bar
        testingInstructions.SetActive(false); // Disable water testing instructions text
        Invoke("LoadToTrashCollection", loadingTime); // Invoke method LoadToTrashCollection after loadingTime (in seconds)
    }

    public void GoodWaterQuality()
    {
        goodWaterQualityText.SetActive(true);// Enable poor water quality text
        progressBar.SetActive(false);// Disable progress bar
        testingInstructions.SetActive(false); // Disable water testing instructions text
        Invoke("LoadToMainScene", loadingTime); // Invoke method LoadToMainScene after loadingTime (in seconds)
    }
}

