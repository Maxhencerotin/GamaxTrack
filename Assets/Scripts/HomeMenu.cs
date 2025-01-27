using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using TMPro.Examples;

public class HomeMenu : MonoBehaviour
{

    public TextMeshProUGUI TotalTimeDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float TotalTime = 0;
        for (int i = 1; i <= GameData.NUMBER_LEVEL; i++)
        {
            //float leveliTime = PlayerPrefs.GetFloat(GameData.BESTTIME_DATA_KEYWORD + "Level" + i, -1);
            float leveliTime = SaveManager.LoadData().bestTime[i - 1];
            if (TotalTime >= 0 && leveliTime >= 0)
            {
                TotalTime += leveliTime;
            }
            else
            {
                TotalTime = -1;
            }
        }
        Chronometer.DisplayChrono(TotalTimeDisplay, TotalTime);

    }

    public void gotoLevel(string levelx)
    {
        SceneManager.LoadScene(levelx);     
    }

}

