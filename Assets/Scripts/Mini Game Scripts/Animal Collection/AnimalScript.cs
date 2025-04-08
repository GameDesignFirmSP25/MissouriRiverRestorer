using UnityEngine;

public class AnimalScript : MonoBehaviour
{
    private float topBound = 30;
    private float lowerBound = -50;
    public float speed = 40.0f;
    private AnimalGameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject managerObject = GameObject.Find("AnimalGameManager");

        if (managerObject != null)
        {
            gameManager = managerObject.GetComponent<AnimalGameManager>();
        }

        if (gameManager == null)
        {
            Debug.LogError("AnimalGameManager script not found on AnimalGameManager object!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (transform.position.z > topBound)
        {
            Debug.Log("left bounds");
            Destroy(gameObject);
        }

    }

    void OnMouseDown()
    {
        if (gameObject.tag == "Invasive")
        {
            gameManager.Score -= 2;
            Destroy(gameObject);
            Debug.Log("Score = " + gameManager.Score);
        }
        else
        {
            gameManager.Score += 1;
            Destroy(gameObject);
            Debug.Log("Score = " + gameManager.Score);
        }
    }
}
