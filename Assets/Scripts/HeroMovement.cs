using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    
    public float speed = 5.0F;
    public float jumpForce = 5.0F;
    public int maxJumps = 2;
    private int jumpCount = 0;
    
    
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.5f;

    private bool isGrounded;
    private bool facingRight = true;
    
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
        HandleMovement();
        
        HandleJump();

        CheckGroundedStatus();
    }

    private void HandleMovement()
    {
        float direction = Input.GetAxis("Horizontal");

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

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
        }
    }

    private void CheckGroundedStatus()
    {
        // Perform a raycast to check if the character is grounded
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);


        if (isGrounded)
        {
            jumpCount = 0;
            anim.SetBool("IsJumping", false);
        }
        else
        {
            anim.SetBool("IsJumping", true);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
    }
}
