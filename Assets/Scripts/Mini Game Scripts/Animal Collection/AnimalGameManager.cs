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
    public GameObject pauseButton;
    //Public Variables
    public GameObject[] animalPrefabs; //Add animals to this array, tag invasive animals with the invasive tag
    public GameObject[] fishPrefabs; //Add fish to this array, tag invasive fish with the invasive tag
    private float animalMinX = 33; //How far apart the animals can spawn from the left to the right of the screen
    private float animalMaxX = -15; //How far apart the animals can spawn from the top to the bottom of the screen
    private float animalYPos = 0; //How far up the animals spawn
    private float fishMinX = 45; //How far apart the fish can spawn from the left to the right of the screen
    private float fishMaxX = 54; //How far apart the fish can spawn from the top to the bottom of the screen
    private float fishYPos = -7; //How far up the fish spawn
    private float spawnPosZ = -60; //how far to the left or right of the camera the animals spawn
    private float startDelay = 1; //Delay before animals start spawning after games begin
    private float spawnInterval = 0.25f; //Time between animal spawns (spawn rate)
    private float timeRemaining = 60f; //How long the game lasts
    private float ScoreThreshold = 25f; //Score to meet to win
    public float Score = 0f; //tracks player score
    private float restartTimer = 3f; //Time before game resets

    private bool timerRunning = false; //Game is 'active'
    public static bool trappingCompleted = false; // Global variable to check if trapping is completed

    void Start()
    {
        returnButton.SetActive(false);
        startButton.SetActive(true); //show start button
        startPanel.SetActive(true); //show start panel
        pauseButton.SetActive(false); //hide pause button
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
        int fishIndex = Random.Range(0, fishPrefabs.Length);
        Vector3 animalSpawnPos = new Vector3(Random.Range(animalMinX, animalMaxX), animalYPos, spawnPosZ);
        Vector3 fishSpawnPos = new Vector3(Random.Range(fishMinX, fishMaxX), fishYPos, spawnPosZ);

        Instantiate(animalPrefabs[animalIndex], animalSpawnPos, animalPrefabs[animalIndex].transform.rotation);
        Instantiate(fishPrefabs[fishIndex], fishSpawnPos, fishPrefabs[fishIndex].transform.rotation);

    }

    public void StartButton() //triggers on start button press
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval); //start spawning animals
        ScoreText.text = "Score: " + Score.ToString() + "/" + ScoreThreshold.ToString();
        startButton.SetActive(false); //hide start button
        timerRunning = true; //start timer
        startPanel.SetActive(false); //hide start panel
        pauseButton.SetActive(true); //show pause button
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
