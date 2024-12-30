using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 50;
    private int currentHealth;
    private bool isDying = false;

    private EnemyMovementController movementController;
    private EnemyAnimationController animationController;
    private Collider2D enemyCollider;

    private void Start()
    {
        currentHealth = maxHealth;
        movementController = GetComponent<EnemyMovementController>();
        animationController = GetComponent<EnemyAnimationController>();
        enemyCollider = GetComponent<Collider2D>();
    }

    public void TakeDamage(int damage)
    {
        if (isDying) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDying = true;
        
        if (movementController != null)
            movementController.StopMovement();
        
        if (animationController != null)
            animationController.PlayDeathAnimation();
        
        if (enemyCollider != null)
            enemyCollider.enabled = false;

        Destroy(gameObject, 0.4f);
    }
}