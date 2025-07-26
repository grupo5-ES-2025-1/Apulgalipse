using UnityEngine;

public class Enemy : MonoBehaviour
{   
    public int health = 3; // Example health value
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void TakeDamage(int damage)
    {
        
            health -= damage;
            if (health <= 0)
            {
                Destroy(gameObject); // Destroy the enemy when health reaches 0
            }
        
    }
}
