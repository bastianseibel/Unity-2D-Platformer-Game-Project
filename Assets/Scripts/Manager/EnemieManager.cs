using UnityEngine;

public class EnemieManager : MonoBehaviour
{
    // * Enemy movement and health settings
    public float moveSpeed = 2f;
    public float moveDistance = 3f;
    public int health = 50;
    public bool facingRightAtStart = true;

    // * Track movement state
    private Vector3 startPosition;
    private bool movingRight = true;
    private Animator animator;
    private bool isDying = false;

    // * Store initial position
    void Start()
    {
        startPosition = transform.position;
        movingRight = facingRightAtStart;
        animator = GetComponent<Animator>();

        if (!facingRightAtStart != (transform.localScale.x > 0))
        {
            Flip();
        }
    }

    // * Update movement every frame
    void Update()
    {
        if (!isDying)
        {
            Move();
        }
    }

    // * Handle back and forth movement
    void Move()
    {
        Vector2 movement;
        if (movingRight)
        {
            // Bewegung nach rechts
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

    // * Flip enemy sprite direction
    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // * Damage player on contact
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HeroHealth heroHealth = collision.GetComponent<HeroHealth>();
            if (heroHealth != null)
            {
                heroHealth.TakeDamage(1);
            }
        }
    }

    // * Handle enemy taking damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            EnemyDeath();
        }
    }

    // * Play death animation and destroy enemy
    private void EnemyDeath()
    {
        if (!isDying)
        {
            isDying = true;
            moveSpeed = 0;

            if (SaveLoadManager.Instance != null)
            {
                UniqueID enemyID = GetComponent<UniqueID>();
                if (enemyID != null)
                {
                    SaveLoadManager.Instance.RegisterDefeatedEnemy(enemyID.uniqueID);
                }
            }

            if (animator != null)
            {
                animator.SetTrigger("Death");

                if (GetComponent<Collider2D>() != null)
                {
                    GetComponent<Collider2D>().enabled = false;
                }

                Destroy(gameObject, 0.4f);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}