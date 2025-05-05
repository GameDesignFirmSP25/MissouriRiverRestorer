using System;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    [Range(0f,1f)]public float gameProgress = 0f;
    [SerializeField] MeshRenderer environment = null;
    [SerializeField] MeshRenderer river = null;

    Material environmentMat = null;
    Material riverMat = null;

    private void Awake()
    {
        if(environmentMat == null && environment != null && environment.materials.Length > 0)
        {
            environmentMat = environment.materials[0];
        }
        else
        {
            Debug.LogWarning("An appropriate TERRAIN shader is not assigned in the MeshRendererer");
        }

        if (riverMat == null && river != null && river.materials.Length > 0)
        {
            riverMat = river.materials[0];
        }
        else
        {
            Debug.LogWarning("An appropriate RIVER shader is not assigned in the MeshRendererer");
        }
    }

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
        ChangeProgressionState(GameProgressManager.instance.GameState);
    }
    private void OnDisable()
    {
        GameProgressManager.instance.gameStateChanged.RemoveListener(ChangeProgressionState);
    }

    private void ChangeProgressionState(GameState state)
    {
        gameProgress = ((int)state) / Enum.GetValues(typeof(GameState)).Length;
        environmentMat.SetFloat("_GameStateLerp", gameProgress);        
        
        //environmentMat.SetFloat("_GameStateLerp", gameProgress);


        //   Dirty,
        //AfterTrashGame,
        //AfterPlantAndAnimalGame,
        //AfterSecondWaterTest,

        //   environmentMat.SetFloat("_GAMESTATE", (int)state);
        //   riverMat.SetFloat("_GAMESTATE", (int)state);

        //   string s = "";
        //   switch(state)
        //   {
        //       case GameState.Dirty:
        //           s = "DIRTY";
        //           break;            
        //       case GameState.Dirty:
        //           break;            
        //       case GameState.Dirty:
        //           break;            
        //       case GameState.Dirty:
        //           default;
        //           break;
        //   }


        /*
         *     Dirty, //
               AfterTrashGame,
               AfterPlantAndAnimalGame,
               AfterSecondWaterTest,
        */
    }
}
