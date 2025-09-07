using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform ataquePoint; // para melee
    [SerializeField] private Transform launchPoint; // para ranged (deve estar na m�o)
    [SerializeField] private float ataqueRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int vida = 5;
    [SerializeField] private WeaponManager weaponManager;

    private bool isAttacking;

    void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (weaponManager == null) Debug.LogWarning("WeaponManager n�o atribu�do em PlayerAttack.");
    }

    // InputSystem chama isso
    public void PerformAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (isAttacking)
        {
            Debug.Log("Ignorando ataque: j� atacando.");
            return;
        }

        if (weaponManager == null)
        {
            Debug.LogError("WeaponManager est� null no PerformAttack.");
            return;
        }

        Weapon weapon = weaponManager.GetCurrentWeapon();
        if (weapon == null)
        {
            Debug.LogWarning("Nenhuma arma equipada ao tentar atacar.");
            return;
        }

        Debug.Log($"Realizando attack com arma: {weapon.WeaponName} (type={weapon.Type})");
        animator.SetTrigger(weapon.AttackTrigger);
        isAttacking = true;
    }

    // Deve ser chamado por Animation Event no frame de "soltar" / impacto
    public void PerformHitOrThrow()
    {
        Debug.Log("Animation Event: PerformHitOrThrow chamado.");
        if (weaponManager == null)
        {
            Debug.LogError("WeaponManager null em PerformHitOrThrow.");
            return;
        }

        Weapon weapon = weaponManager.GetCurrentWeapon();
        if (weapon == null)
        {
            Debug.LogWarning("PerformHitOrThrow: weapon null.");
            return;
        }

        switch (weapon.Type)
        {
            case WeaponType.Hands:
            case WeaponType.Melee:
                Debug.Log("Executando DealDamage (melee).");
                DealDamage(StatsManager.Instance.damage);
                break;

            case WeaponType.Ranged:
                Debug.Log("Executando LaunchProjectile (ranged).");
                LaunchProjectile(weapon);
                break;
        }
    }

    private void DealDamage(int damage)
    {
        if (ataquePoint == null)
        {
            Debug.LogWarning("DealDamage: ataquePoint null.");
            return;
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(ataquePoint.position, ataqueRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent<Enemy>(out Enemy enemyScript))
            {
                enemyScript.TakeDamage(damage);
            }
        }
    }

    private void LaunchProjectile(Weapon weapon)
    {
        if (weapon == null)
        {
            Debug.LogWarning("LaunchProjectile: weapon null");
            return;
        }

        if (weapon.ProjectilePrefab == null)
        {
            Debug.LogWarning("LaunchProjectile: ProjectilePrefab n�o atribu�do na weapon.");
            return;
        }

        if (launchPoint == null)
        {
            Debug.LogWarning("LaunchProjectile: launchPoint n�o atribu�do.");
            return;
        }

        // calcula dire��o
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPos.z = 0f;
        Vector2 startPos = launchPoint.position;
        Vector2 direction = (mouseWorldPos - (Vector3)startPos).normalized;

        Debug.Log($"Instanciando proj�til em {startPos} dire��o {direction}");

        GameObject proj = Instantiate(weapon.ProjectilePrefab, startPos, Quaternion.identity);

        // garante que proje?til esteja ativo na cena raiz
        proj.transform.SetParent(null);

        if (proj.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.linearVelocity = direction * weapon.ProjectileSpeed;
            rb.gravityScale = 0f; // top-down
        }
        else
        {
            Debug.LogWarning("Prefab do projetil n�o tem Rigidbody2D.");
        }

        // orienta sprite para a dire��o (opcional)
        proj.transform.right = direction;
    }

    // Animation Event no final da anima��o
    public void EndAttack()
    {
        isAttacking = false;
        Debug.Log("Animation Event: EndAttack chamado -> isAttacking=false");
        animator.ResetTrigger("Punch");
        animator.ResetTrigger("Throw");
    }

    private void OnDrawGizmosSelected()
    {
        if (ataquePoint == null) return;
        Gizmos.DrawWireSphere(ataquePoint.position, ataqueRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            vida--;
            if (vida <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
