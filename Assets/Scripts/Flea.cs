using UnityEngine;

public class Flea : Enemy
{
    [Header("Movement")]
    [SerializeField] private float speed = 2f;
    private Transform player;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (player != null)
            MoveTowardsPlayer();

        if (IsPlayerInRange() && canAttack)
        {
            Attack();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);

        // Animação de andar (se existir um animator configurado)
        if (animator != null)
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("Speed", direction.sqrMagnitude);
        }
    }

    private void Attack()
    {
        if (animator != null)
            animator.SetTrigger("Attack");

        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (hit != null)
        {
            hit.GetComponent<PlayerHealth>()?.TakeDamage(attackDamage);
        }

        canAttack = false;
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
