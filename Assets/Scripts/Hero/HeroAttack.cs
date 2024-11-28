using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour
{

    public Transform attackPoint;
    public LayerMask EnemyLayer;
    public float attackRange = 0.5f;
    public int attackDamage;
    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.ResetTrigger("HeroAttack");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }
    }

    void Attack()
    {
        anim.SetTrigger("HeroAttack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, EnemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Mushroom>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
