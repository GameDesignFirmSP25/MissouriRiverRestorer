using UnityEngine;

public class Trash : MonoBehaviour
{
    private float xRange = 75f;
    private float yPosition = 3f;
    private float minimumZ = -40f;
    private float maximumZ = 50f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // Set transform position to new Vector3 within specified range
        transform.position = new Vector3(Random.Range(-xRange, xRange), yPosition, Random.Range(minimumZ, maximumZ));

        // If bool isTrashCollected is true
        if (TestTransitionsGameManager.isTrashCollected)
        {
            Destroy(this.gameObject); // Destroy this game object
        }
    }
}
