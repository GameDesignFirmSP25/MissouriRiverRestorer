using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;

public class Trashcast : MonoBehaviour
{
    public TrashCollectionGame trashCollectionGame;
    public int playerScore;
    public int CollectedTrash;
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
                    playerScore++; // add section for if player score == amount of game objects in scene to pull up pop up

                    Debug.Log("playerScore: " + playerScore);


                }
                if (hit.collider.CompareTag("Trash: Styrofoam Cup")&& !TrashCollectionGame.ObjectveScup) 
                {
                    Debug.Log("Stryofoam cup clicked");
                    Destroy(hit.collider.gameObject);// takes trash object off of the screen
                    playerScore++; // adds score for styrofoam cup
                }
                if (hit.collider.CompareTag("Trash: Bottle") && !TrashCollectionGame.ObjectvBottle)
                {
                    Debug.Log("Bottle clicked");
                    Destroy(hit.collider.gameObject);// takes trash object off of the screen
                    playerScore++; // adds score for bottle 
                }
                if (hit.collider.CompareTag("Trash: trash bag")&& !TrashCollectionGame.ObjectvTrashBag)
                {
                    Debug.Log("Trash bag clicked");
                    Destroy(hit.collider.gameObject);// takes trash object off of the screen
                    playerScore++; // adds score for trash bag
                }
                if (hit.collider.CompareTag("Trash: Pizza Slice")&& !TrashCollectionGame.ObjectvPizzaSlice)
                {
                    Debug.Log("Pizza slice clicked");
                    Destroy(hit.collider.gameObject);// takes trash object off of the screen
                    playerScore++; // adds score for pizza slice
                }
                if (hit.collider.CompareTag("Trash: gas can")&& !TrashCollectionGame.ObjectvGasCan)
                {
                    DebugFrameTiming.Log("Gas can clicked");
                    Destroy(hit.collider.gameObject);// takes trash object off of the screen
                    playerScore++; // adds score for gas can
                }
                if (hit.collider.CompareTag("Save bird") && !TrashCollectionGame.ObjectvSaveBird)
                {
                    Debug.Log("Save bird clicked");
                    Destroy(hit.collider.gameObject);// takes trash object off of the screen
                    playerScore++; // adds score for save bird
                }
                if (hit.collider.CompareTag("Save fish") && !TrashCollectionGame.ObjectvSaveFish)
                {
                    Debug.Log("Save fish clicked");
                    Destroy(hit.collider.gameObject);// takes trash object off of the screen
                    playerScore++; // adds score for save fish
                }
                if (hit.collider.CompareTag("Save deer") && !TrashCollectionGame.ObjectvSaveDeer)
                {
                    Debug.Log("Save deer clicked");
                    Destroy(hit.collider.gameObject);// takes trash object off of the screen
                    playerScore++; // adds score for save deer
                }


                if (hit.collider.CompareTag("TrashReceptical") &&  playerScore ==  8)
                {
                    Debug.Log("Trash Receptical clicked");
                    CollectedTrash += playerScore;
                    Debug.Log("Trash Collected: " +CollectedTrash);
                    
                }

            }
        }
    }
}