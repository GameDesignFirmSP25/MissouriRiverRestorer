using UnityEngine;

/// <summary>
/// Class for holding data related to minigames. 
/// </summary>
public class MiniGameData : MonoBehaviour
{
     public string Name() => gameObject.name;
     public bool IsStarted;
     public bool IsComplete;
     public bool IsInteractable;
     // TODO: Decide on if and how we want to keep score of minigames
     public int score;   
}
