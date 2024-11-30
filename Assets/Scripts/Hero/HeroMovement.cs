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

    private CoinManager coinManager;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coinManager = FindObjectOfType<CoinManager>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        
        HandleMovement();
        
        HandleJump();

        CheckGroundedStatus();

        UpdateCamera();
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
    float verticalVelocity = rb.velocity.y;

    isGrounded = Physics2D.CircleCast(groundCheck.position, 0.1f, Vector2.down, groundCheckDistance, groundLayer);

    if (isGrounded)
    {
        jumpCount = 0;
        anim.SetBool("IsJumping", false);
        anim.SetBool("IsFalling", false);
    }
    else
    {
        if (verticalVelocity< -0.1f)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            coinManager.addCoin();
            Destroy(collision.gameObject);
        }
    }
    private void UpdateCamera()
    {
        if (mainCamera != null)
        {
            Vector3 newPosition = transform.position;
            newPosition.z = mainCamera.transform.position.z; // Behalte die ursprüngliche Z-Position der Kamera
            mainCamera.transform.position = newPosition;
        }
    }

}
