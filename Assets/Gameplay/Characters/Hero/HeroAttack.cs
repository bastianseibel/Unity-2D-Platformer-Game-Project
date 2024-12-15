using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    // * Attack settings
    public Transform attackPoint;
    public LayerMask EnemyLayer;
    public float attackRange = 0.5f;
    public int attackDamage;

    // * Animation component
    private Animator anim;
    
    // * Initialize animator
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.ResetTrigger("HeroAttack");
    }

    // * Called by button press
    public void OnAttackButtonPressed()
    {
        Attack();
    }

    // * Perform attack and damage enemies
    void Attack()
    {
        // Trigger attack animation
        anim.SetTrigger("HeroAttack");

        // Check for enemies in attack range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, EnemyLayer);

        // Deal damage to each enemy hit
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Mushroom>().TakeDamage(attackDamage);
        }
    }

    // * Draw attack range in editor for debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}