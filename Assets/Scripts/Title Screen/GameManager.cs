using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField]
    private SFXMaker interactButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayInteractButtonClick()
    {
        interactButton.PlaySound(); // Play the interact button click sound
    }
}
