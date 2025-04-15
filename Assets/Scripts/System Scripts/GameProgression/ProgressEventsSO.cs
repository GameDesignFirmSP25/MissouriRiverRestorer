using UnityEngine;

public enum ProgressEventType
{
     MiniGame,
     NPC
}

[CreateAssetMenu(fileName = "PrgressEventSO", menuName = "ScriptableObjects/Progress Event/ProgressEvent", order = 1)]
/// <summary>
/// Scriptable Object for progression events. These get onlocked in the order GameProgressManagers has them in.
/// </summary>
public class ProgressEventsSO : ScriptableObject
{
     /// <summary>
     /// For NPC types, this name must match the name of the NPC action name it is going to listen to. 
     /// See the dictionary in NPC.cs
     /// </summary>
     public string _Name;
     public string TargetScene;
     public ProgressEventType EventType;
}

