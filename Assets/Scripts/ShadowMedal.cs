using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;

public class ShadowMedal : MonoBehaviour
{
    public int levelNumber;
    public GameData.MedalType MedalType;
    public TextMeshProUGUI TextInMedal;

    // Start is called before the first frame update
    void Start()
    {

        if (levelNumber < 1 || levelNumber > GameData.NUMBER_LEVEL)
        {
            Debug.Log("LevelNumber is greater than the number of levels of the game");
        }
        GameData.LevelData level = GameData.LEVELS[levelNumber - 1];

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

        TextInMedal.text = timeToBeat.ToString();
    }
}
