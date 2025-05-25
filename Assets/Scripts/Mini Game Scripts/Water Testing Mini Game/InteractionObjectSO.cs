using UnityEngine;

[CreateAssetMenu(fileName = "InteractionObject", menuName = "Scriptable Objects/InteractionObject")]
public class InteractionObjectSO : ScriptableObject
{
    public string interactionType; // Type of interaction (e.g., "Trash Bag", "Surface Wave")
    public float interactionDistance = 5.0f; // Distance from the player to interact with the object
    public SFXMaker interactSound; // Sound to play when interacting with the object
}
