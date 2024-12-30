using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
public class HeroAnimationController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private HeroGroundCheck groundCheck;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<HeroGroundCheck>();
    }

    private void Update()
    {
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        if (groundCheck.IsGrounded)
        {
            animator.SetBool("IsWalking", Mathf.Abs(rb.velocity.x) > 0.2f);
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsJumping", rb.velocity.y > 1f);
            animator.SetBool("IsFalling", rb.velocity.y < -1f);
        }
    }

    public void PlayDamageAnimation()
    {
        animator.SetTrigger("Damage");
    }

    public void PlayDeathAnimation()
    {
        animator.SetTrigger("Die");
    }

    public void ResetAnimations()
    {
        Debug.Log("Resetting animations");
        // * Reset all animations
        animator.Rebind();
        animator.Update(0f);

        // * Reset all parameters
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            switch (param.type)
            {
                case AnimatorControllerParameterType.Bool:
                    animator.SetBool(param.name, false);
                    break;
                case AnimatorControllerParameterType.Trigger:
                    animator.ResetTrigger(param.name);
                    break;
            }
        }

        animator.Play("HeroIdle", 0, 0f);
    }
}