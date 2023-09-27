using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StoplightModule : MonoBehaviour
{
    public Color state = Color.green;
    public float[] timings = new float[3] { 6f, 1f, 2f };
    public float speed_threshold = 0.1f;

    private float timer = 0;
    private float cycleTime = 0.0f;

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
            Green();
        } else if(timer >= cycleTime - timings[2])
        {
            Red();
        } else if (timer >= cycleTime - timings[1] - timings[2])
        {
            Yellow();
        } else
        {
            Green();
        }


        if(tracking_active)
        {
            if(PlayerMovement.instance.GetSpeed() >= speed_threshold)
            {
                tracking_active = false;
                Debug.Log("player has been detected, respawning at checkpoint...");
            }
        }

    }

    void Red()
    {
        state = Color.red;
        tracking_active = true;
    }

    void Yellow()
    {
        state = Color.yellow;
    }

    void Green()
    {
        state = Color.green;

        tracking_active = false;
    }
}
