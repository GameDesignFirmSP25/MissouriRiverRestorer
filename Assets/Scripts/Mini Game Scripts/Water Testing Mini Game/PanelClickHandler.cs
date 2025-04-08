using UnityEngine;
using UnityEngine.EventSystems;

public class PanelClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool isPanelClicked = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Panel CLicked!");
        isPanelClicked = true;
        if (WaterTestingManager.isFirstWaterTestComplete && !WaterTestingManager.isSecondWaterTestComplete)
        {
            WaterTestingManager.cleanWaterPanelActive = false;
        }
        
        if (WaterTestingManager.isSecondWaterTestComplete)
        {
            WaterTestingManager.greatJobPanelActive = false;
        }
    }
}
