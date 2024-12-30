using UnityEngine;

public class LadderController
{
    private Rigidbody2D playerRb;
    private HeroMovementController movementController;

    public void Initialize()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody2D>();
            movementController = player.GetComponent<HeroMovementController>();
        }
    }

    public void StartClimbing()
    {
        if (playerRb != null)
        {
            playerRb.gravityScale = 0;
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
            movementController?.SetLadderState(true);
        }
    }

    public void StopClimbing()
    {
        if (playerRb != null)
        {
            playerRb.gravityScale = 1;
            movementController?.SetLadderState(false);
        }
    }

    public void UpdateMovement(float direction, float speed)
    {
        if (playerRb != null)
        {
            float horizontalVelocity = playerRb.velocity.x;
            float verticalVelocity = direction * speed;
            Vector2 newVelocity = new Vector2(horizontalVelocity, verticalVelocity);
            playerRb.velocity = newVelocity;
        }
    }
}