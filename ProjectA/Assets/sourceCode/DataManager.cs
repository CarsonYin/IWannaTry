using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public LevelNames currentLevel;

    public int test;

    public enum LevelNames{
        Level0 = 0,
        Level1 = 1,
        Level2 = 2
    }


    void Awake() {
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

    void Start() {

        SceneManager.LoadScene(currentLevel.ToString());
    }

}
