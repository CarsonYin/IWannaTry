using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public LevelNames currentLevel;

    // Player info
    public int currentSavePoint;
    public bool haveShield;
    public int shieldFragmentNumber;
    // Ends Player info

    public enum LevelNames
    {
        StartPage = 0,
        Test = 1,
        Level1 = 2,
        Level2 = 3

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
        DataManager.Instance.currentLevel = LevelNames.Test;

        SceneManager.LoadScene(LevelNames.StartPage.ToString());

        currentSavePoint = -1;  // Means Did not reach any savepoint at current level
        
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
        currentSavePoint = number;
    }

    public void RespawnSettings()
    {
        GameObject player = GameObject.Find("Player").gameObject;
        GameObject sceneManager = GameObject.Find("CurrentSceneManager").gameObject;
        sceneManager.GetComponent<CurrentSceneManager>().currentSavePoint = currentSavePoint;
        if (currentSavePoint > -1)
        {
            player.transform.position = sceneManager.transform.GetChild(currentSavePoint).position;
        }
        player.gameObject.GetComponent<PlayerControl>().haveShield = haveShield;
        player.gameObject.GetComponent<PlayerControl>().shieldFragmentNumber = shieldFragmentNumber;
    }
}
