using UnityEngine;
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

                }
                if (hit.collider.CompareTag("Trash: Bottle") && !TrashCollectionGame.ObjectvBottle)
                {

                }
                if (hit.collider.CompareTag("Trash: trash bag")&& !TrashCollectionGame.ObjectvTrashBag)
                {

                }
                if (hit.collider.CompareTag("Trash: Pizza Slice")&& !TrashCollectionGame.ObjectvPizzaSlice)
                {

                }
                if (hit.collider.CompareTag("Trash: gas can")&& !TrashCollectionGame.ObjectvGasCan)
                {

                }
                if (hit.collider.CompareTag("Save bird") && !TrashCollectionGame.ObjectvSaveBird)
                {

                }
                if (hit.collider.CompareTag("Save fish") && !TrashCollectionGame.ObjectvSaveFish)
                {

                }
                if (hit.collider.CompareTag("Save deer") && !TrashCollectionGame.ObjectvSaveDeer)
                {

                }


                if (hit.collider.CompareTag("TrashReceptical") &&  playerScore ==  8)
                {
                    CollectedTrash += playerScore;
                    Debug.Log("Trash Collected: " +CollectedTrash);
                    
                }

            }
        }
    }
}