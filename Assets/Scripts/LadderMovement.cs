using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    // * References to player components
    private HeroMovementController movementController;
    private Rigidbody2D playerRb;
    
    // * Track ladder movement state
    private bool isMovingUp = false;
    private bool isMovingDown = false;
    private bool isOnLadder = false;

    [SerializeField] private float climbSpeed = 8f;

    // * Get player components at start
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            movementController = player.GetComponent<HeroMovementController>();
            playerRb = player.GetComponent<Rigidbody2D>();
        }
    }

    // * Enable ladder movement when player enters
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isOnLadder = true;
            playerRb.gravityScale = 0;
        }
    }

    // * Disable ladder movement when player exits
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isOnLadder = false;
            playerRb.gravityScale = 1;
            isMovingUp = false;
            isMovingDown = false;
        }
    }

    // * Check for ladder movement every frame
    private void Update()
    {
        if (isOnLadder)
        {
            HandleLadderMovement();
        }
    }

    // * Process vertical movement on ladder
    private void HandleLadderMovement()
    {
        float verticalMovement = 0;
        
        if (isMovingUp) verticalMovement = 1;
        if (isMovingDown) verticalMovement = -1;

        playerRb.velocity = new Vector2(playerRb.velocity.x, verticalMovement * climbSpeed);
    }

    // * Button input handlers for ladder movement
    public void OnUpButtonDown()
    {
        if (isOnLadder)
        {
            isMovingUp = true;
        }
    }

    public void OnUpButtonUp()
    {
        isMovingUp = false;
    }

    public void OnDownButtonDown()
    {
        if (isOnLadder)
        {
            isMovingDown = true;
        }
    }

    public void OnDownButtonUp()
    {
        isMovingDown = false;
    }
}