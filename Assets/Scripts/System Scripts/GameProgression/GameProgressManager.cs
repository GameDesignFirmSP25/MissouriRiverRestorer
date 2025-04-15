using UnityEngine;
using System.Collections.Generic;

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

     [SerializeField]
     private NPC npc;

     // Holds the minigame objects that 
     [SerializeField]
     private List<MiniGameData> minigames;
     [SerializeField]
     private List<AbstractProgressEvent> progressEvents;

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
          npc.FirstWaterGameTasked += OnFirstWaterTasked;
          npc.TrashGameTasked += OnTrashTasked;
          npc.PlantGameTasked += OnPlantTasked;
          npc.SecondWaterGameTasked += OnSecondWaterTasked;
          npc.AnimalGameTasked += OnAnimalTasked;
     }

     private void OnDestroy()
     {
          npc.FirstWaterGameTasked -= OnFirstWaterTasked;
          npc.TrashGameTasked -= OnTrashTasked;
          npc.PlantGameTasked -= OnPlantTasked;
          npc.SecondWaterGameTasked -= OnSecondWaterTasked;
          npc.AnimalGameTasked -= OnAnimalTasked;
     }

     private void OnFirstWaterTasked()
     {

     }
     private void OnTrashTasked()
     {

     }

     private void OnPlantTasked()
     {

     }

     private void OnSecondWaterTasked()
     {

     }

     private void OnAnimalTasked()
     {
          
     }
}
