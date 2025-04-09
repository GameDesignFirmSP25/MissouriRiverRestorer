using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public Camera _mainCamera;

    private Ray ray;
    private RaycastHit hit;

    public bool riverClicked = false;
    public static bool isClickable = false;

    private void Update()
    {
        RiverClicked();
    }

    public void RiverClicked() // Raycast to detect when the mouse clicks the River
    {
        if (isClickable)
        {
            // Detect left mouse click.
            if (Input.GetMouseButtonDown(0))
            {

                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the clicked object has the "River" tag
                    if (hit.collider.CompareTag("River"))
                    {
                        Debug.Log("River Clicked"); // Debug.Log
                        riverClicked = true; // Set bool riverClicked to true
                    }
                }
            }
        }
    }
}
