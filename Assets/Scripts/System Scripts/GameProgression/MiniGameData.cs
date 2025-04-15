using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Class for holding data related to minigames. 
/// </summary>
public class MiniGameData : MonoBehaviour
{
     public string Name() => gameObject.name;
     public string TargetSceneName;
     public bool IsTasked;         // Talk to waterson to receive task
     public bool IsInteractable;   // After talking to waterson, game object become interactable
     public bool IsStarted;        // Triggering the scene transition starts the minigame
     public bool IsComplete;       // Minigame manager triggers event when minigame is considered complete.

     // TODO: Decide on if and how we want to keep score of minigames
     public int score;   
}
