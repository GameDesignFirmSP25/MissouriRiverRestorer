using GneissUtilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;


/// <summary>
/// labels for each layer of music
/// </summary>
public enum MX
{
    Perc,
    G_Lead,
    G_Pad,
    G_Arps,
    B_Lead,
    B_Pad,
    B_Arps,
    Minigame,
    MainMenu
}

/// <summary>
/// GameState including minigames and mainmenu
/// </summary>
public enum MXGameState
{
    //all this is overworld stuff
    Dirty, // all bad
    AfterFirstWaterTest, //water testing mini game
    AfterTrashGame,
    AfterPlantAndAnimalGame,
    AfterSecondWaterTest,


    Minigame, //minigame music
    MainMenu, //all good overworld theme
    Overworld
}

public enum MusicState
{
    Minigame, //minigame music
    MainMenu, //all good overworld theme
    Overworld
}

/// <summary>
/// container for grouping and associating a label with instance references of audio sources
/// </summary>
[System.Serializable]
public class MXSource
{
    public MX label;
    public AudioSource source;
    public float maxVolume = 1f;
}

/// <summary>
/// container for assigning a collection of layers to each progression state
/// </summary>
[System.Serializable]
public class ProgMX
{
    public MXGameState progState;
    public MX[] layers;
}





/// <summary>
/// Plays dynamic music depending on game state
/// </summary>
public class MusicController : MonoBehaviour
{
    [Header("State")]
    [SerializeField] public List<MX> currentLayers;
    [SerializeField] public GameState currentGameState;
    [SerializeField] public MXGameState currentMXState;

    [Header("Configuration")]
    [SerializeField] [Range(0f,1f)]float fadeRate = 1f;   //the time it takes for layers to fade in/out
    [SerializeField] float startDelay = 1f; //the time it takes for music to begin playing so it isn't right on load
    [SerializeField] MusicProfileSO profiles; //progression profiles

    [Header("References")]
    public MXSource[] instruments; //the instruments and their labels

    private Dictionary<MX, MXSource> mxSourceDict = new Dictionary<MX, MXSource>();
    private Dictionary<MXGameState, MX[]> stateMXDict = new Dictionary<MXGameState, MX[]>();

    //bool firstWaterTestComplete = false;

    Coroutine fadeInRoutine = null;
    Coroutine fadeOutRoutine = null;
    bool fadingIn = false;
    bool fadingOut = false;

    bool init = false;

    #region Initialize in Awake, Start, and OnDisable
    private void Awake()
    {
        if(!init)
        {
            // BUILD DICTIONARIES
            foreach (MXSource instrument in instruments)
            {
                if (!mxSourceDict.ContainsKey(instrument.label))
                {
                    mxSourceDict.Add(instrument.label, instrument);
                }
            }

            foreach (ProgMX profile in profiles.progProfiles)
            {
                if (!stateMXDict.ContainsKey(profile.progState))
                {
                    stateMXDict.Add(profile.progState, profile.layers);
                }
            }
        }

    }

    private void Start()
    {
        if(!init)
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
                Debug.LogWarning("Game Progress Manager Instance is null");
            }
            SceneManager.sceneLoaded += OnSceneLoaded;
            Invoke("StartMusic", startDelay);
            init = true;
        }

    }

    private void StartMusic()
    {
        SetMusicState(SceneManager.GetActiveScene());
    }

    private void OnDisable()
    {
        try
        {
            GameProgressManager.instance.gameStateChanged.RemoveListener(ChangeProgressionState);
        }
        catch
        {

        }

    }

    #endregion

    #region Music Controller State Machine

    float cacheBuildIndex = -1;
    float currentBuildIndex = -1;
    /// <summary>
    /// On scene loaded, check if we need to change music states
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetMusicState(scene);
    }

    private void SetMusicState(Scene scene)
    {
        //Update Music
        currentBuildIndex = scene.buildIndex;
        if (cacheBuildIndex != currentBuildIndex)
        {
            //Debug.Log("Scene changed to " + currentBuildIndex);

            cacheBuildIndex = currentBuildIndex;
            switch (currentBuildIndex)
            {
                case 0: //title
                    //Debug.Log("Starting Main Menu");
                    ChangeMXState(MXGameState.MainMenu);
                    break;

                case 2: //water testing minigame
                case 3: //minigames
                case 4:
                case 5:
                case 6: //end
                    //Debug.Log("Starting Minigame");
                    ChangeMXState(MXGameState.Minigame);
                    break;

                case 1: //overworld
                default:
                    //Debug.Log("Starting Overworld");
                    ChangeMXState(MXGameState.Overworld);
                    break;
            }
        }
    }

    private void ChangeMXState(MXGameState state)
    {
        currentMXState = state;

        switch(state)
        {
            case MXGameState.Minigame:
                SetMusic(MXGameState.Minigame);
                break;
            case MXGameState.MainMenu:
                SetMusic(MXGameState.MainMenu);
                break;
            case MXGameState.Overworld:
            default:
                SetMusic(GetGameState());
                break;
        }
    }

    private void ChangeProgressionState(GameState state)
    {
        currentGameState = state;

        MXGameState v = MXGameState.Dirty;

        switch (state)
        {
            case GameState.Dirty:
                v = MXGameState.Dirty;
                break;

            case GameState.AfterTrashGame:
                v = MXGameState.AfterTrashGame;
                break;

            case GameState.AfterPlantAndAnimalGame:
                v = MXGameState.AfterPlantAndAnimalGame;
                break;

            case GameState.AfterSecondWaterTest:
            default: //default to good music
                v = MXGameState.AfterSecondWaterTest;
                break;

        }
    }



    private MXGameState GetGameState()
    {
        if(GameProgressManager.instance == null) //either on the main menu or some weird scene
        {
            return MXGameState.AfterSecondWaterTest;
        }

        MXGameState v = MXGameState.Dirty;
        switch (GameProgressManager.instance.GameState)
        {
            case GameState.Dirty:
                v = MXGameState.Dirty;
                break;

            case GameState.AfterTrashGame:
                v = MXGameState.AfterTrashGame;
                break;
            case GameState.AfterPlantAndAnimalGame:
                v = MXGameState.AfterPlantAndAnimalGame;
                break;
            case GameState.AfterSecondWaterTest:
                v = MXGameState.AfterSecondWaterTest;
                break;

            //default to good music
            default:
                v = MXGameState.AfterSecondWaterTest;
                break;

        }
        return v;
    }

    #endregion

    public List<MX> toFadeOut = new List<MX>();
    public List<MX> toFadeIn = new List<MX>();
    private void SetMusic(MXGameState v)
    {
        if (!stateMXDict.ContainsKey(v)) //error, no music change
        {
            Debug.LogWarning("State not included in music profile: " + v.ToString());
            return;
        }

        MX[] newLayers = stateMXDict[v]; //get profile layers array
        //Debug.Log("Setting Music to state: " + v.ToString() + ".... New Layers: " + newLayers.ToList().PrintCollection(" "));


        // REMOVE ---ACTIVE LAYERS--- that are [not present] in ---LAYERS---
        toFadeOut.Clear();
        foreach (MX currentLayer in currentLayers)
        {
            if (!newLayers.Contains(currentLayer))
            {
                //Debug.Log("Fading out " + layer.ToString());
                toFadeOut.Add(currentLayer);
            }
            else
            {
                //if in both lists, leave it alone. 
            }
        }

        if(toFadeOut.Count > 0)
        {
            FadeOutMusic(toFadeOut);
            //Debug.Log("Fading Out Layers: " + toFadeOut.ToList().PrintCollection(" "));
        }



        //Debug.Log("Active Layers: " + currentLayers.ToList().PrintCollection(" "));


        // ADD => LAYERS ---not present--- ACTIVE LAYERS
        //Update Active Layers to new profile
        toFadeIn.Clear();
        foreach (MX layer in newLayers)
        {
            if (!currentLayers.Contains(layer))
            {
                //Debug.Log("Fading in " + layer.ToString());
                toFadeIn.Add(layer);
            }
            else
            {
                //Debug.Log("ACTIVE Layers already has " + layer.ToString());
            }
        }

        if(toFadeIn.Count >0)
        {
            FadeInMusic(toFadeIn);
            //Debug.Log("Fading In Layers: " + toFadeIn.ToList().PrintCollection(" "));
        }

        currentLayers.Clear();
        currentLayers = newLayers.ToList<MX>();
    }

    #region FadeRoutines

    //Start Coroutine methods

    public void FadeOutMusic(List<MX> layers)
    {
        if (fadingOut)
        {
            fadingOut = false;
            //Debug.Log("Stopping Fade out routine");
            StopCoroutine(fadeOutRoutine);
            fadeOutRoutine = null;
        }
        fadeOutRoutine = StartCoroutine(FadeOutRoutine(fadeRate, layers));
    }


    public void FadeInMusic(List<MX> layers)
    {
        if (fadingIn)
        {
            fadingIn = false;
            //Debug.Log("Stopping Fade in routine");
            StopCoroutine(fadeInRoutine);
            fadeInRoutine = null;
        }
        fadeInRoutine = StartCoroutine(FadeInRoutine(fadeRate, layers));
    }

    // IEnumerators

    /// <summary>
    /// Lower volume to 0 by coroutine over fadeTime seconds
    /// </summary>
    /// <param name="fadeRate"></param>
    /// <param name="source"></param>
    /// <returns></returns>
    public IEnumerator FadeOutRoutine(float fadeRate, List<MX> source)
    {

        //Debug.Log("FadeOut count: " + source.Count);
        fadingOut = true;
        float f = 1f;
        if (source != null)
        {
            while (f > 0f)
            {
                foreach (MX a in source)
                {
                    //Change Volume
                    //Debug.Log("Fading out:" + mxSourceDict[a].source.name);
                    mxSourceDict[a].source.volume -= Time.deltaTime * fadeRate;
                }
                f -= Time.deltaTime * fadeRate;
                yield return null;
            }
        }

        foreach (MX a in source)
        {
            mxSourceDict[a].source.volume = 0f;
        }
        //Debug.Log("Fadeout Routine Complete");
        fadingOut = false;
    }

    /// <summary>
    /// Raise volume to maxVol by Coroutine over fadeTime seconds
    /// </summary>
    /// <param name="fadeRate"></param>
    /// <param name="maxVol"></param>
    /// <param name="source"></param>
    /// <returns></returns>
    public IEnumerator FadeInRoutine(float fadeRate, List<MX> source)
    {
        fadingIn = true;
        float f = 0f;
        int v = source.Count;
        //Debug.Log("Fade In count: " + v);
        if (source != null)
        {
            while (v > 0f)
            {
                foreach (MX a in source)
                {
                    if (mxSourceDict[a].source.volume > mxSourceDict[a].maxVolume)
                    {
                        continue;
                    }
                    //Debug.Log("Fading in: " + mxSourceDict[a].source.name);

                    //Change Volume
                    mxSourceDict[a].source.volume += Time.deltaTime * fadeRate;

                    if (mxSourceDict[a].source.volume > mxSourceDict[a].maxVolume)
                    {
                        v--;
                    }
                }
                f += Time.deltaTime;
                yield return null;
            }
        }

        foreach (MX a in source)
        {
            mxSourceDict[a].source.volume = 1f;
        }
        //Debug.Log("Fadein Routine Complete");
        fadingIn = false;
    }

#endregion
}
