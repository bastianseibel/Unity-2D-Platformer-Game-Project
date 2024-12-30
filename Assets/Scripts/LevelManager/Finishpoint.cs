using UnityEngine;
using UnityEngine.SceneManagement;

public class Finishpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // * Disable player movement
            HeroMovementController movementController = other.GetComponent<HeroMovementController>();
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            
            if (movementController != null)
            {
                movementController.enabled = false;
            }

            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
            }

            // * Stop timer
            if (TimerManager.Instance != null)
            {
                TimerManager.Instance.StopTimer();
            }

            // * Load Level Complete Menu
            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.LoadLevelCompleteScene();
            }
            else
            {
                Debug.LogError("LevelManager nicht gefunden!");
            }
        }
    }
}