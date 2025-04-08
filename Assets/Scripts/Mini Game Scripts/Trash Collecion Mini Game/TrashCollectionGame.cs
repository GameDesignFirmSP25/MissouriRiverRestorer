using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject Finishpanel;
    void Start() // Start is called once before the first execution of Update after the MonoBehaviour is created
    {
        Time.timeScale = 0f;
        Finishpanel.SetActive(false);
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
         if (trashcast.playerScore >= GameScore &&! isgameComplete)
            {
                Debug.Log("Trash Collected");
                isgameComplete = true;
                // add panel to pop up
                EndButton.SetActive(true);// sets button active
                Finishpanel.SetActive(true);// sets panel active
            endbtn.onClick.AddListener(Home);

            }
      
    }
    public void Home() 
    {
        endbtn.onClick.RemoveListener(Home);
        SceneManager.LoadScene(0);

    }

}
