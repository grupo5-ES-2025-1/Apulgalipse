using UnityEngine;

public class movimentoPulga : MonoBehaviour
{
public Transform player; // Reference to the player's transform
public float speed = 2f; // Movement speed of the enemy
    public float followRange = 5f; // Distance within which the enemy starts following

public Animator animator; // Reference to the Animator component

    void Update()
{
// Calculate distance between enemy and player
float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Check if player is within follow range
        if (distanceToPlayer <= followRange)
        {
            // Move towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        // Optional: flip the enemy sprite based on player position
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Flip sprite to face left
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // Unflip sprite to face right
        }
}
}