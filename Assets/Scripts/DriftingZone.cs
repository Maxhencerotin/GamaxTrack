using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DriftingZone : MonoBehaviour
{

    //SerializeField is used so I can see it in the hierarchy but let it private
    //(i used it here to try but i shoul maybe extend that to the whole project later)
    [SerializeField] private GameObject driftingGauge;

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
        }

    }
}
