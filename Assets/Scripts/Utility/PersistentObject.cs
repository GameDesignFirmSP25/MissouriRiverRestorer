using System.Collections.Generic;
using UnityEngine;
using GneissUtilities;

public class PersistentObject : MonoBehaviour
{
    static PersistentObject instance;

    private void OnEnable()
    {
        gameObject.name = Time.time.ToString();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }
}
