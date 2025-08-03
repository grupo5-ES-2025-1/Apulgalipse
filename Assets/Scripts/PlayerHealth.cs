using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 3;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }

    }
}
