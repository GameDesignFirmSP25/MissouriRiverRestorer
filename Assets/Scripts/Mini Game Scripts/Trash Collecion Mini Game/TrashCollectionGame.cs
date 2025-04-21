using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.Mathematics;
using System;
using Unity.VisualScripting;


public class TrashCollectionGame : BaseMiniGameManager
{
    
    public int GameScore;
    public bool isgameComplete = false;
    public Trashcast trashcast;
    
    public Button StartBtn;
    public GameObject Panel;
    public GameObject StartButton;

    public Button endbtn;
    public GameObject EndButton;
    public GameObject Finishpanel1;

    

    
    public static bool trashCollected = false; // global variable to check if trash is collected

    [Header("Objective Panels")]
    public GameObject objectiveScupPanel;
    public GameObject objectiveGasCanPanel;
    public GameObject objectivePizzaSlicePanel;
    public GameObject objectiveTrashBagPanel;
    public GameObject objectiveBottlePanel;
    public GameObject objectiveSaveBirdPanel;
    public GameObject objectiveSaveFishPanel;
    public GameObject objectiveSaveDeerPanel;
    [Header("Objective text")]
    [SerializeField]
    TextMeshProUGUI objectiveScupText;
    [SerializeField]
    TextMeshProUGUI objectiveGasCanText;
    [SerializeField]
    TextMeshProUGUI objectivePizzaSliceText;
    [SerializeField]
    TextMeshProUGUI objectiveTrashBagText;
    [SerializeField]
    TextMeshProUGUI objectiveBottleText;
    [SerializeField]
    TextMeshProUGUI objectiveSaveBirdText;
    [SerializeField]
    TextMeshProUGUI objectiveSaveFishText;
    [SerializeField]
    TextMeshProUGUI objectiveSaveDeerText;

    [Header("Bools")]
    public static bool ObjectveScup = false;
    public static bool ObjectvGasCan = false;
    public static bool ObjectvPizzaSlice = false;
    public static bool ObjectvTrashBag = false;
    public static bool ObjectvBottle = false;
    public static bool ObjectvSaveBird = false;
    public static bool ObjectvSaveFish = false;
    public static bool ObjectvSaveDeer = false;

    [Header("ObjectiveText")]
    public TextMeshProUGUI ObjectiveScuptext;
    public TextMeshProUGUI ObjectiveGasCantext;
    public TextMeshProUGUI ObjectivePizzaSlicetext;
    public TextMeshProUGUI ObjectiveTrashBagtext;
    public TextMeshProUGUI ObjectiveBottletext;
    public TextMeshProUGUI ObjectiveSaveBirdtext;
    public TextMeshProUGUI ObjectiveSaveFishtext;
    public TextMeshProUGUI ObjectiveSaveDeertext;

    void Start() // Start is called once before the first execution of Update after the MonoBehaviour is created
    {
        GameScore = 8;
        Time.timeScale = 0f;
        Finishpanel1.SetActive(false);

        EndButton.SetActive(false);
        Panel.SetActive(true);
        StartButton.SetActive(true);
        StartBtn.onClick.AddListener(StartGame);
        if (trashcast.CollectedTrash >= GameScore && !isgameComplete)
        {
            gameCompleteScore();
        }
    }

     private void OnDestroy()
     {
          StartBtn.onClick.RemoveListener(StartGame);

         
     }

     void Update()// Update is called once per frame
    {
        if (IsAnyObjectivePanelOpen())
        {
            return; // Do not execute the rest of the Update method if any objective panel is open
        }
        if (trashcast.playerScore >= GameScore && !isgameComplete )
        {
            gameCompleteScore();
        }
    }
    private bool IsAnyObjectivePanelOpen()
    {
        return objectiveScupPanel.activeSelf ||
               objectiveGasCanPanel.activeSelf ||
               objectivePizzaSlicePanel.activeSelf ||
               objectiveTrashBagPanel.activeSelf ||
               objectiveBottlePanel.activeSelf ||
               objectiveSaveBirdPanel.activeSelf ||
               objectiveSaveFishPanel.activeSelf ||
               objectiveSaveDeerPanel.activeSelf;
    }
    public void StartGame()
    {
        Time.timeScale = 1f;
        GameScore = 8;
        StartButton.SetActive(false);
        Panel.SetActive(false);
       
    }

    public void gameCompleteScore()
    {
                Time.timeScale = 0f;
                Debug.Log("Trash Collected");
                isgameComplete = true;
                trashCollected = true; // set the global variable to true
                // add panel to pop up
                
                Finishpanel1.SetActive(true);// sets panel active
                EndButton.SetActive(true);


        // For Game Progression
        TriggerMiniGameCompleteEvent(0); // Can add a score pass through here
    }
    public void Home() 
    {
        endbtn.onClick.RemoveListener(Home);
        SceneManager.LoadScene("Overworld");

    }
    public void strikethrough()
    {
        if (ObjectveScup)
        {
            Debug.Log("Stryofoam cup Striked");
            ObjectiveScuptext.fontStyle = FontStyles.Strikethrough;
        }
        if (ObjectvGasCan)
        {
            Debug.Log("Gas Can Striked");
            ObjectiveGasCantext.fontStyle = FontStyles.Strikethrough;
        }
        if (ObjectvPizzaSlice)
        {
            Debug.Log("Pizza Slice Striked");
            ObjectivePizzaSlicetext.fontStyle = FontStyles.Strikethrough;
        }
        if (ObjectvTrashBag)
        {
            Debug.Log("Trash Bag Striked");
            ObjectiveTrashBagtext.fontStyle = FontStyles.Strikethrough;
        }
        if (ObjectvBottle)
        {
            Debug.Log("Bottle Striked");
            ObjectiveBottletext.fontStyle = FontStyles.Strikethrough;
        }
        if (ObjectvSaveBird)
        {
            Debug.Log("Save Bird Striked");
            ObjectiveSaveBirdtext.fontStyle = FontStyles.Strikethrough;
        }
        if (ObjectvSaveFish)
        {
            Debug.Log("Save Fish Striked");
            ObjectiveSaveFishtext.fontStyle = FontStyles.Strikethrough;
        }
        if (ObjectvSaveDeer)
        {
            Debug.Log("Save Deer Striked");
            ObjectiveSaveDeertext.fontStyle = FontStyles.Strikethrough;
        }
    }

}
