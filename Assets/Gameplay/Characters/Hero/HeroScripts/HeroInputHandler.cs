using UnityEngine;
using System;

public class HeroInputHandler : MonoBehaviour
{
    public event System.Action<float> OnMoveInput;
    public event System.Action OnJumpInput;

    private void Update()
    {

        float moveInput = Input.GetAxis("Horizontal");
        OnMoveInput?.Invoke(moveInput);

        if (Input.GetButtonDown("Jump"))
        {
            OnJumpInput?.Invoke();
        }
    }
}