using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float follow_strength = 0.2f, follow_speed_mod = 100;
    public Vector3 offset = new Vector3(0, 2, -10);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 playerVelocity = PlayerMovement.instance.rb.velocity * follow_strength;
        Vector3 targetPos = new Vector3(-playerVelocity.x, -playerVelocity.y * 0.7f, 0) + offset;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, follow_speed_mod * Time.deltaTime);
    }
}
