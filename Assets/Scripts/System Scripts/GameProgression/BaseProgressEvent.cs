using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Scriptable Object for progression events. These get onlocked in the order GameProgressManagers has them in.
/// </summary>
public class BaseProgressEvent
{
     public BaseProgressEvent(string name, string targetScene, ProgressEventType type)
     {
          _Name = name;
          TargetScene = targetScene;
          Type = type;
     }

     public BaseProgressEvent(ProgressEventsSO data)
     {
          _Name = data._Name;
          TargetScene = data.TargetScene;
          Type = data.EventType;
     }

     public string _Name;
     public string TargetScene;
     public ProgressEventType Type;

     public event UnityAction<int> ProgressEventCompleted;
     public void CompleteProgressEvent(int score)
     {
          ProgressEventCompleted?.Invoke(score);
     }
}
