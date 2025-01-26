using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;

public class Countdown : MonoBehaviour
{
    public enum CountdownStatus
    {
        Running,    
        EndOfCountdown,
        Disabled      
    }

    private float COUNTDOWN_TIME = 1.2f;
    private float GO_DISPLAYS_DURATION = 0.5f;

    public CountdownStatus status;
    private float countdownValue;

    public TextMeshProUGUI countdownDisplay;

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        countdownValue -= Time.deltaTime;

        if (status == CountdownStatus.Running)
        {
            if (countdownValue <= 0) 
            { 
                status = CountdownStatus.EndOfCountdown;
            }
            else //so it does not display negative values (little bug sometimes
            {
                //display countdown
                countdownDisplay.text = string.Format("{0:D1}.{1:D1}", Mathf.FloorToInt(countdownValue), Mathf.FloorToInt((countdownValue * 10) % 10));    // --> precise timing
                //countdownDisplay.text = string.Format("{0}", Mathf.FloorToInt(countdownValue) + 1); // --> 3, 2, 1
            }

        }
        else if(status == CountdownStatus.EndOfCountdown)
        {
            countdownDisplay.text = "Go!";
            status = Countdown.CountdownStatus.Disabled;
        }
        else if(status == CountdownStatus.Disabled)
        {
            if (countdownValue <= -GO_DISPLAYS_DURATION) //to let "go" be displayed for a moment
            {
                countdownDisplay.text = ""; //hide countdown
            }
        }
    }

    public void StartCountdown()
    {
        countdownValue = COUNTDOWN_TIME;
        status = CountdownStatus.Running;
    }

}
