using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/* Game progression
 * Initially dirty, must talk to waterson
 * Waterson instructs to test water. Water is poor quality
 * Player picks up trash. Game state progresses.
 * Player does plant sorting. Game state progresses
 * Player retests water. Water is good quality.
 * Player does animal collect. Game state final progression
 * 
 * Game manager keeps track of what minigames have started
 * what games have been completed
 * games score?
 * state of cleanliness
 * 

 * 
 * 
 * 
 */

/// <summary>
/// State of game progression decided by which minigames have been completed.
/// </summary>
public enum GameState
{
     Dirty,
     TrashRemoved,
     PlantsSorted,
     ClearWater,
     AnimalsCollected
}


/// <summary>
/// Singleton responsible for keeping track of minigame progress and directing progression.
/// </summary>
public class GameProgressManager : MonoBehaviour
{
     static GameProgressManager instance;

     public GameState GameState;

     public int CurrentProgressionStep;
     public MiniGameData CurrentMiniGamedData;

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
          }

          // Move to the next progression step
          CurrentProgressionStep++;

          // If the next progression event is a minigame progression event
          if (progressEvents[CurrentProgressionStep] is MiniGameProgressEvent)
          {
               // Find the minigame data with the matching target scene name
               CurrentMiniGamedData = minigames.FirstOrDefault(mg => mg.TargetSceneName == progressEvents[CurrentProgressionStep].TargetScene);
               CurrentMiniGamedData.IsTasked = true;
               CurrentMiniGamedData.gameObject.SetActive(true);
               CurrentMiniGamedData.IsInteractable = true;
          }
     }
}
