using UnityEngine;
using System.Collections;

public class HeroHealth : MonoBehaviour
{
    // * Health and immunity settings
    [Header("Health Settings")]
    public int maxHealth = 2;
    public float immunityDuration = 1.5f;
    
    // * Component references
    [Header("References")]
    private SpriteRenderer spriteRenderer;
    private HeroMovement heroMovement;
    private Animator animator;
    private HeartManager heartManager;

    // * Health state tracking
    public int currentHealth;
    public bool isImmune = false;
    private Vector3 spawnPoint; 

    // * Initialize components and health
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        heroMovement = GetComponent<HeroMovement>();
        heartManager = FindObjectOfType<HeartManager>();
        animator = GetComponent<Animator>();
        spawnPoint = transform.position;
    }

    // * Handle damage taking and immunity
    public void TakeDamage(int damage)
    {
        if (!isImmune)
        {
            currentHealth -= damage;

            if (heartManager != null)
            {
                heartManager.UpdateHearts();
            }

            if (currentHealth == 1)
            {
                animator.SetTrigger("Damage");
            }
            else if (currentHealth <= 0)
            {
                animator.SetTrigger("Die");
                Die();
            }
            else
            {
                StartCoroutine(ImmunityEffect());
            }
        }
    }

    // * Temporary immunity after taking damage
    private IEnumerator ImmunityEffect()
    {
        isImmune = true;
        yield return new WaitForSeconds(immunityDuration);
        isImmune = false;
    }

    // * Reset health and position on death
    public void Die()
    {
        currentHealth = maxHealth;
        transform.position = spawnPoint;
    }

    // * Restore health
    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        if (heartManager != null)
        {
            heartManager.UpdateHearts();
        }
    }

    // * Set new spawn point
    public void SetSpawnPoint(Vector3 position)
    {
        spawnPoint = position;
    }

    // * Update spawn point when reaching checkpoint
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            SetSpawnPoint(transform.position);
        }
    }
}