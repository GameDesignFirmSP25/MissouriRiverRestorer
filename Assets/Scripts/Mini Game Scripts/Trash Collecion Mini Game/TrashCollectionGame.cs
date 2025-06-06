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
    [Header("Scores")]
    public int CollectedTrash;
    public int GameScore;

    [Header("Script References")]
    public Trashcast trashcast;

    [Header("UI Elements")]
    public Button StartBtn;
    public GameObject Panel;
    public GameObject StartButton;

    public Button endbtn;
    public GameObject EndButton;
    public GameObject Finishpanel1;
    
    
    [Header("Objective Panels")]
    public GameObject objectiveScupPanel;
    public GameObject objectiveGasCanPanel;
    public GameObject objectivePizzaSlicePanel;
    public GameObject objectiveTrashBagPanel;
    public GameObject objectiveBottlePanel;
    public GameObject objectiveSaveBirdPanel;
    public GameObject objectiveSaveFishPanel;
    public GameObject objectiveSaveDeerPanel;

    [Header("Objective panel text")]
    public GameObject objectiveScupPanelText;
    public GameObject objectiveGasCanPanelText;
    public GameObject objectivePizzaSlicePanelText;
    public GameObject objectiveTrashBagPanelText;
    public GameObject objectiveBottlePanelText;
    public GameObject objectiveSaveBirdPanelText;
    public GameObject objectiveSaveFishPanelText;
    public GameObject objectiveSaveDeerPanelText;

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
    public bool aPanelIsActive = false; // to check if any panel is active
    public bool isgameComplete = false;
    private bool waitForNextEPress = false;
    private bool eKeyWasDownLastFrame = false;
    public static bool ObjectveScup = false;
    public static bool ObjectvGasCan = false;
    public static bool ObjectvPizzaSlice = false;
    public static bool ObjectvTrashBag = false;
    public static bool ObjectvBottle = false;
    public static bool ObjectvSaveBird = false;
    public static bool ObjectvSaveFish = false;
    public static bool ObjectvSaveDeer = false;
    public static bool trashCollected = false; // global variable to check if trash is collected


    [Header("ObjectiveText")]
    public TextMeshProUGUI ObjectiveScuptext;
    public TextMeshProUGUI ObjectiveGasCantext;
    public TextMeshProUGUI ObjectivePizzaSlicetext;
    public TextMeshProUGUI ObjectiveTrashBagtext;
    public TextMeshProUGUI ObjectiveBottletext;
    public TextMeshProUGUI ObjectiveSaveBirdtext;
    public TextMeshProUGUI ObjectiveSaveFishtext;
    public TextMeshProUGUI ObjectiveSaveDeertext;

    [Header("Audio")]
    [SerializeField]
    private SFXMaker interactButton;

    [SerializeField]
    private SFXMaker trashGrabbed;

    [SerializeField]
    private SFXMaker animalSaved;

    void Start() // Start is called once before the first execution of Update after the MonoBehaviour is created
    {
        CollectedTrash = 0;
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
            CheckForEPress(); // Check if any objective panel is open and handle input accordingly
            return; // Do not execute the rest of the Update method if any objective panel is open
        }
        if (CollectedTrash >= GameScore && !isgameComplete )
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
        PlayButtonClick();
        Panel.SetActive(false);
       
    }

    private void CheckForEPress()
    {
        bool eKeyDown = Input.GetKeyDown(KeyCode.E);

        if (waitForNextEPress)
        {
            if (!Input.GetKey(KeyCode.E))
            {
                waitForNextEPress = false; // Now allow closing on next E press
            }
            return;
        }

        if (eKeyDown)
        {
            CloseObjectivePanel();
            waitForNextEPress = true;
            PlayButtonClick();
        }
    }

    public void CloseObjectivePanel()
    {
        Time.timeScale = 1f; // set the time scale to normal
        GameObject Objectivepanels = GameObject.FindGameObjectWithTag("Objective Panels");
        GameObject ObjectivepanelsText = GameObject.FindGameObjectWithTag("Objective Panels TXT");
        Objectivepanels.SetActive(false); // set the panel to inactive
        ObjectivepanelsText.SetActive(false); // set the panel text to inactive
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
          Time.timeScale = 1f;
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

    public void PlayButtonClick()
    {
        interactButton.PlaySound();
    }

    public void BottleClicked()
    {
        Time.timeScale = 0f; // Pause the game when the bottle is clicked
        CollectedTrash++; // Increment the collected trash count
        Debug.Log($"Player Score: {CollectedTrash} trash collected!");
        ObjectvBottle = true; // Set the boolean to true when the bottle is clicked
        objectiveBottlePanel.SetActive(true); // set the panel to active
        objectiveBottlePanelText.SetActive(true); // set the panel text to active
        strikethrough(); // Call the strikethrough method to update the text style
        waitForNextEPress = true;

        // Find the gameobject named "recycles_icon" and disable it
        GameObject icon = GameObject.Find("recycles_icon");
        GameObject bottle = GameObject.Find("Bottle");

        if (icon != null)
        {
            icon.SetActive(false); // Disable the icon to hide it
        }
        else
        {
            Debug.LogWarning("Icon not found on the bottle object.");
        }

        if (bottle != null)
        {
            bottle.SetActive(false); // Disable the bottle to hide it
        }
        else
        {
            Debug.LogWarning("Bottle not found on the bottle object.");
        }
    }

    public void TrashBagClicked()
    {
        Time.timeScale = 0f; // Pause the game when the trash bag is clicked
        CollectedTrash++; // Increment the collected trash count
        Debug.Log($"Player Score: {CollectedTrash} trash collected!");
        ObjectvTrashBag = true; // Set the boolean to true when the trash bag is clicked
        objectiveTrashBagPanel.SetActive(true); // set the panel to active
        objectiveTrashBagPanelText.SetActive(true); // set the panel text to active
        strikethrough(); // Call the strikethrough method to update the text style
        waitForNextEPress = true;

        // Find the gameobject named "trash_bag_icon" and disable it
        GameObject icon = GameObject.Find("trash_bag_icon");
        GameObject trashBag = GameObject.Find("Trash Bag"); 

        if (icon != null)
        {
            icon.SetActive(false); // Disable the icon to hide it
        }
        else
        {
            Debug.LogWarning("Icon not found on the trash bag object.");
        }

        if (trashBag != null)
        {
            trashBag.SetActive(false); // Disable the trash bag to hide it
        }
        else
        {
            Debug.LogWarning("Trash Bag not found on the trash bag object.");
        }
    }

    public void GasCanisterClicked()
    {
        Time.timeScale = 0f; // Pause the game when the trash bag is clicked
        CollectedTrash++; // Increment the collected trash count
        Debug.Log($"Player Score: {CollectedTrash} trash collected!");
        ObjectvGasCan = true; // Set the boolean to true when the gas canister is clicked
        objectiveGasCanPanel.SetActive(true); // set the panel to active
        objectiveGasCanPanelText.SetActive(true); // set the panel text to active
        strikethrough(); // Call the strikethrough method to update the text style
        waitForNextEPress = true;

        // Find the gameobject named "oil_spill_icon 1" and disable it
        GameObject icon = GameObject.Find("oil_spill_icon 1");
        GameObject gasCan = GameObject.Find("Gas Can");

        if (icon != null)
        {
            icon.SetActive(false); // Disable the icon to hide it
        }
        else
        {
            Debug.LogWarning("Icon not found on the gas can object.");
        }

        if (gasCan != null)
        {
            gasCan.SetActive(false); // Disable the gas can to hide it
        }
        else
        {
            Debug.LogWarning("Gas Can not found on the gas can object.");
        }
    }

    public void StyrofoamCupClicked()
    {
        Time.timeScale = 0f; // Pause the game when the trash bag is clicked
        CollectedTrash++; // Increment the collected trash count
        Debug.Log($"Player Score: {CollectedTrash} trash collected!");
        ObjectveScup = true; // Set the boolean to true when the styrofoam cup is clicked
        objectiveScupPanel.SetActive(true); // set the panel to active
        objectiveScupPanelText.SetActive(true); // set the panel text to active
        strikethrough(); // Call the strikethrough method to update the text style
        waitForNextEPress = true;

        // Find the gameobject named "trash_icon" and disable it
        GameObject icon = GameObject.Find("trash_icon");
        GameObject styrofoamCup = GameObject.Find("Styrofoam Cup");

        if (icon != null)
        {
            icon.SetActive(false); // Disable the icon to hide it
        }
        else
        {
            Debug.LogWarning("Icon not found on the styrofoam cup object.");
        }

        if (styrofoamCup != null)
        {
            styrofoamCup.SetActive(false); // Disable the styrofoam cup to hide it
        }
        else
        {
            Debug.LogWarning("Styrofoam Cup not found on the styrofoam cup object.");
        }
    }

    public void SaveRaccoonClicked()
    {
        Time.timeScale = 0f; // Pause the game when the trash bag is clicked
        CollectedTrash++; // Increment the collected trash count
        Debug.Log($"Player Score: {CollectedTrash} trash collected!");
        ObjectvPizzaSlice = true; // Set the boolean to true when the raccoon is saved
        objectivePizzaSlicePanel.SetActive(true); // set the panel to active
        objectivePizzaSlicePanelText.SetActive(true); // set the panel text to active
        strikethrough(); // Call the strikethrough method to update the text style
        waitForNextEPress = true;

        // Find the gameobject named "sick_raccoon_icon" and disable it
        GameObject icon = GameObject.Find("sick_raccoon_icon");
        GameObject pizzaSlice = GameObject.Find("Pizza Slice");

        if (icon != null)
        {
            icon.SetActive(false); // Disable the icon to hide it
        }
        else
        {
            Debug.LogWarning("Icon not found on the save raccoon object.");
        }

        if (pizzaSlice != null)
        {
            pizzaSlice.SetActive(false); // Disable the pizza slice to hide it
        }
        else
        {
            Debug.LogWarning("Pizza Slice not found on the save raccoon object.");
        }
    }

    public void SaveDeerClicked()
    {
        Time.timeScale = 0f; // Pause the game when the trash bag is clicked
        CollectedTrash++; // Increment the collected trash count
        Debug.Log($"Player Score: {CollectedTrash} trash collected!");
        ObjectvSaveDeer = true; // Set the boolean to true when the deer is saved
        objectiveSaveDeerPanel.SetActive(true); // set the panel to active
        objectiveSaveDeerPanelText.SetActive(true); // set the panel text to active
        strikethrough(); // Call the strikethrough method to update the text style
        waitForNextEPress = true;

        // Find the gameobject named "tire_icon" and disable it
        GameObject icon = GameObject.Find("tire_icon");
        GameObject tire = GameObject.Find("Tire");

        if (icon != null)
        {
            icon.SetActive(false); // Disable the icon to hide it
        }
        else
        {
            Debug.LogWarning("Icon not found on the save deer object.");
        }

        if (tire != null)
        {
            tire.SetActive(false); // Disable the tire to hide it
        }
        else
        {
            Debug.LogWarning("Tire not found on the save deer object.");
        }
    }

    public void SaveBirdClicked()
    {
        Time.timeScale = 0f; // Pause the game when the trash bag is clicked
        CollectedTrash++; // Increment the collected trash count
        Debug.Log($"Player Score: {CollectedTrash} trash collected!");
        ObjectvSaveBird = true; // Set the boolean to true when the bird is saved
        objectiveSaveBirdPanel.SetActive(true); // set the panel to active
        objectiveSaveBirdPanelText.SetActive(true); // set the panel text to active
        strikethrough(); // Call the strikethrough method to update the text style
        waitForNextEPress = true;

        // Find the gameobject named "net_icon" and disable it
        GameObject icon = GameObject.Find("net_icon");
        GameObject net = GameObject.Find("Bird trapping item");

        if (icon != null)
        {
            icon.SetActive(false); // Disable the icon to hide it
        }
        else
        {
            Debug.LogWarning("Icon not found on the save bird object.");
        }

        if (net != null)
        {
            net.SetActive(false); // Disable the net to hide it
        }
        else
        {
            Debug.LogWarning("Net not found on the save bird object.");
        }
    }

    public void SaveFishClicked()
    {
        Time.timeScale = 0f; // Pause the game when the trash bag is clicked
        CollectedTrash++; // Increment the collected trash count
        Debug.Log($"Player Score: {CollectedTrash} trash collected!");
        ObjectvSaveFish = true; // Set the boolean to true when the fish is saved
        objectiveSaveFishPanel.SetActive(true); // set the panel to active
        objectiveSaveFishPanelText.SetActive(true); // set the panel text to active
        strikethrough(); // Call the strikethrough method to update the text style
        waitForNextEPress = true;

        // Find the gameobject named "oil_spill_icon 2" and disable it
        GameObject icon = GameObject.Find("oil_spill_icon 2");
        GameObject gasCanSpilling = GameObject.Find("Gas Can Spilling");

        if (icon != null)
        {
            icon.SetActive(false); // Disable the icon to hide it
        }
        else
        {
            Debug.LogWarning("Icon not found on the save fish object.");
        }

        if (gasCanSpilling != null)
        {
            gasCanSpilling.SetActive(false); // Disable the gas can spilling to hide it
        }
        else
        {
            Debug.LogWarning("Gas Can Spilling not found on the save fish object.");
        }
    }
}
