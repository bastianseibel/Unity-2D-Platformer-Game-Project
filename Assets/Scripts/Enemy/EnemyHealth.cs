using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour, IPooledObject
{
    [Header("Health Settings")]
    public int maxHealth = 50;
    private int currentHealth;
    private bool isDying = false;

    [Header("Audio")]
    [SerializeField] private AudioSource deathSound;

    private EnemyMovementController movementController;
    private EnemyAnimationController animationController;
    private Collider2D enemyCollider;

    public void OnObjectSpawn()
    {
        currentHealth = maxHealth;
        isDying = false;
        if (enemyCollider != null) enemyCollider.enabled = true;
        if (movementController != null) movementController.ResetEnemy();
        gameObject.SetActive(true);
    }

    private void Start()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
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
        deathSound.Play();
        isDying = true;
        
        if (movementController != null)
            movementController.StopMovement();
        
        if (animationController != null)
            animationController.PlayDeathAnimation();
        
        if (enemyCollider != null)
            enemyCollider.enabled = false;

        if (CoinSpawner.Instance != null)
        {
            CoinSpawner.Instance.SpawnRandomCoin(transform.position);
        }

        StartCoroutine(DeactivateAfterDelay(0.4f));
    }

    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}