using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameSuperviser : MonoBehaviour
{
    public int levelNumber; //for a lot of component in the game hierarchy (for modularity)
    public Chronometer chrono;
    public Countdown countdown;
    public CarMovement car;
    public GhostMovement ghost;
    public FinishLine finishLine;

    public GameObject Menu;
    private float MenuApparitionDuration = 1f;

    //for medal apparition with effect
    public Animator bronzeMedalAnimator;
    public Animator silverMedalAnimator;
    public Animator goldMedalAnimator;
    public Animator authorMedalAnimator;

    public TextMeshProUGUI chronoDisplay;
    public TextMeshProUGUI chronoDisplayInMenu;
    public TextMeshProUGUI bestTimeDisplayInMenu;

    private bool gameIsRunning;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();       //for debugging the code

        gameIsRunning = false;
        countdown.StartCountdown();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!(countdown.status == Countdown.CountdownStatus.Running) && !gameIsRunning)
        {
            car.SetCanMove(true);
            ghost.SetCanMove(true);
            chrono.StartChrono();
            gameIsRunning = true;
        }

        if(finishLine.FinishLineIsPassed())
        {
            chrono.StopChrono();
            car.SetCanMove(false);
            ghost.SetCanMove(false);

            /*
            if (chrono.GetChrono() < PlayerPrefs.GetFloat(GameData.BESTTIME_DATA_KEYWORD + SceneManager.GetActiveScene().name, float.PositiveInfinity))  //infinity is default value if no value exists in PlayerPrefs
            {
                PlayerPrefs.SetFloat(GameData.BESTTIME_DATA_KEYWORD + SceneManager.GetActiveScene().name, chrono.GetChrono());
                PlayerPrefs.Save();
            }
            */

            
            SavedData savedData = SaveManager.LoadData();
            if (chrono.GetChrono() < savedData.bestTime[levelNumber - 1])  //infinity is default value if no value exists in PlayerPrefs
            {
                savedData.bestTime[levelNumber - 1] = chrono.GetChrono();
                savedData.ghostXList[levelNumber - 1] = car.GetXList();
                savedData.ghostYList[levelNumber - 1] = car.GetYList();
                savedData.ghostRotationList[levelNumber - 1] = car.GetRotationList();
                SaveManager.SaveData(savedData);
            }


            StartCoroutine(FadeInCoroutine());
            Menu.SetActive(true);

            //for medal apparition with effect
            GameData.LevelData level = GameData.LEVELS[levelNumber - 1];
            if (chrono.GetChrono() < level.bronzeTime)
            {
                bronzeMedalAnimator.enabled = true;
            }
            if (chrono.GetChrono() < level.silverTime)
            {
                silverMedalAnimator.enabled = true;
            }
            if (chrono.GetChrono() < level.goldTime)
            {
                goldMedalAnimator.enabled = true;
            }
            if (chrono.GetChrono() < level.authorTime)
            {
                authorMedalAnimator.enabled = true;
            }
        }

        Chronometer.DisplayChrono(chronoDisplay, chrono.GetChrono());
        Chronometer.DisplayChrono(chronoDisplayInMenu, chrono.GetChrono()); //will not show if menu is not open

        //Chronometer.DisplayChrono(bestTimeDisplayInMenu, PlayerPrefs.GetFloat(GameData.BESTTIME_DATA_KEYWORD + SceneManager.GetActiveScene().name, -1));   //-1 is default value
        Chronometer.DisplayChrono(bestTimeDisplayInMenu, SaveManager.LoadData().bestTime[levelNumber - 1]);
    }

    public void OnMenuClic()
    {
        Time.timeScale = 0f;
        Menu.SetActive(true);
    }

    public void OnPlayClic()
    {
        Time.timeScale = 1f;
        if (finishLine.FinishLineIsPassed())
        {
            RestartGame();
        }
        else
        {
            Menu.SetActive(false);
        }
    }

    public void OnExitClic()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);  //0 is the index of home menu (SceneManager.LoadScene("Home");)
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator FadeInCoroutine()
    {
        float elapsedTime = 0f;

        CanvasGroup menuVisuals = Menu.GetComponent<CanvasGroup>();

        // Make the panel alpha transition from 0 to 1
        while (elapsedTime < MenuApparitionDuration)
        {
            elapsedTime += Time.deltaTime;
            menuVisuals.alpha = Mathf.Lerp(0f, 1f, elapsedTime / MenuApparitionDuration);
            yield return null;  //wait end of frame before continuing
        }
        menuVisuals.alpha = 1f; //to prevent bug
    }

}
