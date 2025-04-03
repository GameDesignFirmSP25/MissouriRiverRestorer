using UnityEngine;
using UnityEngine.SceneManagement;

public class TestTransitionsGameManager : MonoBehaviour
{
    private float loadingTime = 2f;

    public static bool isTrashCollected = false;
    public static bool isFloraPlanted = false;

    // Update is called once per frame
    void Update()
    {
        // If bool isWaterQualityGood is true and bool isTrashCollected is false...
        if (!WaterTestingManager.isWaterQualityGood && !isTrashCollected)
        {
            Invoke("TrashCollected", loadingTime); // Call TrashCollected method after loadingTime (in seconds)
        }

        // If bool isTrashCollected is true and bool isFloraPlanted is false...
        if (isTrashCollected && !isFloraPlanted)
        {
            Invoke("FloraPlanted", loadingTime); // Call FloraPlanted method after loadingTime (in seconds)
        }

        //If bool isFloraPlanted is true...
        if (isFloraPlanted)
        {
            SceneManager.LoadScene("Water Testing Mini Game"); // Load scene "Water Testing Mini Game"
        }
    }

    void TrashCollected()
    {
        isTrashCollected = true; // Set bool isTrashCollected to true
    }

    void FloraPlanted()
    {
        isFloraPlanted = true; // Set bool isFloraPlanted to true
    }
}
