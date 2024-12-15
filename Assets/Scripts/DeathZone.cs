using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // * Check for player collision with death zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HeroHealth heroHealth = collision.GetComponent<HeroHealth>();
            if (heroHealth != null)
            {
                heroHealth.Die();
            }
        }
    }
}