using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneTransition : MonoBehaviour
{
     [SerializeField]
     public string targetSceneName;

     private void OnTriggerEnter(Collider other)
     {
          if(other.tag == "Player")
          {
               TransitionScene(targetSceneName);
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
