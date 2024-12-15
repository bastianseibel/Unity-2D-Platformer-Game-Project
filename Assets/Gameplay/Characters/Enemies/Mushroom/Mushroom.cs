using UnityEngine;

public class Mushroom : MonoBehaviour
{
    // * Enemy movement and health settings
    public float moveSpeed = 2f;
    public float moveDistance = 3f;
    public int health = 50;

    // * Track movement state
    private Vector3 startPosition;
    private bool movingRight = true;

    // * Store initial position
    void Start()
    {
        startPosition = transform.position;
    }

    // * Update movement every frame
    void Update()
    {
        Move();
    }

    // * Handle back and forth movement
    void Move()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            if (transform.position.x >= startPosition.x + moveDistance)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
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
            Die();
        }
    }

    // * Destroy enemy when health reaches zero
    private void Die()
    {
        Destroy(gameObject);
    }
}