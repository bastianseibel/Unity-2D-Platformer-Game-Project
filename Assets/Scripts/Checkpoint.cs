using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // * Track if checkpoint has been activated
    private bool isActivated = false;
    private Animator animator;

    // * Set initial animation state
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("NoBurn");
    }

    // * Check for player collision with checkpoint
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActivated)
        {
        ActivateCheckpoint(collision.gameObject);
        }
    }

    // * Activate checkpoint and set player spawn point
    private void ActivateCheckpoint(GameObject player)
    {
        isActivated = true;
        
        HeroHealth heroHealth = player.GetComponent<HeroHealth>();
        if (heroHealth != null)
        {
            heroHealth.SetSpawnPoint(transform.position);
        }

        animator.SetTrigger("Burn");
    }
}