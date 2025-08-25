using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public HealthBar healthBar;

    void Start()
    {
        StatsManager.Instance.currentHealth = StatsManager.Instance.maxHealth;
        healthBar.SetMaxHealth(StatsManager.Instance.maxHealth);
    }

    public void ChangeHealth(int amount)
    {
        StatsManager.Instance.currentHealth += amount;

        if (StatsManager.Instance.currentHealth > StatsManager.Instance.maxHealth)
        {
            StatsManager.Instance.currentHealth = StatsManager.Instance.maxHealth;
        }

        healthBar.SetHealth(StatsManager.Instance.currentHealth);

        if (StatsManager.Instance.currentHealth <= 0)
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene when health reaches 0
        }

    }
}
