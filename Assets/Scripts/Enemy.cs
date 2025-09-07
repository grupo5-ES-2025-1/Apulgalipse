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

    public GameObject lootPrefab;
    public ItemSO gold;


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
        if (lootPrefab != null)
        {
            Loot loot = Instantiate(lootPrefab, transform.position, Quaternion.identity).GetComponent<Loot>();
            loot.canBePickedUp = true;
            loot.Initialize(gold, 7 * (1 + StatsManager.Instance.luck) / 2 );

        }
        Destroy(gameObject); // pode trocar por anima��o de morte
    }

    protected bool IsPlayerInRange()
    {
        return Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
    }
}
