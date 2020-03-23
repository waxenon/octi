using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] Slider mySlider;
    [SerializeField] float startSliderValue = 1;
    [SerializeField] bool isVolumeSlider = false;

    private void Start()
    {
        startSliderValue = Settings.volume;
        mySlider.value = startSliderValue;
    }

    private void Update()
    {
        if(mySlider.value != startSliderValue)
        {
            startSliderValue = mySlider.value;
            if(isVolumeSlider)
            {
                Settings.volume = mySlider.value;
            }
        }
    }
}
