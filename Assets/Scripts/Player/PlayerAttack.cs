using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private Transform ataquePoint;
    [SerializeField] private float ataqueRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int vida = 5;

    private bool isAttacking;
    void Update()
    {

    }
    public void PerformAttack(InputAction.CallbackContext context)
    {
        if (context.performed && !isAttacking)
        {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("enemy"))
        {
            vida--;
            if (vida <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
            }
        }
    }
}
