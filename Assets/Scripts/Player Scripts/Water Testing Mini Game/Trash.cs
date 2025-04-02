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
        transform.position = new Vector3(Random.Range(-xRange, xRange), yPosition, Random.Range(minimumZ, maximumZ));

        if (SampleSceneGameManager.isTrashCollected)
        {
            Destroy(this.gameObject);
        }
    }
}
