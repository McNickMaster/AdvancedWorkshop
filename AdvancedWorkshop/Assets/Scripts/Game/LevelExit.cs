using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private LevelManager levelManager;

    public Level nextLevel;

    // Start is called before the first frame update
    void Awake()
    {
        levelManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer.Equals(10))
        {

            Debug.Log("loading next level...");
            levelManager.LoadNextLevel(nextLevel);
            this.enabled = false;
            this.gameObject.SetActive(false);
        }
    }
}
