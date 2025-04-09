using UnityEngine;
using UnityEngine.SceneManagement;

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
          if (targetSceneName == "")
          {
               Debug.Log("Scene name not set. Loading first build scene");
               SceneManager.LoadScene(0);
          }
          else
          {
               SceneManager.LoadScene(name);
          }
     }
}
