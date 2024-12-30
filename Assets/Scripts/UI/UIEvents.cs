using UnityEngine;
using System;

public static class UIEvents
{
    // Button Events
    public static event Action<string, bool> OnControlButtonStateChanged;
    public static event Action OnAttackButtonPressed;
    
    // Health Events
    public static event Action<int> OnHealthChanged;
    
    // Menu Events
    public static event Action OnMenuOpened;
    public static event Action OnMenuClosed;

    // Trigger Methods
    public static void TriggerControlButtonState(string buttonType, bool isPressed)
    {
        OnControlButtonStateChanged?.Invoke(buttonType, isPressed);
    }

    public static void TriggerAttackButton()
    {
        OnAttackButtonPressed?.Invoke();
    }

    public static void TriggerHealthChanged(int newHealth)
    {
        OnHealthChanged?.Invoke(newHealth);
    }

    public static void TriggerMenuOpened()
    {
        OnMenuOpened?.Invoke();
    }

    public static void TriggerMenuClosed()
    {
        OnMenuClosed?.Invoke();
    }
}