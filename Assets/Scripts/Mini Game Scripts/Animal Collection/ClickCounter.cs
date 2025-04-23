using UnityEngine;
using UnityEngine.UI;

public class ClickCounter : MonoBehaviour
{
    private Slider slider;

    public float FillSpeed = 0.5f;
    private float targetProgress = 0;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>(); // Get slider component
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value < targetProgress)
            slider.value += FillSpeed * Time.deltaTime; // If the slifer value is less than targetProgress variable, slider value is equal to itself plus FillSpeed variable * Time.deltaTime
    }

    // Add progress to bar
    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress; // targetProcess is equal to slider value + newProgress
    }
}
