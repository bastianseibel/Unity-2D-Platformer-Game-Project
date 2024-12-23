using UnityEngine;
using System.Collections;

// * Manages hero health, damage, death and respawn mechanics
public class HeroHealth : MonoBehaviour
{
    // * Core gameplay settings
    [Header("Settings")]
    public int maxHealth = 2;
    public float immunityDuration = 1.5f;
    public float deathDuration = 1.5f;

    // * Required component references
    [Header("References")]
    private Animator animator;
    private HeroMovement heroMovement;
    private HeartManager heartManager;
    public Vector3 spawnPoint;

    // * Current state tracking
    [Header("State")]
    public int currentHealth;
    private bool isImmune = false;

    // * Initialize components and health on start
    private void Start()
    {
        InitializeComponents();
        ResetHealth();
    }

    // * Get all required components
    private void InitializeComponents()
    {
        animator = GetComponent<Animator>();
        heroMovement = GetComponent<HeroMovement>();
        heartManager = FindObjectOfType<HeartManager>();
        spawnPoint = transform.position;
    }

    // * Reset health to max and update UI
    private void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHeartDisplay();
    }

    // * Handle damage taking and check for death
    public void TakeDamage(int damage)
    {
        if (isImmune) return;

        currentHealth -= damage;
        UpdateHeartDisplay();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            HandleDamageEffect();
        }
    }

    // * Play damage animation and start immunity
    private void HandleDamageEffect()
    {
        animator.SetTrigger("Damage");
        StartCoroutine(ImmunityEffect());
    }

    // * Temporary immunity after taking damage
    private IEnumerator ImmunityEffect()
    {
        isImmune = true;
        yield return new WaitForSeconds(immunityDuration);
        isImmune = false;
    }

    // * Handle death sequence
    public void Die()
    {
        DisableMovement();
        animator.SetTrigger("Die");
        StartCoroutine(RespawnAfterDelay());
    }

    // * Disable movement during death
    private void DisableMovement()
    {
        if (heroMovement != null)
        {
            heroMovement.enabled = false;
        }
    }

    // * Wait for death animation before respawning
    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(deathDuration);
        Respawn();
    }

    // * Reset character state and position
    private void Respawn()
    {
        ResetHealth();
        transform.position = spawnPoint;
        ResetAnimations();
        EnableMovement();
    }

    // * Reset all animation states
    private void ResetAnimations()
    {
        animator.Rebind();
        animator.Update(0f);

        animator.SetBool("IsWalking", false);
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsFalling", false);
        animator.ResetTrigger("Damage");
        animator.ResetTrigger("Die");
        animator.ResetTrigger("HeroAttack");

        animator.Play("HeroIdle", 0, 0f);
    }

    // * Re-enable movement after respawn
    private void EnableMovement()
    {
        if (heroMovement != null)
        {
            heroMovement.enabled = true;
        }
    }

    // * Update UI hearts display
    private void UpdateHeartDisplay()
    {
        if (heartManager != null)
        {
            heartManager.UpdateHearts();
        }
    }

    // * Heal the hero by specified amount
    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateHeartDisplay();
    }

    // * Set new spawn point location
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