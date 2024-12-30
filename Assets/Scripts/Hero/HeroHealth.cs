using UnityEngine;
using System.Collections;

public class HeroHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 2;
    [SerializeField] private float immunityDuration = 1.5f;
    [SerializeField] private float deathDuration = 1.5f;

    private HeroMovementController movementController;
    private HeroAnimationController animationController;
    private Vector3 spawnPoint;
    private bool isImmune = false;
    private int _currentHealth;

    public int currentHealth
    {
        get => _currentHealth;
        private set
        {
            _currentHealth = value;
            UIEvents.TriggerHealthChanged(_currentHealth);
        }
    }

    private void Start()
    {
        InitializeComponents();
        ResetHealth();
    }

    private void InitializeComponents()
    {
        movementController = GetComponent<HeroMovementController>();
        animationController = GetComponent<HeroAnimationController>();
        spawnPoint = transform.position;
    }

    private void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isImmune) return;

        currentHealth = Mathf.Max(0, currentHealth - damage);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            HandleDamageEffect();
        }
    }

    private void HandleDamageEffect()
    {
        animationController.PlayDamageAnimation();
        StartCoroutine(ImmunityEffect());
    }

    private IEnumerator ImmunityEffect()
    {
        isImmune = true;
        yield return new WaitForSeconds(immunityDuration);
        isImmune = false;
    }

    public void Die()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }

        DisableMovement();
        animationController.PlayDeathAnimation();
        StartCoroutine(RespawnAfterDelay());
    }

    private void DisableMovement()
    {
        if (movementController != null)
        {
            movementController.enabled = false;
        }
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(deathDuration);
        Respawn();
    }

    private void Respawn()
    {
        if (animationController != null)
        {
            animationController.ResetAnimations();
        }

        ResetHealth();
        transform.position = spawnPoint;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.velocity = Vector2.zero;
        }

        if (movementController != null)
        {
            movementController.ResetMovementState();
            movementController.enabled = true;
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public void SetSpawnPoint(Vector3 position)
    {
        spawnPoint = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            SetSpawnPoint(transform.position);
        }
    }
}