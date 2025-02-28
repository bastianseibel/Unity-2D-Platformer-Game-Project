using UnityEngine;
public class EnemyMovementController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float moveDistance = 3f;
    public bool facingRightAtStart = true;

    private Vector3 startPosition;
    private bool movingRight;
    private bool canMove = true;

    private void Start()
    {
        InitializeMovement();
    }

    public void ResetEnemy()
    {
        canMove = true;
        InitializeMovement();
    }

    private void InitializeMovement()
    {
        startPosition = transform.position;
        movingRight = facingRightAtStart;

        if (!facingRightAtStart != (transform.localScale.x > 0))
        {
            Flip();
        }
    }

    private void Update()
    {
        if (canMove)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector2 movement;
        if (movingRight)
        {
            movement = transform.right * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)movement;

            if (transform.position.x >= startPosition.x + moveDistance)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            movement = -transform.right * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)movement;

            if (transform.position.x <= startPosition.x - moveDistance)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void StopMovement()
    {
        canMove = false;
    }
}