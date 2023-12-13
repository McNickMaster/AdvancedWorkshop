using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D body2d;
    public float velocityThreshold;

    public float swingTimer = 2;
    private float currentVelocity = 0;

    void Awake()
    {
        body2d = GetComponent<Rigidbody2D>();
        currentVelocity = velocityThreshold;

        Invoke("SwitchVelocityDirection", swingTimer);
    }

    // Update is called once per frame
    void Update()
    {
        body2d.angularVelocity = currentVelocity;
    }

    void SwitchVelocityDirection()
    {

         currentVelocity = -1 * currentVelocity;
        Invoke("SwitchVelocityDirection", swingTimer);

    }


}
