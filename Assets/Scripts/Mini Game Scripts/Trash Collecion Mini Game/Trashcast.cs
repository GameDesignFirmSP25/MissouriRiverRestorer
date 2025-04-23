using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Trashcast : MonoBehaviour
{
    public TrashCollectionGame trashCollectionGame;
    [Header("Scores")]
    public int playerScore;
    public int CollectedTrash;

    [Header("Objective Panels")]
    public GameObject ObjectiveScupPanel;
    public GameObject ObjectiveGasCanPanel;
    public GameObject ObjectivePizzaSlicePanel;
    public GameObject ObjectiveTrashBagPanel;
    public GameObject ObjectiveBottlePanel;
    public GameObject ObjectiveSaveBirdPanel;
    public GameObject ObjectiveSaveFishPanel;
    public GameObject ObjectiveSaveDeerPanel;

    [Header("Objective panel text")]
    public GameObject ObjectiveScupPanelText;
    public GameObject ObjectiveGasCanPanelText;
    public GameObject ObjectivePizzaSlicePanelText;
    public GameObject ObjectiveTrashBagPanelText;
    public GameObject ObjectiveBottlePanelText;
    public GameObject ObjectiveSaveBirdPanelText;
    public GameObject ObjectiveSaveFishPanelText;
    public GameObject ObjectiveSaveDeerPanelText;
    [Header("panel close Button")]
    public GameObject ObjectivePanelCloseButton;
    public Button ObjectivePanelCloseButton1;
    void Start()
    {
        playerScore = 0;
    }
    void Update()
    {
        // Check if the player clicks
        if (Input.GetMouseButtonDown(0)) // 0 = Left Click
        {
            // Perform Raycast from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {


                if (hit.collider.CompareTag("Trash: Styrofoam Cup")&& !TrashCollectionGame.ObjectveScup) 
                {
                    Time.timeScale = 0f; // set the time scale to 0
                    Debug.Log("Stryofoam cup clicked");
                    Destroy(hit.collider.gameObject);// takes trash object off of the screen
                    CollectedTrash++; // adds score for styrofoam cup
                    Debug.Log("playerScore: " + CollectedTrash);
                    TrashCollectionGame.ObjectveScup = true; // set the bool to true
                    ObjectiveScupPanel.SetActive(true); // set the panel to active
                    ObjectiveScupPanelText.SetActive(true); // set the panel text to active
                    ObjectivePanelCloseButton.SetActive(true); // set the panel close button to active
                    ObjectivePanelCloseButton1.onClick.AddListener(CloseObjectivePanel); // add listener to the close button
                    trashCollectionGame.strikethrough(); // call the strikethrough function
                }
                if (hit.collider.CompareTag("Trash: Bottle") && !TrashCollectionGame.ObjectvBottle)
                {
                    Time.timeScale = 0f; // set the time scale to 0
                    Debug.Log("Bottle clicked");
                    Destroy(hit.collider.gameObject);// takes trash object off of the screen
                    CollectedTrash++; // adds score for bottle 
                    Debug.Log("playerScore: " + CollectedTrash);
                    TrashCollectionGame.ObjectvBottle = true; // set the bool to true
                    ObjectiveBottlePanel.SetActive(true); // set the panel to active
                    ObjectiveBottlePanelText.SetActive(true); // set the panel text to active
                    ObjectivePanelCloseButton.SetActive(true); // set the panel close button to active
                    ObjectivePanelCloseButton1.onClick.AddListener(CloseObjectivePanel); // add listener to the close button

                    Debug.Log("playerScore: " + CollectedTrash);
                    trashCollectionGame.strikethrough(); // call the strikethrough function
                }
                if (hit.collider.CompareTag("Trash: trash bag")&& !TrashCollectionGame.ObjectvTrashBag)
                {
                    Time.timeScale = 0f; // set the time scale to 0
                    Debug.Log("Trash bag clicked");
                    Destroy(hit.collider.gameObject);// takes trash object off of the screen
                    CollectedTrash++; // adds score for trash bag
                    Debug.Log("playerScore: " + CollectedTrash);
                    TrashCollectionGame.ObjectvTrashBag = true; // set the bool to true
                    ObjectiveTrashBagPanel.SetActive(true); // set the panel to active
                    ObjectiveTrashBagPanelText.SetActive(true); // set the panel text to active
                    ObjectivePanelCloseButton.SetActive(true); // set the panel close button to active
                    ObjectivePanelCloseButton1.onClick.AddListener(CloseObjectivePanel); // add listener to the close button
                    trashCollectionGame.strikethrough(); // call the strikethrough function
                }
                if (hit.collider.CompareTag("Trash: Pizza Slice")&& !TrashCollectionGame.ObjectvPizzaSlice)
                {
                    Time.timeScale = 0f; // set the time scale to 0
                    Debug.Log("Pizza slice clicked");
                    Destroy(hit.collider.gameObject);// takes trash object off of the screen
                    CollectedTrash++; // adds score for pizza slice
                    Debug.Log("playerScore: " + CollectedTrash);
                    TrashCollectionGame.ObjectvPizzaSlice = true; // set the bool to true
                    ObjectivePizzaSlicePanel.SetActive(true); // set the panel to active
                    ObjectivePizzaSlicePanelText.SetActive(true); // set the panel text to active
                    ObjectivePanelCloseButton.SetActive(true); // set the panel close button to active
                    ObjectivePanelCloseButton1.onClick.AddListener(CloseObjectivePanel); // add listener to the close button
                    trashCollectionGame.strikethrough(); // call the strikethrough function
                }
                if (hit.collider.CompareTag("Trash: gas can")&& !TrashCollectionGame.ObjectvGasCan)
                {
                    Time.timeScale = 0f; // set the time scale to 0
                    Debug.Log("Gas can clicked");
                    Destroy(hit.collider.gameObject);// takes trash object off of the screen
                    CollectedTrash++; // adds score for gas can
                    Debug.Log("playerScore: " + CollectedTrash);
                    TrashCollectionGame.ObjectvGasCan = true; // set the bool to true
                    ObjectiveGasCanPanel.SetActive(true); // set the panel to active
                    ObjectiveGasCanPanelText.SetActive(true); // set the panel text to active
                    ObjectivePanelCloseButton.SetActive(true); // set the panel close button to active
                    ObjectivePanelCloseButton1.onClick.AddListener(CloseObjectivePanel); // add listener to the close button
                    trashCollectionGame.strikethrough(); // call the strikethrough function
                }
                if (hit.collider.CompareTag("Save bird") && !TrashCollectionGame.ObjectvSaveBird)
                {
                    Time.timeScale = 0f; // set the time scale to 0
                    Debug.Log("Save bird clicked");
                    Destroy(hit.collider.gameObject);// takes trash object off of the screen
                    CollectedTrash++; // adds score for save bird
                    Debug.Log("playerScore: " + CollectedTrash);
                    TrashCollectionGame.ObjectvSaveBird = true; // set the bool to true
                    ObjectiveSaveBirdPanel.SetActive(true); // set the panel to active
                    ObjectiveSaveBirdPanelText.SetActive(true); // set the panel text to active
                    ObjectivePanelCloseButton.SetActive(true); // set the panel close button to active
                    ObjectivePanelCloseButton1.onClick.AddListener(CloseObjectivePanel); // add listener to the close button
                    trashCollectionGame.strikethrough(); // call the strikethrough function
                }
                if (hit.collider.CompareTag("Save fish") && !TrashCollectionGame.ObjectvSaveFish)
                {
                    Time.timeScale = 0f; // set the time scale to 0
                    Debug.Log("Save fish clicked");
                    Destroy(hit.collider.gameObject);// takes trash object off of the screen
                    CollectedTrash++; // adds score for save fish
                    Debug.Log("playerScore: " + CollectedTrash);
                    TrashCollectionGame.ObjectvSaveFish = true; // set the bool to true
                    ObjectiveSaveFishPanel.SetActive(true); // set the panel to active
                    ObjectiveSaveFishPanelText.SetActive(true); // set the panel text to active
                    ObjectivePanelCloseButton.SetActive(true); // set the panel close button to active
                    ObjectivePanelCloseButton1.onClick.AddListener(CloseObjectivePanel); // add listener to the close button
                    trashCollectionGame.strikethrough(); // call the strikethrough function
                }
                if (hit.collider.CompareTag("Save deer") && !TrashCollectionGame.ObjectvSaveDeer)
                {
                    Time.timeScale = 0f; // set the time scale to 0
                    Debug.Log("Save deer clicked");
                    Destroy(hit.collider.gameObject);// takes trash object off of the screen
                    CollectedTrash++; // adds score for save deer
                    Debug.Log("playerScore: " + playerScore);
                    TrashCollectionGame.ObjectvSaveDeer = true; // set the bool to true
                    ObjectiveSaveDeerPanel.SetActive(true); // set the panel to active
                    ObjectiveSaveDeerPanelText.SetActive(true); // set the panel text to active
                    ObjectivePanelCloseButton.SetActive(true); // set the panel close button to active
                    ObjectivePanelCloseButton1.onClick.AddListener(CloseObjectivePanel); // add listener to the close button
                    trashCollectionGame.strikethrough(); // call the strikethrough function
                }
            }
        }
    }
    public void CloseObjectivePanel()
    {
        Time.timeScale = 1f; // set the time scale to normal
        GameObject Objectivepanels = GameObject.FindGameObjectWithTag("Objective Panels");
        GameObject ObjectivepanelsText = GameObject.FindGameObjectWithTag("Objective Panels TXT");
        Objectivepanels.SetActive(false); // set the panel to inactive
        ObjectivepanelsText.SetActive(false); // set the panel text to inactive
        ObjectivePanelCloseButton.SetActive(false); // set the panel close button to inactive
        ObjectivePanelCloseButton1.onClick.RemoveListener(CloseObjectivePanel); // remove listener from the close button

    }
}