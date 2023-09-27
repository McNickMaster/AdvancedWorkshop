using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float MAX_SPEED = 7;
    public float X_ACCEL = 90;
    public float MAX_Y_SPEED = 10;
    public float JUMP_FORCE = 1750;
    public float GRAVITY = 4;
    public float GRAVITY_FALL_MOD = 1.75f;
    public float AIRBORNE_MOVE_MOD = 0.3f;
    public float FASTFALL_MOD = 1.75f;
    public float JUMP_BUFFER = 0.2f;

    private Vector2 movementMod = Vector2.one;
    private float input_x;
    [SerializeField]
    private bool input_jump;
    private bool input_down;

    [SerializeField]
    private bool is_falling, is_grounded, can_jump;

    public int groundLayer = 8;

    public Rigidbody2D rb;
    public Transform groundCheckTL, groundCheckBR;


    private Vector2 velocity = Vector2.zero;
    private float acceleration = 0;

    private int counter = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        CheckFlags();

        if(input_jump && is_grounded && can_jump)
        {
            Jump();
        }

        ApplyGravity();


        
    }

    void FixedUpdate()
    {
        Move();

    }

    void GetInput()
    {
        input_x = Input.GetAxis("Horizontal");
        input_down = Input.GetKey(KeyCode.S);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            input_jump = true;
            Invoke("ResetJumpPress", JUMP_BUFFER);
        }
    }

    void Move()
    {
       acceleration = input_x * X_ACCEL * movementMod.x;

        if(Mathf.Abs(rb.velocity.x) >= MAX_SPEED)
        {
            rb.velocity = new Vector2(rb.velocity.normalized.x * MAX_SPEED, rb.velocity.y);
        }

        //rb.velocity = new Vector2(velocity.x, rb.velocity.y);
        rb.AddForce(acceleration * Vector2.right);
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * JUMP_FORCE);
        can_jump = false;

        counter++;
        PrintCounter();


    }

    float Gravity()
    {
        float g = GRAVITY;
        g *= is_falling ? GRAVITY_FALL_MOD : 1;
        return g;
    }

    void ApplyGravity()
    {
        rb.AddForce(Vector2.down * Gravity());

        if (rb.velocity.y > MAX_Y_SPEED)
        {
            rb.velocity = new Vector2(rb.velocity.x, MAX_Y_SPEED);
        }
    }


    void CheckFlags()
    {
        is_falling = rb.velocity.y < -0.01f;
        is_grounded = isGrounded();


        if(is_grounded)
        {
           movementMod.x = 1;
        } else
        {
            movementMod.x = AIRBORNE_MOVE_MOD;
        }
    }

    bool isGrounded()
    {
        bool x = false;

        Collider2D collider = Physics2D.OverlapArea(groundCheckTL.position, groundCheckBR.position);
        if (collider != null && collider.gameObject.layer.Equals(groundLayer))
        {
            
            x = true;
        }

        return x;
    }

    void ResetJumpPress()
    {
        input_jump = false;
        can_jump = true;
    }



    private void PrintCounter()
    {
        Debug.Log("counter: " + counter);
    }


    
}
