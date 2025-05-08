using System;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    [Range(0f, 1f)] public float gameProgress = 0f;
    //[SerializeField] MeshRenderer environment = null;
    //[SerializeField] MeshRenderer river = null;

    [SerializeField] private Material[] progressionMaterials = null;
    public Material[] ProgressionMaterials
    {
        get
        {
            if(progressionMaterials == null || progressionMaterials.Length == 0) 
            {
                progressionMaterials = Resources.LoadAll<Material>("ProgressionMaterials");
            }
            return progressionMaterials;
        }
    }
    [SerializeField] Material sky = null;
    public Material Sky
    {
        get
        {
            if (sky == null)
            {
                Material[] m = Resources.LoadAll<Material>("ProgressionMaterials/Sky");
                if(m.Length > 0) { sky = m[0]; }
            }
            return sky;
        }
    }


    private GameProgressManager ProgressionManager
    {
        get
        {
            return GameProgressManager.instance;
        }
    }


    public static EnvironmentController instance = null;

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Debug.LogWarning("More than one GameProgressManager started. Destroying duplicate");
            Destroy(this);
        }

    }

    //private Material envMat;
    //Material EnvironmentMat
    //{
    //    set
    //    {
    //        envMat = value;
    //    }
    //    get
    //    {
    //        if (envMat == null) 
    //        { 
    //            if(environment == null)
    //            {
    //                Transform t = transform.Find("StCharles");
    //                if(t!= null)
    //                {
    //                    environment = t.GetComponent<MeshRenderer>();
    //                }

    //            }
    //            if(environment != null)
    //            {
    //                envMat = environment.materials[0];
    //            }
    //        }

    //        return envMat;
    //    }
    //}

    //private Material riverMat;
    //Material RiverMat
    //{
    //    set
    //    {
    //        riverMat = value;
    //    }
    //    get
    //    {
    //        if (riverMat == null)
    //        {
    //            if (river == null)
    //            {
    //                Transform t = transform.Find("MissouriRiver");
    //                if(t != null)
    //                {
    //                    river = t.GetComponent<MeshRenderer>();
    //                }
    //            }
    //            if (river != null)
    //            {
    //                riverMat = river.materials[0];
    //            }
    //        }
    //        return riverMat;
    //    }
    //}

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

    //private void OnEnable()
    //{
    //    Debug.Log("OnEnable: Environment Controller on object " + gameObject.name);
    //    if (GameProgressManager.instance != null)
    //    {
    //        if (GameProgressManager.instance.gameStateChanged != null)
    //        {
    //            GameProgressManager.instance.gameStateChanged.AddListener(ChangeProgressionState);
    //        }
    //        else
    //        {
    //            Debug.LogWarning("Game Progress Manager gameStateChanged event is null");
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("Game Progress Manager Instance is null");
    //    }

    //    if(GameProgressManager.instance != null)
    //    {
    //        ChangeProgressionState(GameProgressManager.instance.GameState);
    //    }

    //}

    //private void OnDisable()
    //{
    //    if (GameProgressManager.instance != null)
    //    {
    //        GameProgressManager.instance.gameStateChanged.RemoveListener(ChangeProgressionState);
    //    }
    //}

    private void Update()
    {
        if (ProgressionManager != null)
        {
            //gameProgress = (int)GameProgressManager.instance.GameState
            ChangeProgressionState(ProgressionManager.GameState);
        }
        else
        {
            Debug.Log("GameProgressManager.instance is null");
        }

        if (Sky != null)
        {
            Sky.SetFloat("_Rotation", Time.time);
        }
    }


    private void ChangeProgressionState(GameState state)
    {
        gameProgress = (((float)state) / (Enum.GetValues(typeof(GameState)).Length-1));
        //0 - 0
        //1 - .33
        //2 - .66
        //3 - 100

        //Debug.Log("GameState: " + state + ", " + (int)state + ", " + gameProgress);
        foreach (Material m in ProgressionMaterials)
        {
            //Debug.Log("Setting Material: " + m);
            m.SetFloat("_GameStateLerp", gameProgress);
        }

        ////Debug.Log("Setting Material: " + EnvironmentMat);
        //if(EnvironmentMat != null)
        //{
        //    EnvironmentMat.SetFloat("_GameStateLerp", gameProgress);
        //}


        ////Debug.Log("Setting Material: " + RiverMat);
        //if ((RiverMat != null)
        //{
        //    RiverMat.SetFloat("_GameStateLerp", gameProgress);
        //}

    }
}
