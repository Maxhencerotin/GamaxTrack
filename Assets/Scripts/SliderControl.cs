using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    public Slider mySlider; // Reference to the slider

    void Start()
    {
        // 0.0f Set to half of the slider's range
        mySlider.value = 0.0f; 
    }

    //return a value between -1 (left) and 1 (right)
    public float getTurnValue()
    {
        return -mySlider.value;
    }

    //return a value between -1 (both extremity) and 1 (middle)
    public float getAccelValue()
    {
        //return (-2 * Mathf.Abs(mySlider.value) + 1.0f); //not as user friendly as the new function (especially when stuck)
        return -2 * mySlider.value * mySlider.value + 1; //breakes more in the extremities
    }

    //return a value between 0 and 1 (from edge to middle) (0 (0%) means edges and 100% means middle)
    public float getSlidingValue()
    {
        return 1 - Mathf.Abs(mySlider.value);
    }

    public float getValue()
    {
        return mySlider.value;
    }
}
