using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base class for minigame managers
/// If we were to scale this project, we could move a lot of the shared functionality here.
/// </summary>
public class BaseMiniGameManager : MonoBehaviour
{
     public event UnityAction<int> MiniGameComplete;
     public void TriggerMiniGameCompleteEvent(int param)
     { 
          MiniGameComplete?.Invoke(param);
     }
}
