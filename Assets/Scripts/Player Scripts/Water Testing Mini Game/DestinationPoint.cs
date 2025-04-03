using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DestinationPoint : MonoBehaviour
{
    public float Range;
    private float xRange = 60f;
    private float yPosition = 3f;
    private float minimumZ = -40f;
    private float maximumZ = -60f;
    
    void Start()
    {
        // If bool isTrashCollected and bool isFloraPlanted are true...
        if (TestTransitionsGameManager.isTrashCollected && TestTransitionsGameManager.isFloraPlanted)
        {
            SetTransform(); 
        }
    }

    // Set transform position 
    void SetTransform()
    {
        transform.position = new Vector3(Random.Range(-xRange, xRange), yPosition, Random.Range(minimumZ, maximumZ)); // transform position is equal to new Vector3 within range
    }

#if UNITY_EDITOR
    private void OnDrawGizmos ()
    {
        Gizmos.DrawWireSphere (transform.position, Range);
    }

#endif
}
