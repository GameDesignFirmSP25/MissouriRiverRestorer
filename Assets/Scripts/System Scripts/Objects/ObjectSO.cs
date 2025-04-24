using UnityEngine;
using System.IO;

public enum Type {
     Flora,
     Fauna
}


[CreateAssetMenu(fileName = "GuidebookObject", menuName = "ScriptableObjects/GuidebookObject", order = 1)]
public class ObjectSO : ScriptableObject
{
     public string Name;
     public string LatinName;
     public bool isScanned = false;
     public bool isInvasive = false;
     //public int PageNumber;

     public TextAsset Description;
     //public TextAsset CondesnedDescription;
     public Sprite Image;
     public GameObject Model;
     public Vector3 ModelOffset;
     public Vector3 ModelScale = Vector3.one;
}
