using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TrashCollectionGame : MonoBehaviour
{
    
    public int GameScore;
    public bool isgameComplete = false;
    public Trashcast trashcast;
    public Button StartBtn;
    public GameObject Panel;
    public GameObject StartButton;
    void Start() // Start is called once before the first execution of Update after the MonoBehaviour is created
    {
        Time.timeScale = 0f;
        
        GameObject panel = GetComponent<GameObject>();
        panel.SetActive(true);
        GameObject startButton = GetComponent<GameObject>();
        startButton.SetActive(true);
        Button Strt = StartBtn.GetComponent<Button>();
        Strt.onClick.AddListener(StartGame);
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
            }
    }
}
