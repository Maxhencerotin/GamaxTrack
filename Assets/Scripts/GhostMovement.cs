using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public GameSuperviser gameSuperviser;
    private List<float> positionsXList;
    private List<float> positionsYList;
    private List<float> rotationList;
    private int fixedFrameNumber;

    // Start is called before the first frame update
    void Start()
    {
        fixedFrameNumber = 0;
        //Check if null
        positionsXList = SaveManager.LoadData().ghostXList[gameSuperviser.levelNumber - 1];
        positionsYList = SaveManager.LoadData().ghostYList[gameSuperviser.levelNumber - 1];
        rotationList = SaveManager.LoadData().ghostRotationList[gameSuperviser.levelNumber - 1];

    }


    private float interpolationAlpha = 0f; // ratio entre la frame précédente et actuelle

    void FixedUpdate()
    {
        int sizePosList = positionsXList.Count;
        if (fixedFrameNumber < sizePosList - 1)
        {
            fixedFrameNumber++; // on avance d’une frame FixedUpdate
            interpolationAlpha = 0f; // reset alpha pour l’interpolation
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame (better than fixed update for no saccade and performance)
    void Update()
    {
        if (fixedFrameNumber == 0 || fixedFrameNumber >= positionsXList.Count) return;

        // calcul du ratio alpha basé sur le temps depuis le dernier FixedUpdate
        interpolationAlpha += Time.deltaTime / Time.fixedDeltaTime;
        interpolationAlpha = Mathf.Clamp01(interpolationAlpha);

        int indexA = Mathf.Clamp(fixedFrameNumber - 1, 0, positionsXList.Count - 1);
        int indexB = Mathf.Clamp(fixedFrameNumber, 0, positionsXList.Count - 1);

        // interpolation position
        transform.position = Vector3.Lerp(
            new Vector3(positionsXList[indexA], positionsYList[indexA], 0),
            new Vector3(positionsXList[indexB], positionsYList[indexB], 0),
            interpolationAlpha
        );

        // interpolation rotation
        transform.eulerAngles = new Vector3(
            0, 0,
            Mathf.LerpAngle(rotationList[indexA], rotationList[indexB], interpolationAlpha)
        );
    }


    //
    /*
    void FixedUpdate()
    {
        int sizePosList = positionsXList.Count;  //should be the same as positionsYList and rotationList
        if (fixedFrameNumber < sizePosList)
        { 
            transform.position = new Vector3(positionsXList[fixedFrameNumber], positionsYList[fixedFrameNumber], 0);
            transform.eulerAngles = new Vector3(0, 0, rotationList[fixedFrameNumber]);
        }
        else
        {
            gameObject.SetActive(false);
        }
        ++fixedFrameNumber;
    }
    */


}
