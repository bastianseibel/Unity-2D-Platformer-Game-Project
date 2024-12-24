using UnityEngine;
using UnityEngine.SceneManagement;

public class Finishpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Timer stoppen
            if (TimerManager.Instance != null)
            {
                TimerManager.Instance.StopTimer();
                Debug.Log($"Level beendet! Zeit: {TimerManager.Instance.GetCurrentTime():F2} Sekunden");
            }

            // Level freischalten
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            if (SaveLoadManager.Instance != null)  // Prüfe ob SaveLoadManager existiert
            {
                SaveLoadManager.Instance.UnlockLevel(currentIndex);
            }
            else
            {
                Debug.LogError("SaveLoadManager nicht gefunden!");
            }

            // Spieler deaktivieren
            if (other.TryGetComponent<HeroMovement>(out var heroMovement))
            {
                heroMovement.enabled = false;
            }
            if (other.TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.velocity = Vector2.zero;
            }

            // Level Complete Screen laden
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