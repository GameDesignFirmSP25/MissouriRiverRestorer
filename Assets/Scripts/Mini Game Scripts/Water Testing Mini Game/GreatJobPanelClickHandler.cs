using UnityEngine;
using UnityEngine.EventSystems;

public class GreatJobPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool isGreatJobPanelClicked = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Great Job Panel Clicked!");
        isGreatJobPanelClicked = true;
        if (WaterTestingManager.isFirstWaterTestComplete && WaterTestingManager.isSecondWaterTestComplete)
        {
            WaterTestingManager.greatJobPanelActive = false;
        }
    }
}
