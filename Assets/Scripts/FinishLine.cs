using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public Chronometer chrono;
    public CarMovement car;

    private bool finishLinePassed;

    void Start()
    {
        finishLinePassed = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            finishLinePassed = true;
        }
    }

    public bool FinishLineIsPassed()
    {
        return finishLinePassed;
    }
}
