using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private LevelManager levelManager;

    public Level nextLevel;
    public bool last = false;
    private bool flag = false;

    // Start is called before the first frame update
    void Awake()
    {
        levelManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer.Equals(10) && !flag)
        {
            if(last)
            {
                levelManager.LoadLastLevel();
            } else 
            {
                levelManager.LoadNextLevel(nextLevel);
            }
            Debug.Log("loading next level...");
            Destroy(this.gameObject);
            flag = true;
        }
    }
}
