using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private LevelManager levelManager;

    public Level nextLevel;

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

            Debug.Log("loading next level...");
            levelManager.LoadNextLevel(nextLevel);
            Destroy(this.gameObject);
            flag = true;
        }
    }
}
