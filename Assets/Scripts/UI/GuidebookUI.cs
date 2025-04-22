using UnityEngine;
using UnityEngine.UI;
using StarterAssets;
using UnityEngine.SceneManagement;

public class GuidebookUI : MonoBehaviour
{
     public Button BookButton;
     public GameObject GuidebookButton;
     public DynamicGuidebook Guidebook;
     public GameObject GuidebookCanvas;
     public StarterAssetsInputs PlayerInput;

     public bool isGuidebookOpen;

     private void Awake()
     {
          SceneManager.sceneLoaded += OnSceneLoad;
     }

     private void OnDestroy()
     {
          SceneManager.sceneLoaded -= OnSceneLoad;
     }

     private void Update()
     {
          if(Input.GetKeyDown(KeyCode.G))
          {
               OnBookButton();
          }
     }

     public void OnBookButton(int page = 0)
     {
          if(isGuidebookOpen)
          {
               GuidebookCanvas.SetActive(false);
               //return player control
               if (PlayerInput != null)
                    PlayerInput.controlsLocked = false;
          }
          else
          {
               GuidebookCanvas.SetActive(true);
               Guidebook.LoadPage(page);
               if (PlayerInput != null)
                    PlayerInput.controlsLocked = true;

          }
          isGuidebookOpen = !isGuidebookOpen;
     }

     private void OnSceneLoad(Scene loadedScene, LoadSceneMode loadMode)
     {
          PlayerInput = FindFirstObjectByType<StarterAssetsInputs>();
     }
}
