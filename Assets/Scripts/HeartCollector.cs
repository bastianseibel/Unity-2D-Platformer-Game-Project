using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollector : MonoBehaviour
{
    public int healthIncrease = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HeroHealth heroHealth = collision.GetComponent<HeroHealth>();
            if (heroHealth != null)
            {
                heroHealth.Heal(healthIncrease);
                Destroy(gameObject);
            }
        }
    }
}
