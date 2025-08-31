using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected int health = 3;
    [SerializeField] protected int attackDamage = 1;

    [Header("Attack")]
    [SerializeField] protected float attackRange = 0.5f;
    [SerializeField] protected float attackCooldown = 1f;

    [Header("References")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask playerLayer;

    protected bool canAttack = true;

    // Agora pode ser sobrescrito
    protected virtual void Start() { }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject); // pode trocar por animação de morte
    }

    protected bool IsPlayerInRange()
    {
        return Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
    }
}
