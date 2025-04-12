using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour
{
    public Button StrtButton;
    public Button ExitButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Button startGame= StrtButton.GetComponent<Button>();
        startGame.onClick.AddListener(StartGameButton);
        Button exitGame = ExitButton.GetComponent<Button>();
        exitGame.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void StartGameButton()
    {
        Debug.Log("Button CLicked");
        SceneManager.LoadScene(0);
    }
    void Exit()
    {
        Debug.Log("Exit Button CLicked");
        Application.Quit();
    }
}
