using UnityEngine;
using System;

public static class LevelEvents
{
    public static event Action<LevelState> OnLevelStateChanged;
    public static event Action OnLevelCompleted;
    public static event Action<int> OnLevelLoading;

    public static void TriggerLevelStateChanged(LevelState newState)
    {
        OnLevelStateChanged?.Invoke(newState);
    }

    public static void TriggerLevelCompleted()
    {
        OnLevelCompleted?.Invoke();
    }

    public static void TriggerLevelLoading(int levelIndex)
    {
        OnLevelLoading?.Invoke(levelIndex);
    }
}