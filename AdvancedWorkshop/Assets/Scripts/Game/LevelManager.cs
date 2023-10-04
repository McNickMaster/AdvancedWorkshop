using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Level firstLevel;

    public Level[] levelOrder;

    private int currentLevelIndex = 0;

    private Scene currentLevel;

    // Start is called before the first frame update
    void Awake()
    {
        
        //LoadLevel(firstLevel);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadFirstLevel()
    {

    }

    void LoadNextLevel()
    {

    }

    private void LoadLevel(Level level)
    {
        //ill finish this at home
        SceneManager.LoadSceneAsync(level.ToString(), LoadSceneMode.Additive);
        Debug.Log(level.ToString());
    }
}


public enum Level
{
    UNDERGROUND, FARM


}

