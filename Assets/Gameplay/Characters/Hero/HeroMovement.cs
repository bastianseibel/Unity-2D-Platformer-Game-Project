using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    // * Movement and jump settings
    public float speed = 5.0F;
    public float jumpForce = 5.0F;
    public int maxJumps = 2;

    // * Character variables
    private int jumpCount = 0;
    private bool facingRight = true;
    public bool isOnLadder = false;
    private float moveDirection = 0f;
    
    private Rigidbody2D rb;
    private Animator anim;

    // * Ground Check settings
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.5f;


    // * Start the game and set the animations to the default state
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        anim.SetBool("IsWalking", false);
        anim.SetBool("IsJumping", false);
        anim.SetBool("IsFalling", false);
    }

    // * Fixed Update is called once per physics frame, move the hero
    void FixedUpdate()
    {
        float moveSpeed = moveDirection * speed;
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    // * Checks all the time if the hero is grounded
    void Update()
    {
        CheckGroundedStatus();
    }

    // * Handle movement and flip the hero sprite
    public void Move(float direction)
    {
        moveDirection = direction;
        anim.SetBool("IsWalking", direction != 0);

        if (direction > 0 && !facingRight)
        {
            Flip();
        }
        else if (direction < 0 && facingRight)
        {
            Flip();
        }
    }

    // * Handle jumping
    public void Jump()
    {
        if (jumpCount < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
            anim.SetBool("IsJumping", true);
            anim.SetBool("IsFalling", false);
        }
    }

    // * Flip the hero sprite
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // * Reset the jump count
    public void ResetJumpCount()
    {
        jumpCount = 0;
    }

    // * Check if the hero is grounded and update the aniamtions if so
    private void CheckGroundedStatus()
    {
        float verticalVelocity = rb.velocity.y;
        bool isGrounded = Physics2D.CircleCast(groundCheck.position, 0.1f, Vector2.down, groundCheckDistance, groundLayer);

        if (isGrounded)
        {
            ResetJumpCount();
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsFalling", false);
        }
        else
        {
            if (verticalVelocity < -0.1f)
            {
                anim.SetBool("IsFalling", true);
                anim.SetBool("IsJumping", false);
                anim.SetBool("IsWalking", false);
            }
            else if (verticalVelocity > 0.1f)
            {
                anim.SetBool("IsJumping", true);
                anim.SetBool("IsFalling", false);
                anim.SetBool("IsWalking", false);
            }
        }
    }

    // * Ground Check line draw in editor
    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        }
    }
}