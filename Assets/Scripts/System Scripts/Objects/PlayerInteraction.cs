using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerInteraction : MonoBehaviour
{
     public InteractableObject CurrentInteractionObject;
     public GuidebookUI GBUI;

     public float castRadius = 0.5f;
     public float maxDistance = 2f;
     public LayerMask obstacleLayer;

     // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         Spherecast();
         Interaction();
    }

     private void Spherecast()
     {
          Vector3 forward = transform.forward;
          Vector3 origin = transform.position;

          RaycastHit hit;
          if (Physics.SphereCast(origin, castRadius, forward, out hit, maxDistance, obstacleLayer))
          {
               //Debug.Log("SphereCast hit: " + hit.collider.gameObject.name + " at distance: " + hit.distance);

               InteractableObject nextObject = hit.collider.gameObject.GetComponent<InteractableObject>();
               if(CurrentInteractionObject!= null)
               {
                    CurrentInteractionObject.OutRange();
               }
               CurrentInteractionObject = nextObject;
               CurrentInteractionObject.InRange();
          }
     }

     private void Interaction()
     {
          if (CurrentInteractionObject == null) return;

          if(Input.GetKeyDown(KeyCode.E)) 
          {
               // load correct page
               GBUI.OnBookButton(ObjectManager.instance.GetIndexByName(CurrentInteractionObject.data.Name));
          }
     }
}
