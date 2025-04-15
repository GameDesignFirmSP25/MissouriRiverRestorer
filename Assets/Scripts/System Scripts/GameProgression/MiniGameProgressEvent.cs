using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "MiniGameProgressEvent", menuName = "ScriptableObjects/Progress Event/Mini Game", order = 1)]
public class MiniGameProgressEvent : BaseProgressEventSO
{
     public string TargetMiniGameScene;
     public event UnityAction<int> ProgressEventCompleted;
     private IMiniGameComplete iMiniGame;

     private void Awake()
     {
          SceneManager.sceneLoaded += OnSceneLoaded;
          SceneManager.sceneUnloaded += OnSceneUnloaded;
     }

     private void OnDestroy()
     {
          SceneManager.sceneLoaded -= OnSceneLoaded;
          SceneManager.sceneUnloaded -= OnSceneUnloaded;
     }

     private void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadSceneMode)
     {
          // Check for matching scene name
          if (loadedScene.name != TargetMiniGameScene) return;

          // Find the minigame complete interface
          iMiniGame = FindAnyObjectByType<IMiniGameComplete>();
          if(iMiniGame == null)
          {
               Debug.LogError("Could not find IMinigame.");
               return;
          }

          iMiniGame.MiniGameComplete += OnMiniGameComplete;
     }

     private void OnSceneUnloaded(Scene loadedScene)
     {
          iMiniGame.MiniGameComplete -= OnMiniGameComplete;
     }

     private void OnMiniGameComplete(int score)
     {
          // TODO: Add score saving logic here
          Debug.Log("Minigame Score: " + score);
          ProgressEventCompleted?.Invoke(score);
     }
}
