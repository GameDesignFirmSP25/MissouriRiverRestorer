using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TrashCollectionGame : MonoBehaviour
{
    
    public int GameScore;
    
    public Trashcast trashcastInstance;
    public Button StartBtn;
    public GameObject Panel;
    public GameObject StartButton;
    void Start() // Start is called once before the first execution of Update after the MonoBehaviour is created
    {
        
        GameScore = 30;
        GameObject panel = GetComponent<GameObject>();
        panel.SetActive(true);
        GameObject startButton = GetComponent<GameObject>();
        startButton.SetActive(true);
        Button Strt = StartBtn.GetComponent<Button>();
        Strt.onClick.AddListener(StartGame);
    }

    
    void Update()// Update is called once per frame
    {
        
    }
    public void StartGame()
    {
        StartButton.SetActive(false);
        Panel.SetActive(false);
        if (Trashcast.playerScore == GameScore)
            {
                Debug.Log("Trash Collected");
            }
    }
}
