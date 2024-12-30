using UnityEngine;
using System;

// * For communication between checkpoint and hero
public static class CheckpointEvents
{
    public static event Action<Vector3> OnCheckpointActivated;
    public static event Action<Vector3> OnPlayerRespawned;

    public static void TriggerCheckpointActivated(Vector3 position)
    {
        OnCheckpointActivated?.Invoke(position);
    }

    public static void TriggerPlayerRespawned(Vector3 position)
    {
        OnPlayerRespawned?.Invoke(position);
    }
}