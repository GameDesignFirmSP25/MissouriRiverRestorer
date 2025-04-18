using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class GuidebookUI : MonoBehaviour
{
     public Button BookButton;
     public GameObject GuidebookButton;
     public DynamicGuidebook Guidebook;
     public GameObject GuidebookCanvas;
     public StarterAssetsInputs PlayerInput;

     private bool isGuidebookOpen;

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
               PlayerInput.controlsLocked = false;
          }
          else
          {
               GuidebookCanvas.SetActive(true);
               Guidebook.LoadPage(page);
               PlayerInput.controlsLocked = true;

          }
          isGuidebookOpen = !isGuidebookOpen;
     }
}
