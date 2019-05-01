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


    public enum LevelNames
    {
        StartPage = 0,
        Test = 1,
        Level1 = 2,
        Level2 = 3,
        LevelX = 4,
        Level3 = 5
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

        DataManager.Instance.currentLevel = LevelNames.Test;

        DataManager.Instance.currentSavePoint = -1;  // Means Did not reach any savepoint at current level

    }

    public void StartGame()
    {
        //currentLevel = (LevelNames)levelChosen;
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

    public void RespawnSettings()
    {
        GameObject player = GameObject.Find("Player").gameObject;
        GameObject sceneManager = GameObject.Find("CurrentSceneManager").gameObject;
        sceneManager.GetComponent<CurrentSceneManager>().currentSavePoint = DataManager.Instance.currentSavePoint;
        if (DataManager.Instance.currentSavePoint > -1)
        {
            player.transform.position = sceneManager.transform.GetChild(DataManager.Instance.currentSavePoint).position;
        }
        player.gameObject.GetComponent<PlayerControl>().haveShield = DataManager.Instance.haveShield;
        player.gameObject.GetComponent<PlayerControl>().shieldFragmentNumber = DataManager.Instance.shieldFragmentNumber;
    }

    //public void BackToMenu() {
    //  //  GameObject.Find("Cavans").transform.GetChild(0).gameObject.GetComponent<Dropdown>().value = (int)DataManager.Instance.currentLevel - 1;

    //}

}
