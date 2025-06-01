using UnityEngine;
using UnityEngine.Rendering;

public class BirdFlight : MonoBehaviour
{
    [Header("Player Reference")]
    public GameObject player;

    [Header("Animator Reference")]
    public Animator animator;

    [Header("Booleans")]
    public bool birdsFlownAway = false;

    [Header("Float Variables")]
    private float flySpeed = .25f; // Speed when flying
    private float randomOffset = 3f; // Add some randomness for direction

    [Header("Vector3")]
    private Vector3 targetPosition; // Position to fly to

    [Header("Materials")]
    private Material eventInteraction;


    private void Start()
    {
        animator.SetBool("isFlying", false);

        Renderer renderer = GetComponentInChildren<Renderer>(); // Get the Renderer component of the interaction object 

        if (renderer != null)
        {
            eventInteraction = new Material(renderer.material); // Create unique instance

            eventInteraction.SetFloat("_OutlineType", 5);

            renderer.material = eventInteraction; // Assign one of them as the active material
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} has no Renderer component in children. Material setup skipped."); // Debug.LogWarning
        }
    }

    private void Update()
    {
        if (AnimalGameManager.birdEventZoneComplete && !birdsFlownAway && BirdEventZonePanelClickHandler.isBirdEventZonePanelClicked)
        {
            FlyAway(player.transform.position);
        }
    }

    public void FlyAway(Vector3 playerPosition)
    {
        Vector3 direction = (transform.position - playerPosition).normalized;
        targetPosition = transform.position + direction * randomOffset + Vector3.up * 30f; // Add upward movement
        targetPosition = Vector3.ClampMagnitude(targetPosition, 300f); // Clamp to a maximum distance

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
    }

    public void SetOutline(bool enabled)
    {
        if (eventInteraction != null)
            eventInteraction.SetFloat("_HasOutline", enabled ? 1.0f : 0.0f);
    }
}
