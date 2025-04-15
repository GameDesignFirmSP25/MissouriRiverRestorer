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
 * Minigame data
 * Name - Transition Area
 * Started - event raised by transition area
 * Completed - event raised by minigame manager
 * Score? - parameter passed through completed event (score type?)
 * Available - Decided by game progress manager. - Will enable/disable scene transition
 * 
 * 
 * 
 */

public enum GameState
{
     Dirty,
     TrashRemoved,
     PlantsSorted,
     ClearWater,
     AnimalsCollected
}

public class GameProgressManager : MonoBehaviour
{
     static GameProgressManager instance;

     public GameState GameState;

     [SerializeField]
     private List<GameObject> transitionAreas;

     private void Awake()
     {
          if (instance == null)
          {
               instance = this;
               DontDestroyOnLoad(gameObject);
          }
          else if (instance != this)
          {
               Destroy(gameObject);
          }
     }
}
