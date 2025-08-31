using UnityEngine;

public class Flea : Enemy
{
    [Header("Locomoção")]
    [SerializeField] private float moveSpeed = 3.5f;  // mais rápida que o zumbi
    [SerializeField] private float chaseRange = 6f;   // enxerga o player de mais longe
    [SerializeField] private float stopChaseRange = 10f; // demora mais pra desistir

    private Transform player;
    private Rigidbody2D rb;
    private bool isChasing;
    private float lastAttackTime;
    private bool isTouchingPlayer = false;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();

        if (stopChaseRange <= chaseRange)
            stopChaseRange = chaseRange + 0.1f;
    }

    private void Update()
    {
        if (!player) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (!isChasing && dist <= chaseRange)
            isChasing = true;
        else if (isChasing && dist >= stopChaseRange)
            isChasing = false;

        if (!isChasing)
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", false);
            return;
        }

        if (isTouchingPlayer)
        {
            if (rb) rb.linearVelocity = Vector2.zero;
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", true);
            Attack();
        }
        else if (dist > attackRange)
        {
            MoveTowardsPlayer();
            animator.SetBool("isMoving", true);
            animator.SetBool("isAttacking", false);
        }
        else
        {
            if (rb) rb.linearVelocity = Vector2.zero;
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", true);
            Attack();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 toPlayer = (Vector2)(player.position - transform.position);
        Vector2 dir = toPlayer.sqrMagnitude > 0.0001f ? toPlayer.normalized : Vector2.zero;

        if (rb)
            rb.MovePosition(rb.position + dir * moveSpeed * Time.deltaTime);
        else
            transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);

        if (Mathf.Abs(dir.x) > 0.01f)
            transform.localScale = new Vector3(dir.x < 0 ? -1 : 1, 1, 1);
    }

    private void Attack()
    {
        if (Time.time < lastAttackTime + attackCooldown) return;

        animator.SetTrigger("Attack");
        lastAttackTime = Time.time;

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        Debug.Log($"Pulga atacou, encontrou {hits.Length} colliders.");
        foreach (var h in hits)
        {
            Debug.Log($"Collider encontrado: {h.name}");
            h.GetComponent<PlayerHealth>()?.TakeDamage(attackDamage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isTouchingPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isTouchingPlayer = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, stopChaseRange);
    }
}
