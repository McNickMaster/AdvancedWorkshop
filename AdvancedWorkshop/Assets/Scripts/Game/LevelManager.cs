using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Level firstLevel;

    public Level[] levelOrder;

    public Scene mainScene;
    private int currentLevelIndex = 0;
    private Level currentLevel;

    // Start is called before the first frame update
    void Awake()
    {
        LoadFirstLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            UnloadLevel(Level.FARM);
            currentLevelIndex = 0;
            LoadFirstLevel();
        }
    }

    void LoadFirstLevel()
    {
        LoadLevel(firstLevel);
    }

    public void LoadNextLevel()
    {
        UnloadLevel(levelOrder[currentLevelIndex]);
        currentLevelIndex++;
        LoadLevel(levelOrder[currentLevelIndex]);
    }

    private void LoadLevel(Level level)
    {
        SceneManager.LoadSceneAsync(level.ToString(), LoadSceneMode.Additive);
        currentLevel = level;
        StoplightModule.instance.Reset();
        Debug.Log(level.ToString());
    }

    private void UnloadLevel(Level level)
    {
        SceneManager.UnloadSceneAsync(level.ToString());
    }
}


public enum Level
{
    UNDERGROUND, FARM, PEAK


}

