using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlantGameManager : BaseMiniGameManager
{
    [Header("UI Elements")]
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI RestartingText;
    public GameObject startButton;
    public GameObject returnButton;
    public GameObject checkButton; 

    [Header("Round Settings")]
    public float ScoreThreshold = 3f; //Max number of mistakes
    private float restartTimer = 3f; //Time before restarting the game
    public int[] plantsPerRound = { 5, 10, 15 };     //How many plants per round
    public float[] speedsPerRound = { 1f, 2f, 3f };  //How fast they move per round
    public GameObject[] plantprefabs; //Add plants to the array and tag invasive ones (MUST HAVE RIGID BODY WITH SAME SETTINGS AS EXAMPLE PREFABS & PLANT SCRIPT)
    public Vector3 spawnCenter = Vector3.zero; //center of spawn area
    public Vector3 spawnAreaSize = new Vector3(22, 0, 22); //The size of the area where plants can spawn

    [HideInInspector]
    public int Score = 0; //Number of current mistakes
    private List<GameObject> spawnedPlants = new List<GameObject>(); //Track all spawned plants
    private bool gameActive = false; //Check if the game has started
    private int currentRound = 1;

    public static bool plantingCompleted = false; // Global variable to check if planting is completed

    void Start()
    {
        checkButton.SetActive(false);
        RestartingText.text = ""; //Hide the restarting text
    }

    void Update()
    {
        if (gameActive)
        {
            ScoreText.text = "Native Plants Removed: " + Score.ToString() + "/" + ScoreThreshold.ToString(); //update score text 
        }

        if ((Score >= ScoreThreshold || AreAllPlantsDestroyed()) && gameActive) //If too many good plants are removed OR if all plants are removed end the game
        {
            GameLost();
        }
    }

    void SpawnObjects()
    {
        int numberToSpawn = plantsPerRound[currentRound - 1]; //set number of plants
        float speed = speedsPerRound[currentRound - 1];       //set speed of plants
       
        foreach (GameObject prefab in plantprefabs) //Spawn one guaranteed non-invasive plant
        {
            if (!prefab.CompareTag("Invasive"))
            {
                Vector3 goodSpawnPos = GetRandomSpawnPosition();
                GameObject goodPlant = Instantiate(prefab, goodSpawnPos, Quaternion.identity);

                PlantScript plantScript = goodPlant.GetComponent<PlantScript>();
                if (plantScript != null)
                {
                    plantScript.speed = speed;
                }

                spawnedPlants.Add(goodPlant);
                break;
            }
        }

        for (int i = 1; i < numberToSpawn; i++) //Spawn the remaining plants randomly
        {
            GameObject prefab = plantprefabs[Random.Range(0, plantprefabs.Length)];
            Vector3 spawnPosition = GetRandomSpawnPosition();

            GameObject newPlant = Instantiate(prefab, spawnPosition, Quaternion.identity);

            PlantScript plantScript = newPlant.GetComponent<PlantScript>();
            if (plantScript != null)
            {
                plantScript.speed = speed;
            }
            spawnedPlants.Add(newPlant);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f);
        float z = Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f);
        return new Vector3(x, 0, z) + spawnCenter;
    }

    public void StartButton()
    {
        returnButton.SetActive(false); //Hide other UI assets
        startButton.SetActive(false);
        TitleText.text = "";
        checkButton.SetActive(true); //Show the check button
        StartRound();
        gameActive = true; //Game has started
    }

    void StartRound()
    {
        ClearPlants(); //Clear leftover plants from previous round
        SpawnObjects();
    }

    public void CheckPlantsButton() //Check to see if all invasive plants have been removed
    {
        bool invasiveFound = false;

        foreach (GameObject plant in GameObject.FindGameObjectsWithTag("Invasive"))
        {
            if (plant != null)
            {
                invasiveFound = true;
                break;
            }
        }

        if (invasiveFound)
        {
            GameOver(false); //Player failed to remove all invasive plants
        }
        else
        {
            if (currentRound < 3)
            {
                currentRound++;
                StartRound(); //Advance to next round
            }
            else
            {
                GameOver(true); //Player wins
            }
        }
    }

    public bool AreAllPlantsDestroyed() //Check if there are no plants in the scene
    {
        spawnedPlants.RemoveAll(plant => plant == null); //Remove any destroyed objects from the list
        return spawnedPlants.Count == 0; //If the list is now empty all plants have been destroyed
    }

    void GameOver(bool win) //Game end through checking
    {
        checkButton.SetActive(false);
        gameActive = false;
        ClearPlants();

        if (win)
        {
            TitleText.text = "You removed all invasive species!"; //Player won the minigame
            plantingCompleted = true; //Set the global variable to true
            returnButton.SetActive(true); //Show the return button

               // For Game Progression
               TriggerMiniGameCompleteEvent(0);   // Can add score as pass through
        }
        else
        {
            TitleText.text = "Invasive species remain! Try again.";//Player lost the minigame after checking
            Restart(); //Restart
        }
    }

    void GameLost() //Player lost the minigame after either removing all plants or too many good ones
    {
        ClearPlants();
        checkButton.SetActive(false);
        gameActive = false;
        TitleText.text = "Removed too many good plants! Try again.";
        Restart(); //Restart
    }

    public void ReturnButton()
    {
        SceneManager.LoadScene("Overworld");
    }

    void ClearPlants() //clear plants from scene
    {
        foreach (GameObject plant in spawnedPlants)
        {
            if (plant != null)
                Destroy(plant);
        }
        spawnedPlants.Clear();
    }

    void Restart() //Restart
    {
        RestartingText.text = "Restarting in " + restartTimer.ToString() + " seconds...";
        Invoke("ReloadScene", restartTimer); //Reload the scene after a short delay
    }

    void ReloadScene() //Reload the current scene
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
