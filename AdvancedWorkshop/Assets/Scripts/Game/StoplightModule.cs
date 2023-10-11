using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class StoplightModule : MonoBehaviour
{
    public Image stoplightImage;
    public Image[] stoplightImages = new Image[3];
    public Color state = Color.green;
    public float[] timings = new float[3] { 6f, 1f, 2f };
    public float speed_threshold = 0.1f;

    private float timer = 0;
    private float cycleTime = 0.0f;

    [SerializeField]
    private bool tracking_active;

    private void Awake()
    {
        cycleTime = timings[0] + timings[1] + timings[2];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= cycleTime)
        {
            timer = 0;
            if(state != Color.green)
            {
                Green();
            }
        } else if(timer >= cycleTime - timings[2])
        {
            if (state != Color.red)
            {
                Red();
            }
        } else if (timer >= cycleTime - timings[1] - timings[2])
        {

            if (state != Color.yellow)
            {
                Yellow();
            }
        } else
        {
            if (state != Color.green)
            {
                Green();
            }
        }


        if(tracking_active)
        {
            if(PlayerMovement.instance.GetSpeed() >= speed_threshold)
            {
                state = Color.red;
                stoplightImage.color = state;


                tracking_active = false;
                //RespawnPlayer();
                Invoke("RespawnPlayer", 0.5f);
                
                Debug.Log("player has been detected, respawning at checkpoint...");
            }
        }

    }

    void RespawnPlayer()
    {
        GameManager.Instance.RespawnPlayer();
        timer = 0;
    }

    void Red()
    {
        state = Color.red;
        stoplightImage.color = state;
        tracking_active = true;
        //stoplightImage = stoplightImages[2];
    }

    void Yellow()
    {
        state = Color.yellow;
        stoplightImage.color = state;
        //stoplightImage = stoplightImages[1];
    }

    void Green()
    {
        state = Color.green;
        stoplightImage.color = state;

        tracking_active = false;

        //stoplightImage = stoplightImages[0];
    }
}
