using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterVis : MonoBehaviour
{
    [SerializeField] private Slider paceSlider;
    [SerializeField] private Slider friskinessSlider;
    
    void Update()
    {
        paceSlider.value = Mathf.MoveTowards(paceSlider.value, Meters.Instance.Pace, .03f);
        friskinessSlider.value = Mathf.MoveTowards(friskinessSlider.value, Meters.Instance.Friskiness, .03f);
    }
}
