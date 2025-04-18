using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ObjectManager : MonoBehaviour
{
     public static ObjectManager instance;

     public List<ObjectSO> ObjectList= new List<ObjectSO>();

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
          }

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
