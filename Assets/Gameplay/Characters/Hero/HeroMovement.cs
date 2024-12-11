using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public float speed = 5.0F;
    public float jumpForce = 5.0F;
    public int maxJumps = 2;
    private int jumpCount = 0;
    private bool facingRight = true;
    public bool isOnLadder = false;
    
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsJumping", false);
        anim.SetBool("IsFalling", false);
    }

    void Update()
    {
        CheckGroundedStatus();
    }

    public void Move(float direction)
    {
        anim.SetBool("IsWalking", direction != 0);

        if (direction > 0 && !facingRight)
        {
            Flip();
        }
        else if (direction < 0 && facingRight)
        {
            Flip();
        }

        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    public void Jump()
    {
        if (jumpCount < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
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
        bool isGrounded = Physics2D.CircleCast(transform.position, 0.1f, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));

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
}