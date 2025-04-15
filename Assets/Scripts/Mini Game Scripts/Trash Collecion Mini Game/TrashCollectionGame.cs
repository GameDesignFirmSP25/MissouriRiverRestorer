using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.Mathematics;
using System;


public class TrashCollectionGame : MonoBehaviour
{
    
    public int GameScore;
    public bool isgameComplete = false;
    public Trashcast trashcast;
    
    public Button StartBtn;
    public GameObject Panel;
    public GameObject StartButton;

    public Button endbtn;
    public GameObject EndButton;
    public GameObject Finishpanel2;
    public GameObject Finishpanel1;

    
    public static bool trashCollected = false; // global variable to check if trash is collected

    
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float RemainingTime;
    [SerializeField] TextMeshProUGUI playerScore;

    void Start() // Start is called once before the first execution of Update after the MonoBehaviour is created
    {
        Time.timeScale = 0f;
        Finishpanel1.SetActive(false);
        Finishpanel2.SetActive(false);
        EndButton.SetActive(false);
        Panel.SetActive(true);
        StartButton.SetActive(true);
        StartBtn.onClick.AddListener(StartGame);
    }

     private void OnDestroy()
     {
          StartBtn.onClick.RemoveListener(StartGame);
     }

     void Update()// Update is called once per frame
    {
        RemainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(RemainingTime / 60);
        int seconds = Mathf.FloorToInt(RemainingTime % 60);
        timerText.text = string.Format("{00:00} : {1:00}", minutes, seconds);

        playerScore.text = $"{trashcast.CollectedTrash} / {GameScore}";


        gameComplete();
    }
    public void StartGame()
    {
        Time.timeScale = 1f;
        GameScore = 30;
        StartButton.SetActive(false);
        Panel.SetActive(false);
       
    }
    public void gameComplete()
    {
         if (trashcast.CollectedTrash >= 20 &&! isgameComplete)
         {
            Debug.Log("Good Enough");
                isgameComplete = true;
                trashCollected = true; // set the global variable to true
                // add panel to pop up
            EndButton.SetActive(true);// sets button active
                Finishpanel1.SetActive(true);// sets panel active
            endbtn.onClick.AddListener(Home);
         }
         if (trashcast.CollectedTrash >= GameScore &&! isgameComplete)
            {
                Debug.Log("Trash Collected");
                isgameComplete = true;
                trashCollected = true; // set the global variable to true
                // add panel to pop up
            EndButton.SetActive(true);// sets button active
                Finishpanel2.SetActive(true);// sets panel active
            endbtn.onClick.AddListener(Home);

            }
         else 
         {
            isgameComplete = false;
            trashCollected = false;
            // add panel to pop up
            
         }
      
    }
    public void Home() 
    {
        endbtn.onClick.RemoveListener(Home);
        SceneManager.LoadScene(0);

    }

}
