using UnityEngine;
using UnityEngine.Rendering;

public class BirdFlight : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    public AnimalGameManager animalGameManager;
    public bool birdsFlownAway = false;
    private float flySpeed = 0.025f; // Speed when flying
    private float randomOffset = 3f; // Add some randomness for direction
    private Vector3 targetPosition; // Position to fly to


    private void Start()
    {
        animator.SetBool("isFlying", false);
    }

    private void Update()
    {
        if (animalGameManager.birdEventZoneComplete && !birdsFlownAway)
        {
            FlyAway(player.transform.position);
        }
    }

    public void FlyAway(Vector3 playerPosition)
    {
        Vector3 direction = (transform.position - playerPosition).normalized;
        targetPosition = transform.position + direction * randomOffset + Vector3.up * 5f; // Add upward movement
        targetPosition = Vector3.ClampMagnitude(targetPosition, 100f); // Clamp to a maximum distance
        //// Calculate a flight direction away from the player
        //targetPosition = transform.position + Vector3.Normalize(Random.onUnitSphere) * randomOffset + (transform.position - playerPosition);

        // Start flying
        StartCoroutine(FlyCoroutine());
    }

    private System.Collections.IEnumerator FlyCoroutine()
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f) // Use a small threshold
        {
            animator.SetBool("isFlying", true);
            transform.position = Vector3.Lerp(transform.position, targetPosition, flySpeed * Time.deltaTime);
            yield return null;
        }

        birdsFlownAway = true;
        Debug.Log("Birds have flown away.");
        //// Fly to target position
        //while (transform.position != targetPosition)
        //{
        //    transform.position = Vector3.Lerp(transform.position, targetPosition, flySpeed * Time.deltaTime);
        //    yield return null;
        //}

        //birdsFlownAway = true;

    }
}
