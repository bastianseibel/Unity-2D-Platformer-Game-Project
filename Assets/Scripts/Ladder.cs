using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private HeroMovement heroMovement;

    private void Start()
    {
        heroMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroMovement>();
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
        float verticalInput = Input.GetAxis("Vertical");
        heroMovement.GetComponent<Rigidbody2D>().velocity = new Vector2(heroMovement.GetComponent<Rigidbody2D>().velocity.x, verticalInput * heroMovement.speed);
    }
}