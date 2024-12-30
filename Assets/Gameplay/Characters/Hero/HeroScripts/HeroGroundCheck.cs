using UnityEngine;
using System;

public class HeroGroundCheck : MonoBehaviour
{
    public event System.Action<bool> OnGroundedStateChanged;

    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckRadius = 0.3f;
    [SerializeField] private LayerMask groundLayer;

    public bool IsGrounded { get; private set; }
    public LayerMask GroundLayer => groundLayer;

    private void Update()
    {
        CheckGroundedStatus();
    }

    private void CheckGroundedStatus()
    {
        bool wasGrounded = IsGrounded;
        
        bool centerGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        bool leftGrounded = Physics2D.OverlapCircle(
            groundCheckPoint.position + new Vector3(-0.2f, 0, 0), 
            groundCheckRadius, 
            groundLayer
        );
        bool rightGrounded = Physics2D.OverlapCircle(
            groundCheckPoint.position + new Vector3(0.2f, 0, 0), 
            groundCheckRadius, 
            groundLayer
        );

        IsGrounded = centerGrounded || leftGrounded || rightGrounded;

        if (wasGrounded != IsGrounded)
        {
            OnGroundedStateChanged?.Invoke(IsGrounded);
        }
    }

    private void OnDrawGizmos()
    {
        if (groundCheckPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
            Gizmos.DrawWireSphere(groundCheckPoint.position + new Vector3(-0.2f, 0, 0), groundCheckRadius);
            Gizmos.DrawWireSphere(groundCheckPoint.position + new Vector3(0.2f, 0, 0), groundCheckRadius);
        }
    }
}