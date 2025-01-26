using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;

public class Chronometer : MonoBehaviour
{
    private const string TEXT_FORMAT = "{0:D2}:{1:D2}.{2:D3}";  //format 00:00:000

    //latwe to be a public argument passed in the hierarchy
    private bool raceOngoing;

    private float chrono;

    void Start()
    {
        
    }

    //FixedUpdate so chronometer is independent of the frameRate (maybe change how many time per second FixedUpdate
    //should be called in the project setting so the chronometer will be more precise) --> !!! not too low for performances
    void FixedUpdate()
    {
        if (raceOngoing == true){
            chrono += Time.deltaTime;
        }
    }

    public void StartChrono() {
        chrono = 0f;
        raceOngoing = true;
    }

    public void StopChrono(){
        raceOngoing = false;
    }

    public float GetChrono()
    {
        return chrono;
    }

    public static void DisplayChrono(TextMeshProUGUI textMeshPro, float chrono)
    {
        if (chrono >= 0) {
            int minutes = Mathf.FloorToInt(chrono / 60);
            int seconds = Mathf.FloorToInt(chrono % 60);
            int milliseconds = Mathf.FloorToInt((chrono * 1000) % 1000);

            textMeshPro.text = string.Format(TEXT_FORMAT, minutes, seconds, milliseconds);
        }
        else
        {
            textMeshPro.text = "-";
        }
    }
}
