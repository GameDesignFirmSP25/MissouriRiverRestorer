using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static UnityEngine.UI.Image;

public class PlayerInteraction : MonoBehaviour
{
     public InteractableObject CurrentInteractionObject;
     public GuidebookUI GBUI;

     public GameObject interactPanel;
     public Image interactPanelImage;
     public TextMeshProUGUI interactPanelText;

     public float castRadius = 0.5f;
     public float maxDistance = 2f;
     public LayerMask obstacleLayer;

     // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Start()
     {
        interactPanelImage = interactPanel.GetComponent<Image>();
        interactPanelImage.enabled = false;
        interactPanelText = interactPanel.GetComponentInChildren<TextMeshProUGUI>();
        interactPanelText.text = "";
     }

    // Update is called once per frame
    void Update()
    {
         CheckDistance();
         Spherecast();
         Interaction();
    }

     private void CheckDistance()
     {
          if(CurrentInteractionObject == null) return;

          // If out of range, only call OutRange if still in range
          if (Vector3.Distance(transform.position, CurrentInteractionObject.transform.position) > maxDistance)
          {
               if (CurrentInteractionObject.isWithinRange)
                   CurrentInteractionObject.OutRange();
               CurrentInteractionObject = null;
               return;
          }

        // If already interacted, only call OutRange if still in range
          if (CurrentInteractionObject.hasBeenInteractedWith)
          {
               if (CurrentInteractionObject.isWithinRange)
                   CurrentInteractionObject.OutRange();
               return;
          }
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

               if (nextObject != null)
               {
                    if (CurrentInteractionObject != nextObject)
                    {
                        if (CurrentInteractionObject != null && CurrentInteractionObject.isWithinRange)
                            CurrentInteractionObject.OutRange();

                        CurrentInteractionObject = nextObject;
                    }
                    // Only call InRange if not already in range and not already interacted with
                    if (!CurrentInteractionObject.isWithinRange && !CurrentInteractionObject.hasBeenInteractedWith)
                        CurrentInteractionObject.InRange();
               }
          }
          else
          {
               if (CurrentInteractionObject != null && CurrentInteractionObject.isWithinRange)
               {
                   CurrentInteractionObject.OutRange();
                   CurrentInteractionObject = null;
               }
          }
     }

     private void Interaction()
     {
          if (CurrentInteractionObject == null) return;

          if(Input.GetKeyDown(KeyCode.E)) 
          {
               // Set object found to unlock the page before loading
               ObjectManager.instance.SetObjectFound(CurrentInteractionObject.data.Name);
               // load correct page
               DynamicGuidebook.instance.GBUI.OnBookButton(ObjectManager.instance.GetIndexByName(CurrentInteractionObject.data.Name));
          }
     }
}
