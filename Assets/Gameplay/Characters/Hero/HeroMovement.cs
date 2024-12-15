using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public float speed = 5.0F;
    public float jumpForce = 5.0F;
    public int maxJumps = 2;
    private int jumpCount = 0;
    private bool facingRight = true;
    public bool isOnLadder = false;
    private float moveDirection = 0f;
    
    private Rigidbody2D rb;
    private Animator anim;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        anim.SetBool("IsWalking", false);
        anim.SetBool("IsJumping", false);
        anim.SetBool("IsFalling", false);
    }

    void FixedUpdate()
    {
        float moveSpeed = moveDirection * speed;
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    void Update()
    {
        CheckGroundedStatus();
    }

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

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void ResetJumpCount()
    {
        jumpCount = 0;
    }

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