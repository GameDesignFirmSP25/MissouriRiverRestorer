using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Music", menuName = "Music Data")]
[System.Serializable]
public class MusicProfileSO : ScriptableObject
{
    public ProgMX[] progProfiles = null; 
}
