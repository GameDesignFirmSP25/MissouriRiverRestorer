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

/*          Object[] objectArray = Resources.LoadAll("Guidebook/Scriptable Objects/", typeof(ObjectSO));
          for(int i = 0; i < objectArray.Length; i++)
          {
               ObjectList.Add(objectArray[i] as ObjectSO);
          }*/

     }

     // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
