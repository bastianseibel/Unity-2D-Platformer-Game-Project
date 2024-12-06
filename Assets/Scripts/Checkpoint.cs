using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isActivated = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("NoBurn");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActivated)
        {
           ActivateCheckpoint(collision.gameObject);
        }
    }

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