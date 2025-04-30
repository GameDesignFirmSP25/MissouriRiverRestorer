using UnityEngine;
using UnityEngine.AI;

public class NavigationAnimation : MonoBehaviour
{
    public NavMeshAgent agent; // Reference to the NavMeshAgent component
    Animator animator; // Reference to the Animator component

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component from the game object
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isWalking", agent.velocity.magnitude > 0.2f); // Set the isWalking parameter based on the agent's velocity
        animator.SetBool("isWaiting", agent.velocity.magnitude <= 0.2f); // Set the isWaiting parameter based on the agent's velocity
    }
}
