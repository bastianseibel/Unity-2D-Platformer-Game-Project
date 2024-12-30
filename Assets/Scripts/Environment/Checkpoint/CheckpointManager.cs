using UnityEngine;

// * Manage the checkpoint and player respawn, saves the checkpoint position

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }
    
    private Vector3 currentCheckpoint;
    private bool hasCheckpoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        CheckpointEvents.OnCheckpointActivated += SetCheckpoint;
    }

    private void OnDisable()
    {
        CheckpointEvents.OnCheckpointActivated -= SetCheckpoint;
    }

    private void SetCheckpoint(Vector3 position)
    {
        currentCheckpoint = position;
        hasCheckpoint = true;
    }

    public Vector3 GetRespawnPosition()
    {
        return hasCheckpoint ? currentCheckpoint : Vector3.zero;
    }

    public void RespawnPlayer(GameObject player)
    {
        if (!hasCheckpoint) return;
        
        player.transform.position = currentCheckpoint;
        CheckpointEvents.TriggerPlayerRespawned(currentCheckpoint);
    }
}