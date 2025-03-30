using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    private Camera _mainCamera;

    private Ray _ray;
    private RaycastHit _hit;

    public bool riverClicked = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    public void RiverClicked() // Raycast to detect when the mouse clicks the River
    {
        // Detect mouse click.
        if (Input.GetMouseButtonDown(0))
        {
            
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit))
            {
                Debug.Log("River Clicked");
                riverClicked = true; // set riverClicked to true
            }
        }
    }
}
