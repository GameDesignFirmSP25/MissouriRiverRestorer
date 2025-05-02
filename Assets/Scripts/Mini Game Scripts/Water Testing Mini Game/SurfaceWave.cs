using UnityEngine;

public class SurfaceWave : MonoBehaviour
{
    public GameObject waveToSpawn;
    private float spawnRange = 5f;

    public void MoveSurfaceWave()
    {
        //Vector3 randomOffset = Random.insideUnitSphere * spawnRange;
        //Vector3 spawnPosition = transform.position + randomOffset;

        //Instantiate(waveToSpawn, spawnPosition, transform.rotation);

        //Destroy(gameObject);
    }
}
