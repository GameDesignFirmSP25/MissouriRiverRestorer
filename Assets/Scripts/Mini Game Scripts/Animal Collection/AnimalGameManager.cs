using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AnimalGameManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI TitleText;
    public GameObject startButton;
    public GameObject returnButton;
    public GameObject[] animalPrefabs;
    public float spawnRangeX = 20;
    public float spawnPosZ = -30;
    public float startDelay = 2;
    public float spawnInterval = 0.5f;
    public float Score = 0f;
    public float timeRemaining = 60f;
    private bool timerRunning = false;
    public float ScoreThreshold = 25f;

    void Start()
    {
        returnButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Score: " + Score.ToString();
        TimerText.text = "Time: " + Mathf.CeilToInt(timeRemaining).ToString();
        if (timerRunning)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                timerRunning = false;
                EndLevel();
            }
        }
        if (Score >= ScoreThreshold)
        {
            timerRunning = false;
            EndLevel();
        }

    }

    void SpawnRandomAnimal()
    {

        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);

        Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);

    }

    public void StartButton()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
        ScoreText.text = "Score: 0" + Score.ToString();
        startButton.SetActive(false);
        timerRunning = true;
        TitleText.text = "";
    }

    void EndLevel()
    {
        CancelInvoke("SpawnRandomAnimal");
        returnButton.SetActive(true);

        if (Score < ScoreThreshold)
        {
            TitleText.text = "You Lose!";
        }
        else
        {
            TitleText.text = "You Win!";
        }
    }

    public void ReturnButton()
    {
        SceneManager.LoadScene(0);
    }
}
