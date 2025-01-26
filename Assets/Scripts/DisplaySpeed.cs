using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplaySpeed : MonoBehaviour
{
    private const float CONVERSION_KMH = 3.6f;

    public CarMovement car;

    private TextMeshProUGUI speedDisplay;

    // Start is called before the first frame update
    void Start()
    {
        speedDisplay = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        speedDisplay.text = (int)(car.getSpeed() * CONVERSION_KMH) + " km/h";
    }
}
