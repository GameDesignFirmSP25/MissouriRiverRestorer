using UnityEngine;

public class InteractableObject : MonoBehaviour
{
     public GameObject InteractPopup;

     public bool isWithinRange = false;

     public ObjectSO data;

    void Awake()
    {
        if (InteractPopup != null)
            InteractPopup.SetActive(false);
    }

    private void Update()
    {
        if (isWithinRange)
        {
            transform.LookAt(Camera.main.gameObject.transform);
        }
    }

    public void InRange()
    {
        isWithinRange = true;
        if (InteractPopup != null)
            InteractPopup.SetActive(true);
    }

    public void OutRange()
    {
        isWithinRange = false;
        if (InteractPopup != null)
            InteractPopup.SetActive(false);
    }

}
