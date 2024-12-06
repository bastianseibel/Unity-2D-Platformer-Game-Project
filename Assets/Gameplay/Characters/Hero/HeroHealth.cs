using UnityEngine;
using System.Collections;

public class HeroHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 2;
    public float immunityDuration = 1.5f;
    
    [Header("References")]
    private SpriteRenderer spriteRenderer;
    private HeroMovement heroMovement;
    private Animator animator;

    public int currentHealth;
    private HeartManager heartManager;
    public bool isImmune = false;

    private Vector3 spawnPoint; 

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        heroMovement = GetComponent<HeroMovement>();
        heartManager = FindObjectOfType<HeartManager>();
        animator = GetComponent<Animator>();
        spawnPoint = transform.position;
    }

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

    private IEnumerator ImmunityEffect()
    {
        isImmune = true;
        yield return new WaitForSeconds(immunityDuration);
        isImmune = false;
    }

    public void Die()
    {
        currentHealth = maxHealth;
        transform.position = spawnPoint;
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        if (heartManager != null)
        {
            heartManager.UpdateHearts();
        }
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