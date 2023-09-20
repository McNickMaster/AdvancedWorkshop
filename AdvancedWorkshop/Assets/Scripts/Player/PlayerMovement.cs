using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public const float MAX_X_SPEED = 5;
    public const float X_ACCEL = 4;
    public const float JUMP_FORCE = 100;
    public const float GRAVITY = -10;
    public const float GRAVITY_FALL_MOD = 1.75f;
    public const float FASTFALL_MOD = 1.75f;

    private Vector2 movementMod = Vector2.zero;
    private float input_x;
    private bool input_jump;
    private bool input_down;

    public int groundLayer = 8;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetInput()
    {
        input_x = Input.GetAxisRaw("Horizontal");
        input_jump = Input.GetKeyDown(KeyCode.Space);
        input_down = Input.GetKey(KeyCode.S);

    }

    void Walk()
    {

    }
}
