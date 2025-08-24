using Unity.VisualScripting;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    public StatsUI statsUI;

    public PlayerHealth playerHealth;

    [Header("Combat Stats")]
    public int damage;
    public float weaponRange;
    public int knockbackForce;
    public float knockbackTime;
    public float stunTime;

    [Header("Movement Stats")]
    public int speed;
    public int stamina;

    [Header("Health Stats")]
    public int maxHealth;
    public int currentHealth;
    public int defense;

    [Header("Extra Stats")]
    public int luck;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateMaxHealth(int amount)
    {
        maxHealth += amount;
        statsUI.UpdateAllStats();

    }

    public void UpdateCurrentHealth(int amount)
    {
        playerHealth.ChangeHealth(amount);
        statsUI.UpdateAllStats();
    }

    public void UpdateSpeed(int amount)
    {
        this.speed += amount;
        statsUI.UpdateAllStats();
    }

    public void UpdateStamina(int amount)
    {
        this.stamina += amount;
        statsUI.UpdateAllStats();

    }

    public void UpdateDamage(int amount)
    {
        this.damage += amount;
        statsUI.UpdateAllStats();
    }

    public void UpdateLuck(int amount)
    {
        this.luck += amount;
        statsUI.UpdateAllStats();

    }

    public void UpdateDefense(int amount)
    {

        this.defense += amount;
        statsUI.UpdateAllStats();
        
    }
}
