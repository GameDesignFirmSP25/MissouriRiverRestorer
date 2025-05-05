using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/// <summary>
/// State of game progression decided by which minigames have been completed.
/// </summary>
public enum GameState
{
     Dirty,
     AfterTrashGame,
     AfterPlantAndAnimalGame,
     AfterSecondWaterTest,
}

/// <summary>
/// Wrapper class for Unity Events that pass the type GameState
/// </summary>
public class GameStateEvent : UnityEvent<GameState> { }

/// <summary>
/// Singleton responsible for keeping track of minigame progress and directing progression.
/// </summary>
public class GameProgressManager : MonoBehaviour
{
     public static GameProgressManager instance;

     public GameObject QuizButton;

     public GameState GameState;
     public bool isAllEventsCompleted;

     public int CurrentProgressionStep;
     public MiniGameData CurrentMiniGamedData;

    public GameStateEvent gameStateChanged = new GameStateEvent();

    [SerializeField]
     private NPC npc;

     // Holds the minigame objects that 
     [SerializeField]
     private List<MiniGameData> minigames;
     [SerializeField]
     private List<ProgressEventsSO> progressEventsSO;
     [SerializeField]
     private List<BaseProgressEvent> progressEvents = new List<BaseProgressEvent>();


     private void Awake()
     {
          if (instance == null)
          {
               instance = this;
               DontDestroyOnLoad(gameObject);
          }
          else if (instance != this)
          {
               Debug.LogWarning("More than one GameProgressManager started. Destroying duplicate");
               Destroy(gameObject);
          }

          SceneManager.sceneLoaded += OnSceneLoad;
          QuizButton.SetActive(false);
     }


     private void Start()
     {
          foreach(ProgressEventsSO PE in progressEventsSO)
          {
               if(PE.EventType == ProgressEventType.NPC)
               {
                    progressEvents.Add(new NPCProgressEvent(PE, PE._Name, npc));
               }
               else if(PE.EventType == ProgressEventType.MiniGame)
               {
                    progressEvents.Add(new MiniGameProgressEvent(PE));
               }
          }

          foreach(BaseProgressEvent BE in progressEvents)
          {
               BE.ProgressEventCompleted += OnProgressEventCompleted;
          }

          // Turn off all minigames at beginning
          foreach(MiniGameData mini in minigames)
          {
               mini.gameObject.SetActive(false);
          }

          // Reset current progression
          CurrentProgressionStep = 0;
          CurrentMiniGamedData = minigames[0];
     }

     private void OnDestroy()
     {
          foreach (BaseProgressEvent BE in progressEvents)
          {
               BE.ProgressEventCompleted -= OnProgressEventCompleted;
          }

          SceneManager.sceneLoaded -= OnSceneLoad;
     }

     private void OnProgressEventCompleted(int score, BaseProgressEvent _event)
     {
          Debug.Log("Event Complete: " + _event._Name);

          // If the current progression event is a minigame progression event
          if (progressEvents[CurrentProgressionStep] is MiniGameProgressEvent)
          {
               // Close out current minigame
               CurrentMiniGamedData.IsComplete = true;
               CurrentMiniGamedData.gameObject.SetActive(false);
               CurrentMiniGamedData.IsInteractable = false;

               // Maps game state to Correct enum. Assumes alternating events of NPC and Minigame
               GameState = (GameState)(((CurrentProgressionStep + 1) / 2) - 1);
               gameStateChanged.Invoke(GameState);
        }

        // Move to the next progression step
        CurrentProgressionStep++;
          if(CurrentProgressionStep >= progressEvents.Count)
          {
               isAllEventsCompleted = true;
               Debug.Log("All Events Complete!");
               // Make all minigames interactable and disconnect from all progression events
               EnableAllWithoutProgress();
               QuizButton.SetActive(true);

               return;
          }

          Debug.Log("Next Event: " + progressEvents[CurrentProgressionStep]._Name);

          // If the next progression event is a minigame progression event
          if (progressEvents[CurrentProgressionStep] is MiniGameProgressEvent)
          {
               // Find the minigame data with the matching target scene name
               CurrentMiniGamedData = minigames.FirstOrDefault(mg => mg.TargetSceneName == progressEvents[CurrentProgressionStep].TargetScene);
               if(CurrentMiniGamedData == null)
               {
                    Debug.LogError("Could Not Find matching minigame data target scene name for progress event: " + progressEvents[CurrentProgressionStep].TargetScene);
                    return;
               }
               CurrentMiniGamedData.IsTasked = true;
               CurrentMiniGamedData.gameObject.SetActive(true);
               CurrentMiniGamedData.IsInteractable = true;
          }
     }

     public void LoadQuizScreen()
     {
          SceneManager.LoadScene("End Game");
     }

     // For Dev Only. Advances progress step
     public void DebugProgressNextEvent()
     {
          progressEvents[CurrentProgressionStep].CompleteProgressEvent(0);
     }

     // TODO: Cleanup logic
     private void OnSceneLoad(Scene loadedScene, LoadSceneMode mode)
     {
          Debug.Log("Scene Loaded: " + loadedScene.name);
          if (loadedScene.name == "End Game") return;
          //TODO: make function for checking the scene name and which miinigames should be active

          // Turn off minigames when not in overworld
          if (loadedScene.name != "Overworld")
          {
               foreach (MiniGameData mg in minigames)
               {
                    mg.gameObject.SetActive(false);
               }
          }

          // Turn all minigames back on, nothing else to do for progression
          if (isAllEventsCompleted)
          {
               if (loadedScene.name == "Overworld")
               {
                    foreach (MiniGameData mg in minigames)
                    {
                         // TODO: get minigames replayable in post-game
                         // Then set this back to true
                         mg.gameObject.SetActive(false);
                    }
               }
               return;
          }

          if(progressEvents.Count == 0) return;
          


          if (loadedScene.name == "Overworld" && progressEvents[CurrentProgressionStep] is NPCProgressEvent)
          {
               CurrentMiniGamedData?.gameObject.SetActive(false);
               npc = FindFirstObjectByType<NPC>();
               NPCProgressEvent currentNPCEvent = progressEvents[CurrentProgressionStep] as NPCProgressEvent;
               currentNPCEvent.SetNPC(npc);

               return;
          }

          if (isSceneMiniGame(loadedScene))
          {
               CurrentMiniGamedData?.gameObject.SetActive(false);
               MiniGameProgressEvent currentMiniGameProgress = progressEvents[CurrentProgressionStep] as MiniGameProgressEvent;
               currentMiniGameProgress.SetMiniManager(FindFirstObjectByType<BaseMiniGameManager>());

               Debug.Log("Current minidata: " + currentMiniGameProgress.TargetScene);
               return;
          }

          if(loadedScene.name == "Overworld" && progressEvents[CurrentProgressionStep] is MiniGameProgressEvent)
          {
               CurrentMiniGamedData?.gameObject.SetActive(true);
          }
     }

     private void EnableAllWithoutProgress()
     {
          foreach (BaseProgressEvent BE in progressEvents)
          {
               BE.ProgressEventCompleted -= OnProgressEventCompleted;
          }

          foreach (MiniGameData mini in minigames)
          {
               // TODO: get minigames replayable in post-game
               // Then set this back to true
               mini.gameObject.SetActive(false);
          }
     }

     private bool isSceneMiniGame(Scene scene)
     {
          return scene.name != "Overworld" && scene.name != "Title" && scene.name != "Guidebook";
     }
}
