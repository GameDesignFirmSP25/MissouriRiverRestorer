using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PausMenuManager : MonoBehaviour
{
    public GameObject PauseUiPanel;
    public Button PauseBtn;
    public GameObject pauseButton;

    public Button ResumeBtn;
    public GameObject ResumeButton;
    public Button MMBtn;
    public GameObject MenuButton;
    public Button SettingsBtn;
    public GameObject SettingsButton;

    public Button QuitBtn;

    public GameObject QuitButton;

    public bool isPaused = false;

     public GameObject player;
     public Vector3 returnPos = new Vector3();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PauseUiPanel.SetActive(false);
        PauseBtn.onClick.AddListener(pause);
        ResumeBtn.onClick.AddListener(Resume);
        MMBtn.onClick.AddListener(MainMenu);
        SettingsBtn.onClick.AddListener(settings);
        QuitBtn.onClick.AddListener(PosReset);
    }
    void OnDestroy()
    {
        PauseBtn.onClick.RemoveListener(pause);
        ResumeBtn.onClick.RemoveListener(Resume);
        MMBtn.onClick.RemoveListener(MainMenu);
        SettingsBtn.onClick.RemoveListener(settings);
        QuitBtn.onClick.RemoveListener(PosReset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pause()
    {
        Time.timeScale = 0f;

        isPaused = true;

        PauseUiPanel.SetActive(true);
    }
    public void Resume()
    {
        Time.timeScale = 1f;

        isPaused = false;
       
        PauseUiPanel.SetActive(false);
    }
    public void MainMenu()
    {
          Resume();
        SceneManager.LoadScene("Title");
    }
    public void settings()
    {

    }
    public void PosReset()
    {
          Resume();
          if (player == null) return;
          GameObject controller = player.GetComponent<CharacterController>().gameObject;
          controller.SetActive(false);
          player.transform.localPosition = returnPos;
          controller.SetActive(true);
          pauseButton.SetActive(true);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
