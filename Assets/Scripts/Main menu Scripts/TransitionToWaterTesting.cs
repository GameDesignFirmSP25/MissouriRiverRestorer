using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionToWaterTesting : MonoBehaviour
{
    void OnTriggerEnter(Collider self)
    {
        if (gameObject.CompareTag("Building: Water Testing Site"))
        {
            if (WaterTestingManager.isFirstWaterTestComplete)
            {
                Debug.Log("Transition to Water Testing Site");
                SceneManager.LoadScene("Water Testing Mini Game"); // Load scene "Water Testing Mini Game"
            } 
        }
    }
}
