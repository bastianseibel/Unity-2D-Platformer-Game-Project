using UnityEngine;
public class EnemyDamageDealer : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HeroHealth heroHealth = collision.GetComponent<HeroHealth>();
            if (heroHealth != null)
            {
                heroHealth.TakeDamage(damageAmount);
            }
        }
    }
}