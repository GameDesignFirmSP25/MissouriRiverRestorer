using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;
using StarterAssets;
using static UnityEngine.Rendering.DebugUI;
using System.Linq;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
     [SerializeField]
     public string targetSceneName;

     [SerializeField]
     private static GameObject startMiniGamePanel;

     [SerializeField]
     private static TextMeshProUGUI startMiniGamePanelText;

     [SerializeField]
     private static StarterAssetsInputs playerInput;

     private bool panelIsOpen = false;

     private static GameObject sharedMiniGamePanel;

     public static void FindPanel()
     {
          if (sharedMiniGamePanel == null)
          {
               sharedMiniGamePanel = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name.Trim() == "Start Mini Game Panel");
          }

        startMiniGamePanel = sharedMiniGamePanel;

          if (startMiniGamePanel == null)
          {
               Debug.LogError("Start Mini Game Panel not found! Is it misnamed?");
          }
          else
          {
               Debug.Log("Start Mini Game Panel found: " + startMiniGamePanel.name);
          }

          startMiniGamePanelText = startMiniGamePanel?.GetComponentInChildren<TextMeshProUGUI>(true);
          playerInput = GameObject.Find("PlayerArmature")?.GetComponentInChildren<StarterAssetsInputs>();
     }

     private void Update()
     {
          bool eKeyDown = Input.GetKeyDown(KeyCode.E);

          if (eKeyDown && panelIsOpen)
          {
               if (startMiniGamePanel != null && startMiniGamePanel.activeSelf)
               {
                     Debug.Log("E key pressed. Transitioning to scene: " + targetSceneName);

                     startMiniGamePanel.SetActive(false);
                     startMiniGamePanelText.text = "";
                     playerInput.controlsLocked = false; // Unlock player controls when a panel is not active
                     TransitionScene(targetSceneName);
                     panelIsOpen = false; // Reset panel state
               }   
          }
     }

     private void OnTriggerEnter(Collider other)
     {
          //StartCoroutine(FindPanelNextFrame());

          if (other.tag == "Player")
          {
               Debug.Log("Player entered trigger for scene transition to: " + targetSceneName);

               if (startMiniGamePanel != null && !startMiniGamePanel.activeSelf)
               {
                    //TransitionScene(targetSceneName);
                    startMiniGamePanel.SetActive(true);
                    playerInput.controlsLocked = true; // Lock player controls when a panel is active
                    panelIsOpen = true; // Set panel state to open

                    if (targetSceneName == "Water Testing Mini Game")
                    {
                        startMiniGamePanelText.text = "Do you want to start the water testing mini game? Press E to start.";
                    }

                    else if (targetSceneName == "Trash Collection")
                    {
                        startMiniGamePanelText.text = "Do you want to start the trash cleanup mini game? Press E to start.";
                    }

                    else if (targetSceneName == "Wildlife Trapping")
                    {
                        startMiniGamePanelText.text = "Do you want to start the conservation mini game? Press E to start.";
                    }

                    else if (targetSceneName == "End Game")
                    {
                        startMiniGamePanelText.text = "Do you want to start the quiz? Press E to start.";
                    }
               }  
          }
     }

     public void TransitionScene(string name)
     {
          Cursor.lockState = CursorLockMode.None;
          if (targetSceneName == "")
          {
               Debug.Log("Scene name not set. Loading Overworld");
               SceneManager.LoadScene("Overworld");
          }
          else
          {
               SceneManager.LoadScene(name);
          }
     }
}
