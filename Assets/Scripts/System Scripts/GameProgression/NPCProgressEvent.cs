using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Base NPC scriptable object. Used for deriving new specific NPC progress event scriptable objects
/// </summary>
[CreateAssetMenu(fileName = "NPCEvent", menuName = "ScriptableObjects/Progress Event/NPC", order = 1)]
public class NPCProgressEvent : BaseProgressEventSO
{
     public event UnityAction ProgressEventCompleted;
     public string TargetNPCActionName;
     public string TargetNPCScene;

     private NPC npc;

     private void Awake()
     {
          if (npc == null)
          {
               npc = FindAnyObjectByType<NPC>();
          }
          SceneManager.sceneLoaded += OnSceneLoaded;
          SceneManager.sceneUnloaded += OnSceneUnloaded;
     }

     private void OnDestroy()
     {
          SceneManager.sceneLoaded -= OnSceneLoaded;
          SceneManager.sceneUnloaded -= OnSceneUnloaded;
     }

     private void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadSceneMode)
     {
          // Check for matching scene name
          if (loadedScene.name != TargetNPCScene) return;
          npc.actionNames[TargetNPCActionName] += OnEventCompleted;
     }

     private void OnSceneUnloaded(Scene unloadedScene)
     {
          npc.actionNames[TargetNPCActionName] -= OnEventCompleted;
     }

     public void OnEventCompleted()
     {
          ProgressEventCompleted.Invoke();
     }
}
