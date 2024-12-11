using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private HeroMovement heroMovement;
    private Rigidbody2D playerRb;
    private bool isMovingUp = false;
    private bool isMovingDown = false;

    private void Start()
    {
        heroMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroMovement>();
        playerRb = heroMovement.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            heroMovement.isOnLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            heroMovement.isOnLadder = false;
            isMovingUp = false;
            isMovingDown = false;
        }
    }

    private void Update()
    {
        if (heroMovement.isOnLadder)
        {
            HandleLadderMovement();
        }
    }

    private void HandleLadderMovement()
    {
        float verticalMovement = 0;
        
        if (isMovingUp) verticalMovement = 1;
        if (isMovingDown) verticalMovement = -1;

        playerRb.velocity = new Vector2(playerRb.velocity.x, verticalMovement * heroMovement.speed);
    }

    public void OnUpButtonDown()
    {
        if (heroMovement.isOnLadder)
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
        if (heroMovement.isOnLadder)
        {
            isMovingDown = true;
        }
    }

    public void OnDownButtonUp()
    {
        isMovingDown = false;
    }
}