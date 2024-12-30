using UnityEngine;
public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayDeathAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }
    }
}