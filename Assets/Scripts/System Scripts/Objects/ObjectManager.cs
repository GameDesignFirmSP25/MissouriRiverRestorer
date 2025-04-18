using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ObjectManager : MonoBehaviour
{
     public static ObjectManager instance;

     public List<ObjectSO> ObjectList= new List<ObjectSO>();
     public ObjectSO BlankObject;

     private void Awake()
     {
          if (instance == null)
          {
               instance = this;
               DontDestroyOnLoad(gameObject);
          }
          else if (instance != this)
          {
               Debug.LogWarning("More than one Objectmanager started. Destroying duplicate");
               Destroy(gameObject);
               return;
          }
          ResetScannedFlags();
     }

     private void OnDestroy()
     {
          //ResetScannedFlags();
     }

     private void ResetScannedFlags()

     {
          // Reset isScanned flags in Scriptable Objects
          foreach (ObjectSO obj in ObjectList)
          {
               obj.isScanned = false;
          }
     }

     public void SetObjectFound(string name)
     {
          ObjectSO obj = ObjectList.FirstOrDefault(o => o.Name == name);
          if (obj == null)
          {
               Debug.LogWarning("Object name not found. " + name);

          }
          obj.isScanned = true;
     }

          public int GetIndexByName(string name)
     {
          ObjectSO obj = ObjectList.FirstOrDefault(o => o.Name == name);
          if(obj == null)
          {
               Debug.LogWarning("Object name not found. " + name);
               return 0;
          }
          return ObjectList.IndexOf(obj);
     }
}
