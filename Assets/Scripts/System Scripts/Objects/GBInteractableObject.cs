using UnityEngine;

public class InteractableObject : MonoBehaviour
{
     public GameObject InteractPopup;

     public bool isWithinRange = false;


     public ObjectSO data;

     private void Update()
     {
          if(isWithinRange)
          {
               InteractPopup.transform.LookAt(Camera.main.gameObject.transform);
          }
     }

     public void InRange()
     {
               isWithinRange = true;
               InteractPopup.gameObject.SetActive(true);
     }

     public void OutRange()
     {
               isWithinRange = false;
               InteractPopup.gameObject.SetActive(false);
     }

}
