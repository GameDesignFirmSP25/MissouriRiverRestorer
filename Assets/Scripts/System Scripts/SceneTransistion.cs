using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransistion : MonoBehaviour
{
     [SerializeField]
     public Object transitionScene;

     private void OnTriggerEnter(Collider other)
     {
          if(other.tag == "Player")
          {
               TransitionScene(transitionScene.name);
          }
     }

     public void TransitionScene(string name)
     {
          SceneManager.LoadScene(name);
     }
}
