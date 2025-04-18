using UnityEngine;

public class FishScript : MonoBehaviour
{
    private float RightBoundry = 60; //how far right the animal despawns
    private float speedMin = 15.0f; //how fast the animal moves
    private float speedMax = 30.0f; //how fast the animal moves
    private AnimalGameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject managerObject = GameObject.Find("AnimalGameManager"); //reference the AnimalGameManager
        gameManager = managerObject.GetComponent<AnimalGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * Random.Range(speedMin, speedMax)); //move the animal

        if (transform.position.z > RightBoundry) //check if animal is out of bounds
        {

            Destroy(gameObject);
        }

    }

    void OnMouseDown() //when the player clicks an animal with mouse
    {
        if (gameObject.tag == "Invasive") //A 'bad' animal is clicked
        {
            gameManager.Score += 1;
        }
        else //A 'good' animal is clicked
        {
            gameManager.Score -= 2;

            if (gameManager.Score < 0)
            {
                gameManager.Score = 0;
            }
        }

        Destroy(gameObject);
    }
}
