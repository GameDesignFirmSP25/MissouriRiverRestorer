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
          SceneManager.LoadScene(name);
     }
}
