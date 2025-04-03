using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TrashRemover : MonoBehaviour
{
    public int playerScore;
    void Start()
    {
        playerScore = 0;
    }
    void Update()
    {
        // Check if the player clicks
        if (Input.GetMouseButtonDown(0)) // 0 = Left Click
        {
            // Perform Raycast from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the clicked object has the "Trash" tag
                if (hit.collider.CompareTag("Trash"))
                {
                    // Destroy the clicked object
                    Destroy(hit.collider.gameObject);
                    playerScore++;

                    Debug.Log("playerScore: " + playerScore);


                }
            }
        }
    }
}