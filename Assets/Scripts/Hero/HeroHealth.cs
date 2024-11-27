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

    public int currentHealth;
    private HeartManager heartManager;
    public bool isImmune = false;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        heroMovement = GetComponent<HeroMovement>();
        heartManager = FindObjectOfType<HeartManager>();
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

            if (currentHealth <= 0)
            {
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
        
        // Blinc Effect during immunity
        float endTime = Time.time + immunityDuration;
        while (Time.time < endTime)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(1, 1, 1, 1f);
            yield return new WaitForSeconds(0.1f);
        }

        spriteRenderer.color = new Color(1, 1, 1, 1f);
        isImmune = false;
    }

    private void Die()
    {
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        if (heartManager != null)
        {
            heartManager.UpdateHearts();
        }
    }
}