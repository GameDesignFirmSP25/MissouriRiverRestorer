using UnityEngine;

public class AnimalScript : MonoBehaviour
{
    private float topBound = 30;
    public float speed = 40.0f;
    private AnimalGameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject managerObject = GameObject.Find("AnimalGameManager");
        gameManager = managerObject.GetComponent<AnimalGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (transform.position.z > topBound)
        {

            Destroy(gameObject);
        }

    }

    void OnMouseDown()
    {
        if (gameObject.tag == "Invasive")
        {
            gameManager.Score -= 2;

            if (gameManager.Score < 0)
            {
                gameManager.Score = 0;
            }
        }
        else
        {
            gameManager.Score += 1;
        }

        Destroy(gameObject);
    }
}
