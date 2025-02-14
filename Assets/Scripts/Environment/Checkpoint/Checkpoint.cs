using UnityEngine;

// * take care of the checkpoint it's activated and player respawned
public class Checkpoint : MonoBehaviour
{
    private bool isActivated = false;
    private Animator animator;

    [SerializeField] private AudioSource checkpointSound;

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
        checkpointSound.Play();
        isActivated = true;
        animator.SetTrigger("Burn");
        CheckpointEvents.TriggerCheckpointActivated(transform.position);
    }
}