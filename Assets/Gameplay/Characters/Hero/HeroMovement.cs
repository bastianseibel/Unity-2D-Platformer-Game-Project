using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 8f;
    public float acceleration = 50f;
    public float deceleration = 50f;
    public float jumpForce = 14f;
    public float airControl = 25f;
    public float maxFallSpeed = 15f;
    public int maxJumps = 2;

    [Header("Physik")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;

    private int jumpCount;
    private bool facingRight = true;
    private float moveDirectionX;
    private bool isGrounded;
    private bool wasInAir = true;
    public bool isOnLadder = false;

    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ResetAnimations();
    }

    void Update()
    {
        CheckGroundedStatus();
        HandleInput();
    }

   // * Handles all physics-based movement calculations at a fixed time interval
    void FixedUpdate()
    {
    // * Calculate target horizontal speed based on input direction and speed setting
    float targetSpeed = moveDirectionX * speed;
    
    float currentAcceleration = isGrounded ? acceleration : airControl;
    
    float newSpeed = Mathf.MoveTowards(
        rb.velocity.x,
        targetSpeed,
        (moveDirectionX != 0 ? currentAcceleration : deceleration) * Time.fixedDeltaTime
    );

    // * Handle vertical movement with enhanced jump physics
    float verticalVelocity = rb.velocity.y;
    
    // * Apply additional gravity when falling
    if (verticalVelocity < 0)
    {
        // * Increase falling speed using fallMultiplier
        verticalVelocity += Physics2D.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        // * Clamp falling speed to maxFallSpeed
        verticalVelocity = Mathf.Max(verticalVelocity, -maxFallSpeed);
    }
    // * Apply lower gravity when rising and jump button released (for variable jump heights)
    else if (verticalVelocity > 0 && !Input.GetButton("Jump"))
    {
        verticalVelocity += Physics2D.gravity.y * lowJumpMultiplier * Time.fixedDeltaTime;
    }

    // * Apply the calculated velocities to the Rigidbody2D
    rb.velocity = new Vector2(newSpeed, verticalVelocity);

    PreventWallSticking();
}

    // * Handles ladder climbing
    private void HandleInput()
    {
        Move(Input.GetAxis("Horizontal"));

    if (isOnLadder)
    {
        float verticalInput = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(rb.velocity.x, verticalInput * 5);
        rb.gravityScale = 0;
    }
    else
    {
        rb.gravityScale = 1;
    }

    if (Input.GetButtonDown("Jump"))
    {
        Jump();
        }
    }

    // * Processes horizontal movement and character flipping
    public void Move(float direction)
    {
        moveDirectionX = direction;
        
        if (isGrounded)
        {
            anim.SetBool("IsWalking", Mathf.Abs(direction) > 0.1f);
        }

        if ((direction > 0 && !facingRight) || (direction < 0 && facingRight))
        {
            Flip();
        }
    }

    // * Handles jump mechanics including double jump
    public void Jump()
    {
        if (jumpCount < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
            anim.SetBool("IsJumping", true);
            anim.SetBool("IsFalling", false);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // * Checks if player is touching ground and updates status
    private void CheckGroundedStatus()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && wasInAir)
        {
            wasInAir = false;
            ResetAnimations();
            jumpCount = 0;
        }
        else if (!isGrounded)
        {
            wasInAir = true;
        }

        UpdateAnimations();
    }

    // * Updates all animation states based on player movement
    private void UpdateAnimations()
    {
        if (isGrounded)
        {
            anim.SetBool("IsWalking", Mathf.Abs(rb.velocity.x) > 0.2f);
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsFalling", false);
        }
        else
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsJumping", rb.velocity.y > 1f);
            anim.SetBool("IsFalling", rb.velocity.y < -1f);
        }
    }

    // * Resets all animation bools to default state
    private void ResetAnimations()
    {
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsJumping", false);
        anim.SetBool("IsFalling", false);
    }

    // * Prevents player from sticking to walls when moving against them
    private void PreventWallSticking()
    {
        float distance = 0.1f;
        if ((Physics2D.Raycast(transform.position, Vector2.left, distance, groundLayer) && moveDirectionX < 0) || 
            (Physics2D.Raycast(transform.position, Vector2.right, distance, groundLayer) && moveDirectionX > 0))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    // * Draws debug visualization for ground check in Scene view
    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}