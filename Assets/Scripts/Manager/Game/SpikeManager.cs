using UnityEngine;

public class SpikeGroupController : MonoBehaviour
{
    public int damage = 1;
    public bool useSingleCollider = true;

    void Start()
    {
        if (!useSingleCollider)
        {
            foreach (Transform child in transform)
            {
                BoxCollider2D collider = child.gameObject.AddComponent<BoxCollider2D>();
                collider.isTrigger = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HeroHealth heroHealth = collision.GetComponent<HeroHealth>();
            if (heroHealth != null)
            {
                heroHealth.TakeDamage(damage);
            }
        }
    }
}