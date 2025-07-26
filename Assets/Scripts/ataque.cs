using UnityEngine;
using UnityEngine.InputSystem;
public class ataque : MonoBehaviour
{

    public Animator animator;
    private bool isAttacking;
    public Transform ataquePoint;
    public float ataqueRange = 0.5f;
    public LayerMask enemyLayers;

    void Update()
    {

    }
    public void PerformAttack(InputAction.CallbackContext context)
    {
        if (context.performed && !isAttacking) {
            animator.SetTrigger("Attack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(ataquePoint.position, ataqueRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                // Assuming the enemy has a method to take damage
                enemy.GetComponent<Enemy>().TakeDamage(1); // Adjust damage as necessary
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (ataquePoint == null)
            return;

        Gizmos.DrawWireSphere(ataquePoint.position, ataqueRange);
    }

}
