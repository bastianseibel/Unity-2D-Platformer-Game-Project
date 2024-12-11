using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.5f;

    private Animator anim;
    private HeroMovement heroMovement;
    private bool isGrounded;

    void Start()
    {
        anim = GetComponent<Animator>();
        heroMovement = GetComponent<HeroMovement>();
    }

    void Update()
    {
        CheckGroundedStatus();
    }

    private void CheckGroundedStatus()
    {
        float verticalVelocity = GetComponent<Rigidbody2D>().velocity.y;
        isGrounded = Physics2D.CircleCast(groundCheck.position, 0.1f, Vector2.down, groundCheckDistance, groundLayer);

        if (isGrounded)
        {
            heroMovement.ResetJumpCount();
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

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        }
    }
}