using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public LevelNames currentLevel;

    // Player info
    private int currentSavePoint;
    public bool haveShield;
    public int shieldFragmentNumber;
    // Ends Player info

    public int CurrentSavePoint { get => currentSavePoint; set => currentSavePoint = value; }

    public enum LevelNames
    {
        Level1 = 1,
        Level2 = 2
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

        SceneManager.LoadScene(currentLevel.ToString());
        CurrentSavePoint = -1;  // Means Did not reach any savepoint at current level

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
