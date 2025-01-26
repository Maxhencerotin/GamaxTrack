using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //SHOULD PUT ALL THOSE CONSTANTS IN UPPERCASE WHEN I HAVE TIME

    public Transform target;    //object to follow (=car)

    private float followDelay = 0.1f; // delay in second
    private Vector3 initialOffset;
    private Vector3 velocity = Vector3.zero; // speed of camera (SmoothDamp will calculate that)
    
    //for rotation of camera
    private float rotationVelocity = 0; //speed of camera rotation (SmoothDamp will calculate that)
    private float currentRotation;  //easier to keep the z component of the rotation here than to work with quaternions
    private float rotationDelay = 2.5f; //in second
    private const float MAXIMUM_CAMERA_ANGLE = 15f;

    //for camera dezoom
    private float currentZoom;
    private float zoomVelocity = 0f;            //speed of the zoom (SmoothDamp will calculate that)
    private float minZoomIn = 20.0f;            //camera zoom when not moving
    private float zoomSmoothness = 0.4f;        //How fast the zoom transitions
    private float maxZoomOut = 50.0f;           //maximum dezoom
    private float maxDezoomCarSpeed = 30.0f;   //if the car goes faster then this it will not dezoom more (and bigger this nbr is, slower the zoom effect is)

    public SliderControl slider;

    private void Start()
    {
        initialOffset = transform.position - target.position;
        currentZoom = maxZoomOut;   //to make such that when game start we have a zoomIn effect
        Camera.main.orthographicSize = currentZoom;
    }


    void LateUpdate()
    {
        //so the car does not follow the rotation of the car    //obsolete with the new rotation of camera system
        //transform.rotation = Quaternion.identity;

        //camera stay always on top of the car  //probably obsolete now
        //transform.position = new Vector3(target.position.x + initialPosition.x, target.position.y + initialPosition.y, initialPosition.z);    

        //camera follow the car with a little delay (but seems not very fluid --> now fluid with extrapolate in the car rigidbody)
        transform.position = Vector3.SmoothDamp(transform.position, target.position + initialOffset, ref velocity, followDelay);

        //camera rotate a little bit related to the Pad
        float targetRotation = Mathf.Lerp(-MAXIMUM_CAMERA_ANGLE, MAXIMUM_CAMERA_ANGLE, (slider.getValue() + 1f) / 2f);    //because slider has value between -1 and 1
        currentRotation = Mathf.SmoothDamp(currentRotation, targetRotation, ref rotationVelocity, rotationDelay);
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);

        //camera dezoom when the car is going fast
        float targetZoom = Mathf.Lerp(minZoomIn, maxZoomOut, velocity.magnitude/maxDezoomCarSpeed);
        currentZoom = Mathf.SmoothDamp(currentZoom, targetZoom, ref zoomVelocity, zoomSmoothness);
        Camera.main.orthographicSize = currentZoom;


    }
}
