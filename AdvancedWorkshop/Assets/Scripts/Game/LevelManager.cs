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
    public Level currentLevel;

    // Start is called before the first frame update
    void Awake()
    {
        LoadFirstLevel();
    }

    // Update is called once per frame
    void Update()
    {

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

        //Debug.Log("index was " + (currentLevelIndex - 1) + " is now " + currentLevelIndex);
    }

    public void LoadNextLevel(Level level)
    {
        UnloadLevel(levelOrder[currentLevelIndex]);
        currentLevelIndex++;
        LoadLevel(level);
    }

    public void LoadLastLevel()
    {
        LoadLevel(Level.END);
    }

    private void LoadLevel(Level level)
    {
        SceneManager.LoadSceneAsync(level.ToString(), LoadSceneMode.Additive);
        currentLevel = level;
        Debug.Log(level.ToString());
    }

    private void UnloadLevel(Level level)
    {
        SceneManager.UnloadSceneAsync(level.ToString());
    }
}


public enum Level
{
    UNDERGROUND, FARM, PEAK, END


}

