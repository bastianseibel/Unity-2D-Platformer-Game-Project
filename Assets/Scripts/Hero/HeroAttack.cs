using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    public LayerMask EnemyLayer;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private int attackDamage;

    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.ResetTrigger("HeroAttack");
    }

    private void OnEnable()
    {
        UIEvents.OnAttackButtonPressed += HandleAttack;
    }

    private void OnDisable()
    {
        UIEvents.OnAttackButtonPressed -= HandleAttack;
    }

    private void HandleAttack()
    {
        Attack();
    }

    void Attack()
    {
        anim.SetTrigger("HeroAttack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, EnemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}