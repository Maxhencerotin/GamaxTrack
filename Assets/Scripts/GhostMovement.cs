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

    // Update is called once per frame
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

}
