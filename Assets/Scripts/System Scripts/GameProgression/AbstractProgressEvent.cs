using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Interface for progression events. These get onlocked in the order GameProgressManagers has them in.
/// </summary>
public abstract class AbstractProgressEvent
{
     public string Name;
     event UnityAction ProgressEventCompleted;
     public abstract void CompleteProgressEvent();
}
