using UnityEngine;
using UnityEngine.EventSystems;

public class CleanWaterPanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool isCleanWaterPanelClicked = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clean Water Panel Clicked!");
        isCleanWaterPanelClicked = true;

        if (WaterTestingManager.isFirstWaterTestComplete && !WaterTestingManager.isSecondWaterTestComplete)
        {
            WaterTestingManager.cleanWaterPanelActive = false;
        }
    }
}
