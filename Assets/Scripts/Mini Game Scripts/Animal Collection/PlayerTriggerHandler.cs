using TMPro;
using UnityEngine;

public class PlayerTriggerHandler : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField]
    GameObject objectiveSubText1;

    [SerializeField]
    GameObject objectiveSubText2;

    [SerializeField]
    GameObject objectiveSubText3;

    [SerializeField]
    GameObject objectiveSubText4;

    [SerializeField]
    GameObject objectiveSubText5;

    [SerializeField]
    GameObject objectiveSubText6;

    [Header("UI Elements")]
    [SerializeField]
    TextMeshProUGUI objectiveSubtext1;

    [SerializeField]
    TextMeshProUGUI objectiveSubtext2;

    [SerializeField]
    TextMeshProUGUI objectiveSubtext3;

    [SerializeField]
    TextMeshProUGUI objectiveSubtext4;

    [SerializeField]
    TextMeshProUGUI objectiveSubtext5;

    [SerializeField]
    TextMeshProUGUI objectiveSubtext6;

    void Start()
    {
        objectiveSubtext1.gameObject.SetActive(false); //hide objective subtext 1
        objectiveSubtext2.gameObject.SetActive(false); //hide objective subtext 2
        objectiveSubtext3.gameObject.SetActive(false); //hide objective subtext 3
        objectiveSubtext4.gameObject.SetActive(false); //hide objective subtext 4
        objectiveSubtext5.gameObject.SetActive(false); //hide objective subtext 5
        objectiveSubtext6.gameObject.SetActive(false); //hide objective subtext 6
    }

    // Method called when the player enters a trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Ifthe player enters a trigger collider with the tag "Zone: Lower Bank"...
        if (other.CompareTag("Zone: Lower Bank"))
        {
            Debug.Log("Player is exploring the lower bank."); //Debug.Log
            objectiveSubtext1.gameObject.SetActive(true); //show objective subtext 1
            objectiveSubtext1.text = "Interact with fauna."; //set objective subtext 1 text
            objectiveSubtext2.gameObject.SetActive(true); //show objective subtext 2
            objectiveSubtext2.text = "Find invasive species."; //set objective subtext 2 text
        }

        // If the player enters a trigger collider with the tag "Zone: Mid Bank"...
        if (other.CompareTag("Zone: Mid Bank"))
        {
            Debug.Log("Player is exploring the Mid Bank!"); //Debug.Log
            objectiveSubtext3.gameObject.SetActive(true); //show objective subtext 3
            objectiveSubtext3.text = "Interact with fauna."; //set objective subtext 3 text
            objectiveSubtext4.gameObject.SetActive(true); //show objective subtext 4
            objectiveSubtext4.text = "Find invasive species."; //set objective subtext 4 text
        }

        // If the player enters a trigger collider with the tag "Zone: River"...
        if (other.CompareTag("Zone: Upper Bank"))
        {
            Debug.Log("Player is exploring the Upper Bank!"); //Debug.Log
            objectiveSubtext5.gameObject.SetActive(true); //show objective subtext 5
            objectiveSubtext5.text = "Interact with fauna."; //set objective subtext 5 text
            objectiveSubtext6.gameObject.SetActive(true); //show objective subtext 6
            objectiveSubtext6.text = "Find invasive species."; //set objective subtext 6 text
        }
    }
}
