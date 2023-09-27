using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public float MAX_SPEED = 7; //max speed player can move in X direction
    public float X_ACCEL = 90;
    public float AIRBORNE_MOVE_MOD = 0.3f; //modifier for when player is not groudned
    public float MAX_Y_SPEED = 10;
    public float JUMP_FORCE = 1750;
    public float GRAVITY = 4;
    public float GRAVITY_FALL_MOD = 1.75f; //modifier for when the player is falling
    public float FASTFALL_MOD = 1.75f; //modifier for fastfall
    public float COYOTE_TIME = 0.2f; //time to keep the player grounded after they fall off of a ledge
    public float JUMP_BUFFER = 0.2f; //time to buffer the jump input

    public int groundLayer = 8; //ground layer

    private Vector2 movementMod = Vector2.one; //misc modifier

    //inputs
    private float input_x;
    private bool input_jump;
    private bool input_down;

    //flags
    [SerializeField]
    private bool is_falling, is_grounded, can_jump, coyote = false, last_grounded = false;

    
    //modules
    public Rigidbody2D rb;
    public Transform groundCheckTL, groundCheckBR;

    //instance variables
    private Vector2 velocity = Vector2.zero;
    private float acceleration = 0;

    //debug
    private int counter = 0;

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
        input_down = Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

        //buffer the jump for JUMP_BUFFER seconds
        if (Input.GetKeyDown(KeyCode.Space))
        {
            input_jump = true;
            Invoke("ResetJumpPress", JUMP_BUFFER);
        }
    }

    void Move()
    {
        //get accel from input * scalar  * misc modifier
        acceleration = input_x * X_ACCEL * movementMod.x;

        //clamp x speed
        if(Mathf.Abs(rb.velocity.x) >= MAX_SPEED)
        {
            rb.velocity = new Vector2(rb.velocity.normalized.x * MAX_SPEED, rb.velocity.y);
        }

        //add force to player rigidbody
        rb.AddForce(acceleration * Vector2.right);
    }

    void Jump()
    {
        //add force for jump
        rb.AddForce(Vector2.up * JUMP_FORCE);

        //to disable accidental multijump
        can_jump = false;

        //so that coyote doesnt trigger on a jump
        coyote = false;
        


    }

    float Gravity()
    {
        float g = GRAVITY; //flat grav
        g *= is_falling ? GRAVITY_FALL_MOD : 1; //fall faster on the descend (it feels better)
        g *= movementMod.y; //for fastfall
        return g;
    }

    void ApplyGravity()
    {
        //apply gravity from Gravity()
        rb.AddForce(Vector2.down * Gravity());

        //clamp
        if (rb.velocity.y > MAX_Y_SPEED)
        {
            rb.velocity = new Vector2(rb.velocity.x, MAX_Y_SPEED);
        }
    }


    void CheckFlags()
    {
        is_falling = rb.velocity.y < -0.01f;
        last_grounded = is_grounded; //for coyote


        if (coyote)
        {
            is_grounded = true; //if coyote is active, then the player is still on the ground
        } else
        {
            is_grounded = isGrounded(); //if not, then just calc it normally
        }

        //if the player was grounded, but isnt now, but also isnt jumping, active coyote
        if (last_grounded && !is_grounded && !input_jump)
        {
            coyote = true;
            Invoke("ResetGrounded", COYOTE_TIME);
        }

        //if grounded normal, if not then mod player movement
        if (is_grounded)
        {
           movementMod.x = 1;
        } else
        {
            movementMod.x = AIRBORNE_MOVE_MOD;
        }

        //fast fall
        if(input_down)
        {
            movementMod.y = FASTFALL_MOD;
        } else
        {
            movementMod.y = 1;
        }
    }

    bool isGrounded()
    {
        bool x = false;

        //creates a little box under player, if that box collides with "Ground", we grounded
        Collider2D collider = Physics2D.OverlapArea(groundCheckTL.position, groundCheckBR.position);
        if (collider != null && collider.gameObject.layer.Equals(groundLayer))
        {
            
            x = true;
        }

        return x;
    }

    //for jump buffer
    void ResetJumpPress()
    {
        input_jump = false;
        can_jump = true;
    }

    //for coyote
    void ResetGrounded()
    {
        coyote = false;
    }

    //for spotlight
    public float GetSpeed()
    {
        return rb.velocity.magnitude;
    }

    //debug
    private void PrintCounter()
    {
        Debug.Log("counter: " + counter);
    }


    
}
