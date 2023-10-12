using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = 1;
        slider.value = 0;
    }

    public void SetMaxFillValue(float maxValue)
    {
        slider.maxValue = maxValue;
    }

    public void SetCurrentValue(float currentValue)
    {
        if (currentValue < 0)
        {
            slider.value = 0;
        }
        else if (currentValue > slider.maxValue)
        {
            slider.value = slider.maxValue;
        }
        else
        {
            slider.value = currentValue;
        }
    }
}
