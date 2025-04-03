using System.Collections.Generic;
using UnityEngine;


public static class SFXLibrary
{                                     
    //only add new sound tags to the end of the list to avoid index swoopling around

    public enum SFXType { 
        Default, 

        Footstep_Grass, Footstep_Wet, Footstep_Dirt, Footstep_Rocks,

        Dig, Plant,

        Planted_Correct, Planted_Incorrect,
        Grab_Correct, Grab_Incorrect,
        Invasive_Appearance, Invasive_Disappear,
        Water_Collect, Water_TestGood, Water_TestBad, 
        RemoveFromWater_Generic, RemoveFromWater_Trash,

        ClothesRustling, Shore, Brush,

        Score_Up, Score_Down,
        Menu_Open, Menu_Close, Menu_Navigation,
        Notbook_Fill25, Notebook_Fill50, Notebook_Fill75, Notebook_Fill100, 
        Notebook_Open, Notebook_Close, Notebook_PageFlip, 
        Stamp, Drawing, 
        UI_Select, 
        PickUp_Trash,
    }

    public static Dictionary<SFXType, SFXSO> sfxDictionary = new Dictionary<SFXType, SFXSO>();

    public static SFXSO GetSound(SFXType type)
    {
        if (sfxDictionary.Count == 0) 
        {
            var sos = Resources.LoadAll<SFXSO>("Audio");

            foreach (UnityEngine.Object o in sos)
            {
                SFXSO s = (SFXSO)o;
                if (!sfxDictionary.ContainsKey(s.type))
                {
                    sfxDictionary.Add(s.type, s);
                }
                else
                {
                    //Debug.Log(s.type + " is already a key in SFX Dictionary");
                }
            }
            //Debug.Log(sfxDictionary.Count + " SFX types loaded into Dictionary");
        }


        if (sfxDictionary.ContainsKey(type))
        {
            return sfxDictionary[type];
        }
        else return sfxDictionary[SFXType.Default];
    }
    
}
