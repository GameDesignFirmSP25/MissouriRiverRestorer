using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AnimalGameManager : MonoBehaviour
{
    //UI Elements
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI EndText;
    public TextMeshProUGUI RetartingText;
    public GameObject startPanel;
    public GameObject startButton;
    public GameObject returnButton;
    //Public Variables
    public GameObject[] animalPrefabs; //Add animals to this array, tag invasive animals with the invasive tag
    public float spawnRangeX = 25; //How far apart the animals can spawn from the top to the bottom of the screen
    public float spawnPosZ = -60; //how far to the left or right of the camera the animals spawn
    public float startDelay = 2; //Delay before animals start spawning after games begin
    public float spawnInterval = 0.5f; //Time between animal spawns (spawn rate)
    public float timeRemaining = 60f; //How long the game lasts
    public float ScoreThreshold = 25f; //Score to meet to win
    private bool timerRunning = false; //Game is 'active'
    public float Score = 0f; //tracks player score
    public float restartTimer = 3f; //Time before game resets

    public static bool trappingCompleted = false; // Global variable to check if trapping is completed

    void Start()
    {
        returnButton.SetActive(false);
        startButton.SetActive(true); //show start button
        startPanel.SetActive(true); //show start panel
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Score: " + Score.ToString() + "/" + ScoreThreshold.ToString(); //update score text 
        TimerText.text = "Time: " + Mathf.CeilToInt(timeRemaining).ToString(); //update time text 

        if (timerRunning) //Timer countdown if game is 'active'
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0) //end game if timer hits 0
            {
                timeRemaining = 0;
                timerRunning = false;
                EndLevel();
            }
        }
        if (Score >= ScoreThreshold) //end game if score is at threshold
        {
            timerRunning = false;
            EndLevel();
        }

    }

    void SpawnRandomAnimal() //Handles spawning the animals at random coordinates
    {

        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);

        Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);

    }

    public void StartButton() //triggers on start button press
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval); //start spawning animals
        ScoreText.text = "Score: " + Score.ToString() + "/" + ScoreThreshold.ToString();
        startButton.SetActive(false); //hide start button
        timerRunning = true; //start timer
        startPanel.SetActive(false); //hide start panel
    }

    void EndLevel()
    {
        CancelInvoke("SpawnRandomAnimal"); //stop spawning animals
        
        if (Score < ScoreThreshold) //determine if the player won or lost
        {
            EndText.text = "You Lose!";
            Invoke("RestartScene", restartTimer); //reset game after 2 seconds
        }
        else
        {
            EndText.text = "You Win!";
            returnButton.SetActive(true);
            trappingCompleted = true; //set global variable to true
        }  
    }

    public void ReturnButton()
    {
        SceneManager.LoadScene(0); //load main scene
    }

    public void RestartScene()
    {
        RetartingText.text = "Restarting in " + restartTimer.ToString() + " seconds...";
        Invoke("Reload", restartTimer); //reload the game scene after x seconds
    }

    public void Reload()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name); //load the game scene
    }
}
