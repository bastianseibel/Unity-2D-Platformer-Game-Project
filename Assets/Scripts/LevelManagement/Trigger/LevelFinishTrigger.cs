using UnityEngine;

public class LevelFinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DisablePlayer(other);
            CompleteLevel();
        }
    }

    private void DisablePlayer(Collider2D player)
    {
        HeroMovementController movementController = player.GetComponent<HeroMovementController>();
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (movementController != null)
            movementController.enabled = false;

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }
    }

    private void CompleteLevel()
    {
        if (TimerManager.Instance != null)
            TimerManager.Instance.StopTimer();

        LevelEvents.TriggerLevelCompleted();

        if (LevelManager.Instance != null)
            LevelManager.Instance.LoadLevelCompleteScene();
    }
}