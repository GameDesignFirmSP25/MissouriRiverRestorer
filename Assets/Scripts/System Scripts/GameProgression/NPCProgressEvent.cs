using System.Linq;
using System.Collections.Generic;

public class NPCProgressEvent : BaseProgressEvent
{
     public string TargetNPCActionName;
     private NPC npc;

     public NPCProgressEvent(string name, string targetScene, ProgressEventType type, string npcAction, NPC targetNPC) : base(name, targetScene, type)
     {
          TargetNPCActionName = npcAction;
          npc = targetNPC;
     }

     public NPCProgressEvent(ProgressEventsSO data, string npcAction, NPC targetNPC) : base(data)
     {
          TargetNPCActionName = npcAction;
          npc = targetNPC;

          npc.actionNames[TargetNPCActionName] += OnEventCompleted;
     }

     ~NPCProgressEvent()
     {
          npc.actionNames[TargetNPCActionName] -= OnEventCompleted;
     }

     public void SetNPC(NPC newNPC)
     {
          npc = newNPC;

          // Remove any dangling refernces before resetting
          npc.actionNames[TargetNPCActionName] = null;
          npc.actionNames[TargetNPCActionName] += OnEventCompleted;
     }

     public void OnEventCompleted()
     {
          CompleteProgressEvent(0);
     }
}
