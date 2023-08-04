using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    private Slider _slider;
    private void Start()
    { _slider = GetComponent<Slider>(); }
    public void SetExpSlider(Component component, object data)
    {
        if(data is int )
        {
            _slider.maxValue = (int) data;
        }
    }
    public void UpdateExpSlider(Component component, object data)
    { if (data is int) { _slider.value = (int)data; } }

}
