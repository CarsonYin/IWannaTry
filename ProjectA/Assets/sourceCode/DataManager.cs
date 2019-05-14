using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public LevelNames currentLevel;

    // Player info
    public int currentSavePoint;
    public bool haveShield;
    public int shieldFragmentNumber;

    public int deathCount;

    // Ends Player info

    public Dropdown dropdown;

    public StartOptions startOptions;


    public enum LevelNames
    {
        StartPage = 0,
        Level0 = 1,
        Level1 = 2,
        Level2 = 3,
        OldLevel1 = 4,
        OldLevel2 = 5,
        Test = 6,

    }

    public enum StartOptions
    {
        NewGame = 0,
        Respawn = 1,
        NextLevel = 2
    }

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SceneManager.LoadScene(LevelNames.StartPage.ToString());

        //dropdown = GameObject.Find("Cavans").transform.GetChild(0).gameObject.GetComponent<Dropdown>();

        DataManager.Instance.currentLevel = LevelNames.Level0;

        DataManager.Instance.currentSavePoint = -1;  // Means Did not reach any savepoint at current level




    }

    public void StartGame()
    {
        //currentLevel = (LevelNames)levelChosen;
        DataManager.Instance.startOptions = StartOptions.NewGame;
        SceneManager.LoadScene(DataManager.Instance.currentLevel.ToString());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeLevel(int level)
    {
        DataManager.Instance.currentLevel = (LevelNames)(level + 1);

    }

    public void ChangeSavePoint(int number)
    {
        DataManager.Instance.currentSavePoint = number;
    }

    public void ResetLevel()
    {
        DataManager.Instance.currentSavePoint = -1;
        DataManager.Instance.haveShield = false;
        DataManager.Instance.shieldFragmentNumber = 0;
    }

    // startOptions = 0  ----  New Game
    // startOptions = 1  ----  Respawn
    public void RespawnSettings(StartOptions startOptions)
    {
        GameObject player = GameObject.Find("Player").gameObject;
        GameObject sceneManager = GameObject.Find("CurrentSceneManager").gameObject;

        switch (startOptions)
        {


            case StartOptions.NewGame:
                ResetLevel();
                break;
            case StartOptions.Respawn:
                sceneManager.GetComponent<CurrentSceneManager>().currentSavePoint = DataManager.Instance.currentSavePoint;
                if (DataManager.Instance.currentSavePoint > -1)
                {
                    player.transform.position = sceneManager.transform.GetChild(DataManager.Instance.currentSavePoint).position;
                }
                //player.gameObject.GetComponent<PlayerControl>().haveShield = DataManager.Instance.haveShield;
                //player.gameObject.GetComponent<PlayerControl>().shieldFragmentNumber = DataManager.Instance.shieldFragmentNumber;
                break;
            case StartOptions.NextLevel:
                Debug.Log(shieldFragmentNumber);
                player.gameObject.GetComponent<PlayerControl>().haveShield = DataManager.Instance.haveShield;
                player.gameObject.GetComponent<PlayerControl>().shieldFragmentNumber = DataManager.Instance.shieldFragmentNumber;
                break;
            default:
                sceneManager.GetComponent<CurrentSceneManager>().currentSavePoint = -1;
                break;
        }
    }

    //public void BackToMenu() {
    //  //  GameObject.Find("Cavans").transform.GetChild(0).gameObject.GetComponent<Dropdown>().value = (int)DataManager.Instance.currentLevel - 1;

    //}

}
