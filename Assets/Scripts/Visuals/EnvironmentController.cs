using System;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    [Range(0f, 1f)] public float gameProgress = 0f;
    [SerializeField] MeshRenderer environment = null;
    [SerializeField] MeshRenderer river = null;
    [SerializeField] Material[] progressionMaterials = null;

    private Material envMat;
    Material EnvironmentMat
    {
        set
        {
            envMat = value;
        }
        get
        {
            if (EnvironmentMat == null) 
            { 
                if(environment == null)
                {
                    environment = transform.Find("StCharles").GetComponent<MeshRenderer>();
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
            if (EnvironmentMat == null)
            {
                if (river == null)
                {
                    river = transform.Find("StCharles").GetComponent<MeshRenderer>();
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
                Debug.Log("Game Progress Manager gameStateChanged event is null");
            }
        }
        else
        {
            Debug.Log("Game Progress Manager Instance is null");
        }
    }


    private void Update()
    {
        //ChangeProgressionState(GameProgressManager.instance.GameState); //test without this
    }
    private void OnDisable()
    {
        GameProgressManager.instance.gameStateChanged.RemoveListener(ChangeProgressionState);
    }

    private void ChangeProgressionState(GameState state)
    {
        gameProgress = ((int)state) / Enum.GetValues(typeof(GameState)).Length;


        foreach(Material m in progressionMaterials)
        {
            m.SetFloat("_GameStateLerp", gameProgress);
        }
        if (EnvironmentMat != null)
        {
            EnvironmentMat.SetFloat("_GameStateLerp", gameProgress);
        }

        if (RiverMat != null)
        {
            RiverMat.SetFloat("_GameStateLerp", gameProgress);
        }
    }
}
