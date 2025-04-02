using UnityEngine;
using UnityEngine.SceneManagement;

public class SampleSceneGameManager : MonoBehaviour
{
    private float loadingTime = 2f;

    public static bool isTrashCollected = false;
    public static bool isFloraPlanted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!WaterTestingManager.isWaterQualityGood && !isTrashCollected)
        {
            // Start Trash Mini Game

            Invoke("TrashCollected", loadingTime);
        }

        if (isTrashCollected && !isFloraPlanted)
        {
            // Start Planting Mini game

            Invoke("FloraPlanted", loadingTime);
        }

        if (isFloraPlanted)
        {
            SceneManager.LoadScene("Water Testing Mini Game");
        }
    }

    void TrashCollected()
    {
        isTrashCollected = true;
    }

    void FloraPlanted()
    {
        isFloraPlanted = true;
    }
}
