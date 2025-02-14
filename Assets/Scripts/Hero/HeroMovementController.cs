using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HeroMovementController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float airControl = 25f;
    [SerializeField] private float deceleration = 50f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float maxFallSpeed = 15f;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float speed = 8f;

    [Header("Audio")]
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource ladderSound;

    [Header("Jump Physics")]
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;

    private bool facingRight = true;
    private HeroGroundCheck groundCheck;
    private bool isJumpButtonHeld;
    private bool isOnLadder;
    private int jumpCount;
    private float moveDirectionX;
    private float originalGravityScale;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<HeroGroundCheck>();
        originalGravityScale = rb.gravityScale;
    }

    private void OnEnable()
    {
        UIEvents.OnControlButtonStateChanged += HandleControlInput;
        groundCheck.OnGroundedStateChanged += HandleGroundedStateChanged;
    }

    private void OnDisable()
    {
        UIEvents.OnControlButtonStateChanged -= HandleControlInput;
        groundCheck.OnGroundedStateChanged -= HandleGroundedStateChanged;
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        ApplyJumpPhysics();
        PreventWallSticking();
    }

    private void ApplyJumpPhysics()
    {
        if (isOnLadder) return;

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.fixedDeltaTime;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
        }
        else if (rb.velocity.y > 0 && !isJumpButtonHeld)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.fixedDeltaTime;
        }
    }

    private void ApplyMovement()
    {
        float targetSpeed = moveDirectionX * speed;
        float currentAcceleration = groundCheck.IsGrounded ? acceleration : airControl;
        
        float newSpeed = Mathf.MoveTowards(
            rb.velocity.x,
            targetSpeed,
            (moveDirectionX != 0 ? currentAcceleration : deceleration) * Time.fixedDeltaTime
        );

        rb.velocity = new Vector2(newSpeed, rb.velocity.y);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void HandleControlInput(string buttonType, bool isPressed)
    {
        switch(buttonType.ToLower())
        {
            case "left":
                HandleMovement(isPressed ? -1 : 0);
                break;
            case "right":
                HandleMovement(isPressed ? 1 : 0);
                break;
            case "jump":
                if (isPressed)
                    HandleJump();
                SetJumpButtonState(isPressed);
                break;
        }
    }

    private void HandleGroundedStateChanged(bool isGrounded)
    {
        if (isGrounded)
        {
            jumpCount = 0;
        }
    }

    private void HandleJump()
    {
        if (isOnLadder) return;
        
        if (jumpCount < maxJumps)
        {
            jumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
    }

    private void HandleMovement(float direction)
    {
        moveDirectionX = direction;
        if ((direction > 0 && !facingRight) || (direction < 0 && facingRight))
        {
            Flip();
        }
    }

    private void PreventWallSticking()
    {
        float distance = 0.1f;
        if ((Physics2D.Raycast(transform.position, Vector2.left, distance, groundCheck.GroundLayer) && moveDirectionX < 0) || 
            (Physics2D.Raycast(transform.position, Vector2.right, distance, groundCheck.GroundLayer) && moveDirectionX > 0))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    public void ResetMovementState()
    {
        moveDirectionX = 0;
        rb.velocity = Vector2.zero;
        isJumpButtonHeld = false;
    }

    public void SetJumpButtonState(bool isHeld)
    {
        isJumpButtonHeld = isHeld;
    }

    public void SetLadderState(bool state)
    {
        isOnLadder = state;
        rb.gravityScale = state ? 0 : originalGravityScale;
        
        if (state)
        {
            ladderSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        else
        {
            jumpCount = groundCheck.IsGrounded ? 0 : 1;
        }
    }
}