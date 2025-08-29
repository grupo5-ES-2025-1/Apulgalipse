using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;

    void Start()
    {
        // Inicializa a vida no StatsManager
        StatsManager.Instance.currentHealth = StatsManager.Instance.maxHealth;
        healthBar.SetMaxHealth(StatsManager.Instance.maxHealth);
    }

    // Método genérico para alterar a vida (positivo ou negativo)
    public void ChangeHealth(int amount)
    {
        StatsManager.Instance.currentHealth += amount;

        if (StatsManager.Instance.currentHealth > StatsManager.Instance.maxHealth)
            StatsManager.Instance.currentHealth = StatsManager.Instance.maxHealth;

        healthBar.SetHealth(StatsManager.Instance.currentHealth);

        if (StatsManager.Instance.currentHealth <= 0)
            Die();
    }

    // Método público para inimigos chamarem
    public void TakeDamage(int damage)
    {
        Debug.Log($"Player levou {damage} de dano!");
        ChangeHealth(-damage);
    }

    // Método opcional para curar
    public void Heal(int amount)
    {
        ChangeHealth(amount);
    }

    private void Die()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
