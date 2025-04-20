using System.Drawing;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZoneGizmoBox : MonoBehaviour
{

    // public Color boxColor = Color.cyan; // Change the color in the inspector
    public Vector3 position = Vector3.zero; // Position of the gizmo box
    public Vector3 size = Vector3.one; // Size of the gizmo box

    void OnDrawGizmos()
    {
        // Gizmos.color = boxColor; //Set the color of the next draw
        Gizmos.DrawCube(position, size); //Draw a cube gizmo and give it a position and a size
    }
}
