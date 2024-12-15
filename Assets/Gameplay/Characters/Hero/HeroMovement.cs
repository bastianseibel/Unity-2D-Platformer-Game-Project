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
    public bool isOnLadder = false; // Erlaubt klettern
    private float moveDirectionX = 0f;
    private float moveDirectionY = 0f;

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
        float moveSpeedX = moveDirectionX * speed;
        float moveSpeedY = moveDirectionY * speed;

        // Bewegung auf der X- und Y-Achse
        rb.velocity = new Vector2(moveSpeedX, isOnLadder ? moveSpeedY : rb.velocity.y);
    }

    // * Checks all the time if the hero is grounded and processes input
    void Update()
    {
        CheckGroundedStatus();
        HandleInput(); // Eingaben für Bewegung
    }

    // * Handle keyboard input for movement, jumping and vertical movement
    private void HandleInput()
    {
        // Bewegung links und rechts
        float horizontalInput = Input.GetAxis("Horizontal"); // Pfeiltasten oder A/D
        Move(horizontalInput);

        // Hoch und runter mit den Pfeiltasten
        if (isOnLadder) // Nur klettern, wenn auf einer Leiter
        {
            moveDirectionY = Input.GetAxis("Vertical"); // Pfeiltasten oben/unten
        }
        else
        {
            moveDirectionY = 0f; // Verhindert unbeabsichtigtes Klettern
        }

        // Springen mit der "Jump"-Taste (Space oder festgelegte Taste)
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    // * Handle movement and flip the hero sprite
    public void Move(float direction)
    {
        moveDirectionX = direction;
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

    // * Check if the hero is grounded and update the animations
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
