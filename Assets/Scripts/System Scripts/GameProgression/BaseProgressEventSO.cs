using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Scriptable Object for progression events. These get onlocked in the order GameProgressManagers has them in.
/// </summary>
public class BaseProgressEventSO : ScriptableObject
{
     public string Name;
     event UnityAction ProgressEventCompleted;
     public void CompleteProgressEvent()
     {
          ProgressEventCompleted?.Invoke();
     }
}
