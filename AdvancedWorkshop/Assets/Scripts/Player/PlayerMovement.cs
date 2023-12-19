using UnityEngine;
using UnityEngine.InputSystem;

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

    public float platformModifier = 5f;

    //public AudioClip jump;
    //private AudioSource audioSource;

    public int groundLayer = 8; //ground layer

    private Vector2 movementMod = Vector2.one; //misc modifier


    //inputs
    private float input_x;
    [SerializeField]
    private bool input_jump;
    private bool input_down;

    //flags
    [SerializeField]
    private bool is_falling, is_grounded = false, can_jump, coyote = false, last_grounded = false;


    //modules
    public PlayerInput input;
    public InputAction moveAction, jumpAction, downAction;
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
        input.SwitchCurrentActionMap("Player");

        InitInputs();
    }

    // Start is called before the first frame update
    void Start()
    {
      //  audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        CheckFlags();

        if(input_jump && is_grounded && can_jump)
        {
            Jump();
           // audioSource.PlayOneShot(jump);
        }

        ApplyGravity();
    }

    void FixedUpdate()
    {
        Move();

    }

    void InitInputs()
    {
        jumpAction.performed += context => OnJumpInput(context);
        downAction.started += context => OnDownInput(context);
        downAction.canceled += context => OnDownInput(context);
    }

    void GetInput_old()
    {
        /*
        input_x = Input.GetAxis("Horizontal");
        input_down = Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

        //buffer the jump for JUMP_BUFFER seconds
        if (Input.GetKeyDown(KeyCode.Space))
        {
            input_jump = true;
            Invoke("ResetJumpPress", JUMP_BUFFER);
        }
        */
    }

    void GetInput()
    {
        // input_x = input.actions.FindAction("Move", true).ReadValue<float>();
        // input_x = moveAction.ReadValue<float>();

        input_x = moveAction.ReadValue<Vector2>().x;

    }

    void OnJumpInput(InputAction.CallbackContext context)
    {
        input_jump = true;
        Invoke("ResetJumpPress", JUMP_BUFFER);
    }

    void OnDownInput(InputAction.CallbackContext context)
    {
        input_down = context.started;
    }

    public void HandleMovement(InputAction.CallbackContext context)
    {
       // input_x = context.ReadValue<float>();
        Debug.Log("movement is being handled, dw");
    }

    void Move()
    {
        

        if(transform.parent == null)
        {
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        } else
        {
            rb.interpolation = RigidbodyInterpolation2D.None;
        }
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
        PlayerAnimation.instance.Stretch();
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
        g *= Time.deltaTime;
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

    void Landed()
    {
        Debug.Log("landed");

        PlayerAnimation.instance.Squash();
        PlayerCam.instance.StartShake(0.025f);

        Invoke("ResetSquashStretch", 0.05f);
    }

    void Fell()
    {
        Debug.Log("fell");
 
        PlayerAnimation.instance.Stretch();
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

            //if the player wasnt grounded, but is now grounded, they just landed
            if(!is_grounded)
            {
                Landed();
            }

            if(coyote)
            {
                Debug.Log("we coyote but we landed");
                Landed();
            }

            
        } else
        {
            //if the player was grounded, didnt jump, but is now airborne, they fell
            if(is_grounded)
            {
                Fell();
            }
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
        is_grounded = false;
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

    void ResetSquashStretch()
    {
        PlayerAnimation.instance.ResetSS();
    }

    public void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        downAction.Enable();
    }
    
}
