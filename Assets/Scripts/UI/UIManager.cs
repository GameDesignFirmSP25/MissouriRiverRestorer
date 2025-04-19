using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //public PlayerController player;
    //public GameManager GameManager;

    public PauseMenu pauseMenu;
    public VictoryMenu victoryMenu;

    public bool isPaused = false;

    private void Awake()
    {
        //player.m_Controller.StartPressed += OnStartPressed;
        //GameManager.VictoryEvent += OnVictory;
    }

    private void OnDisable()
    {
        //player.m_Controller.StartPressed -= OnStartPressed;
        //GameManager.VictoryEvent -= OnVictory;
    }

    private void Start()
    {

    }



    private void OnStartPressed()
    {
        pauseMenu.TogglePause();
    }


    private void OnVictory()
    {
        victoryMenu.OpenVictoryMenu();
    }
}