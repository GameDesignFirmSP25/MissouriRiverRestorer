using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

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
        // Set Cursor to be visible
        Cursor.visible = true;

        // Enable water testing instructions text
        testingInstructions.SetActive(true);

        // Disable poor water quality text
        poorWaterQualityText.SetActive(false);

        // Disable good water quality text
        goodWaterQualityText.SetActive(false);

        // Get Slider component on Progress Bar
        progressBar = GameObject.Find("Progress Bar");
        slider = progressBar.GetComponent<Slider>();

        if (SampleSceneGameManager.isTrashCollected && SampleSceneGameManager.isFloraPlanted)
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
        if (Input.GetMouseButtonDown(0) && !isPressed) // When left mouse button is down and isPressed is false...
        {
            isWaterCollected = true; // Set bool waterCollected to true
            isPressed = true; // Set bool isPressed to true
            startTime = Time.time; // startTime is stored
            InvokeRepeating("InvokeProgressBar", 0f , 1.0f); // add progress to bar every second 
            testingInstructions.SetActive(false); // Disable water testing instructions text
        }

        if (Input.GetMouseButtonUp(0) && isPressed) // When left mouse button is down and isPressed is true...
        {
            endTime = Time.time; // endTime is stored
            pressDuration = endTime - startTime; // measures how long the mouse was pressed for each click
            //totalTime = totalTime + pressDuration; // total amount of time mouse was pressed
            Debug.Log("Press Duration: " + pressDuration + "seconds"); // Calculate duration of each mouse button press
            //Debug.Log("Total Time Pressed: " + totalTime + "seconds"); // Calculate total time mouse button was pressed
            isPressed = false;
            isWaterCollected = false;
            riverScript.riverClicked = false;
            testingInstructions.SetActive(true); // Enable water testing instructions text
            CancelInvoke(); // cancel all invokes
        }
    }

    // Add progress to progress bar in increments of 0.1
    public void InvokeProgressBar()
    {
        progressBarScript.IncrementProgress(0.1f);
    }

    // Load trash collection mini game scene
    public void LoadToTrashCollection()
    {
        SceneManager.LoadScene("SampleScene");
    }

    //Load main scene
    public void LoadToMainScene()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void PoorWaterQuality()
    {
        poorWaterQualityText.SetActive(true);// Enable poor water quality text
        progressBar.SetActive(false);// Disable progress bar
        testingInstructions.SetActive(false); // Disable water testing instructions text
        Invoke("LoadToTrashCollection", loadingTime);
    }

    public void GoodWaterQuality()
    {
        goodWaterQualityText.SetActive(true);// Enable poor water quality text
        progressBar.SetActive(false);// Disable progress bar
        testingInstructions.SetActive(false); // Disable water testing instructions text
        Invoke("LoadToMainScene", loadingTime);
    }
}

