using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class GuidebookButton : MonoBehaviour
{
     public Button BookButton;
     public GameObject GuidebookUI;
     public StarterAssetsInputs PlayerInput;

     private bool isGuidebookOpen;

     private void Update()
     {
          if(Input.GetKeyDown(KeyCode.G))
          {
               OnBookButton();
          }
     }

     public void OnBookButton()
     {
          if(isGuidebookOpen)
          {
               GuidebookUI.SetActive(false);
               //return player control
               PlayerInput.controlsLocked = false;
          }
          else
          {
               GuidebookUI.SetActive(true);
               PlayerInput.controlsLocked = true;

          }
          isGuidebookOpen = !isGuidebookOpen;
     }
}
