using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivateMedal : MonoBehaviour
{

    public int levelNumber;
    public GameData.MedalType MedalType;
    public Image medal;

    // Start is called before the first frame update
    void Start()
    {

        if (levelNumber < 1 || levelNumber > GameData.NUMBER_LEVEL)
        {
            Debug.Log("LevelNumber is greater than the number of levels of the game");
        }
        GameData.LevelData level = GameData.LEVELS[levelNumber - 1];

        float levelTime = PlayerPrefs.GetFloat(GameData.BESTTIME_DATA_KEYWORD + "Level" + level.levelNumber, -1);
        float timeToBeat = 0f;
        switch (MedalType)
        {
            case GameData.MedalType.Bronze:
                timeToBeat = level.bronzeTime;
                break;
            case GameData.MedalType.Silver:
                timeToBeat = level.silverTime;
                break;
            case GameData.MedalType.Gold:
                timeToBeat = level.goldTime;
                break;
            case GameData.MedalType.Author:
                timeToBeat = level.authorTime;
                break;
            default:
                break;
        }

        if (levelTime >= 0 && levelTime < timeToBeat)
        {
            medal.enabled = true;
        }
        else
        {
            medal.enabled = false;
        }
    }

}
