using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DynamicEnvironments : MonoBehaviour
{
     public List<GameObject> Environments;
     private GameProgressManager _progressManager;

     private void Awake()
     {
          
     }

     private void Start()
     {
          _progressManager = GameProgressManager.instance;
          SetActiveEnvironment(_progressManager.GameState);
     }

     public void SetActiveEnvironment(GameState gameState)
     {
          // Turn off all game objects
          foreach (GameObject obj in Environments)
          {
               obj.SetActive(false);
          }

          // Turn on active environment
          GameObject activeEnv = Environments.FirstOrDefault(en => en.name == gameState.ToString());
          activeEnv.SetActive(true);
     }
}
