using System.Collections.Generic;
using UnityEngine;
using GneissUtilities;

public class PersistentObject : MonoBehaviour
{
    static PersistentObject instance;
    public float birthTime;

    private void OnEnable()
    {
        birthTime = Time.time;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this && instance.birthTime < birthTime)
        {
            try { Destroy(gameObject); }
            catch {}

        }
    }
}
