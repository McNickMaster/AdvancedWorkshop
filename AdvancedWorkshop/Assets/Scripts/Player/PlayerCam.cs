using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public static PlayerCam instance;


    public float follow_strength = 0.2f, follow_speed_mod = 100;
    public Vector3 offset = new Vector3(0, 2, -10);
    public Vector3 shake_offset = new Vector3(0, 0, 0);

    public float shake_duration = 0.2f, shake_amount = 0.02f;
    private float timer = 0;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Follow();

        if(timer > 0)
        {
            timer -= Time.deltaTime;
            Shake();
        } else
        {
            StopShake();
        }
    }

    void Follow()
    {
        Vector3 playerVelocity = PlayerMovement.instance.rb.velocity * follow_strength;
        Vector3 targetPos = new Vector3(-playerVelocity.x, -playerVelocity.y * 0.7f, 0) + offset + shake_offset;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, follow_speed_mod * Time.deltaTime);
       
    }

    public void StartShake()
    {
        timer = shake_duration;
    }

    public void StartShake(float duration)
    {
        timer = duration;
    }

    void Shake()
    {
        shake_offset = Random.insideUnitCircle * shake_amount;
    }

    void StopShake()
    {
        shake_offset = Vector3.zero;
    }
}
