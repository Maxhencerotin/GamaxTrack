using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DriftingZone : MonoBehaviour
{

    private const float DISTANCE_WARNING = 0.3f;    //percentage of the slider size from the gauges from which the warning start to appear 
    private const float WARNING_ALPHA_MAX = 0.6f;
    private const float WARNING_ALPHA_MIN = 0.0f;

    //SerializeField is used so I can see it in the hierarchy but let it private
    //(i used it here to try but i shoul maybe extend that to the whole project later)
    [SerializeField] private GameObject driftingGauge;
    public Image warningColour;
    public SliderControl sliderControl;

    public CarMovement car;

    // Start is called before the first frame update
    void Start()
    {
        //should I maybe initialize the positions ?? --> I do that in the hierarchy
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach (Transform gauge in driftingGauge.transform)
        {
            //update filling
            Image gaugeImage = gauge.GetComponent<Image>();
            gaugeImage.fillAmount = car.GetSlidingLimit();

            //update cursor
            RectTransform gaugePosition = gauge.GetComponent<RectTransform>();
            RectTransform cursorPosition = gauge.GetChild(0).GetComponent<RectTransform>();
            float NewCursorPositionX = gaugePosition.rect.width * car.GetSlidingLimit();
            cursorPosition.anchoredPosition = new Vector2(NewCursorPositionX, cursorPosition.anchoredPosition.y);

            //check warning colour
           
            Color color = warningColour.color;
            if (sliderControl.getSlidingValue() - car.GetSlidingLimit() <= DISTANCE_WARNING)  //when we are at DistanceWarning from the slidingLimit
            {
                if (sliderControl.getSlidingValue() - car.GetSlidingLimit() <= 0)
                {
                    color.a = WARNING_ALPHA_MAX;
                }
                else
                {
                    //with rule of three : proportion of distances gives intensity of colour
                    float alphaRange = WARNING_ALPHA_MAX - WARNING_ALPHA_MIN;
                    float currentSegment = sliderControl.getSlidingValue() - car.GetSlidingLimit();
                    color.a = alphaRange*((DISTANCE_WARNING -currentSegment)/DISTANCE_WARNING);  
                }  
            }
            else
            {
               
                color.a = 0.0f; 
                
            }
            warningColour.color = color;
        }

    }
}
