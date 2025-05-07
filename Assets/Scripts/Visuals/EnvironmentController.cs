using System;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    [Range(0f, 1f)] public float gameProgress = 0f;
    [SerializeField] MeshRenderer environment = null;
    [SerializeField] MeshRenderer river = null;
    [SerializeField] Material[] progressionMaterials = null;
    [SerializeField] Material sky = null;

    private Material envMat;
    Material EnvironmentMat
    {
        set
        {
            envMat = value;
        }
        get
        {
            if (envMat == null) 
            { 
                if(environment == null)
                {
                    Transform t = transform.Find("StCharles");
                    if(t!= null)
                    {
                        environment = t.GetComponent<MeshRenderer>();
                    }

                }
                if(environment != null)
                {
                    envMat = environment.materials[0];
                }
            }
            
            return envMat;
        }
    }

    private Material riverMat;
    Material RiverMat
    {
        set
        {
            riverMat = value;
        }
        get
        {
            if (riverMat == null)
            {
                if (river == null)
                {
                    Transform t = transform.Find("MissouriRiver");
                    if(t!= null)
                    {
                        river = t.GetComponent<MeshRenderer>();
                    }
                }
                if (river != null)
                {
                    riverMat = river.materials[0];
                }
            }
            return riverMat;
        }
    }

    //private void Awake()
    //{
    //    if (environment == null)
    //    {
    //        environment = transform.Find("StCharles").GetComponent<MeshRenderer>();

    //    }
    //    if (river == null)
    //    {
    //        river = transform.Find("MissouriRiver").GetComponent<MeshRenderer>();
    //    }

    //    if (EnvironmentMat == null && environment != null && environment.materials.Length > 0)
    //    {
    //        EnvironmentMat = environment.materials[0];
    //    }
    //    else
    //    {
    //        Debug.LogWarning("An appropriate TERRAIN shader is not assigned in the MeshRendererer");
    //    }

    //    if (RiverMat == null && river != null && river.materials.Length > 0)
    //    {
    //        RiverMat = river.materials[0];
    //    }
    //    else
    //    {
    //        Debug.LogWarning("An appropriate RIVER shader is not assigned in the MeshRendererer");
    //    }
    //}

    private void Start()
    {
        if (GameProgressManager.instance != null)
        {
            if (GameProgressManager.instance.gameStateChanged != null)
            {
                GameProgressManager.instance.gameStateChanged.AddListener(ChangeProgressionState);
            }
            else
            {
                Debug.LogWarning("Game Progress Manager gameStateChanged event is null");
            }
        }
        else
        {
            Debug.Log("Game Progress Manager Instance is null");
        }

        if(GameProgressManager.instance != null)
        {
            ChangeProgressionState(GameProgressManager.instance.GameState);
        }

    }


    private void Update()
    {
        //ChangeProgressionState(GameProgressManager.instance.GameState); //test without this

        if(sky!= null)
        {
            sky.SetFloat("_Rotation", Time.time);
        }
    }
    private void OnDisable()
    {
        if (GameProgressManager.instance != null)
        {
            GameProgressManager.instance.gameStateChanged.RemoveListener(ChangeProgressionState);
        }

    }

    private void ChangeProgressionState(GameState state)
    {
        gameProgress = ((int)state) / Enum.GetValues(typeof(GameState)).Length;


        foreach(Material m in progressionMaterials)
        {
            //Debug.Log("Setting Material: " + m);
            m.SetFloat("_GameStateLerp", gameProgress);
        }

        //Debug.Log("Setting Material: " + EnvironmentMat);
        EnvironmentMat.SetFloat("_GameStateLerp", gameProgress);

        //Debug.Log("Setting Material: " + RiverMat);
        RiverMat.SetFloat("_GameStateLerp", gameProgress);
    }
}
