using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class GarterSnakeClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool isGarterSnakePanelClicked = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Garter Snake panel clicked. Hiding panel...");
        isGarterSnakePanelClicked = true;
        AnimalGameManager.garterSnakePanelActive = false;
    }
}

