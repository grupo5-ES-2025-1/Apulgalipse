using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Configurações")]
    [SerializeField] private float speed = 3f;

    [Header("UI")]
    [SerializeField] private GameObject inventory;

    [Header("Ataque")]
    [SerializeField] private Transform attackPoint; // Attack point do player
    [SerializeField] private float attackOffsetX = 0.5f; // Distância horizontal do player

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input.x < 0)
        {
            spriteRenderer.flipX = true; // Flip sprite for left movement
        }
        else if (input.x > 0)
        {
            spriteRenderer.flipX = false; // Unflip sprite for right movement
        }
        animator.SetFloat("magnitude", input.magnitude);
        rb.linearVelocity = input * speed; // Adjust speed as necessary

    }
    public void Inventory(InputAction.CallbackContext context)
    { 
        if (inventory != null)
        {
            inventory.SetActive(!inventory.activeSelf); // Toggle inventory visibility
        }
    }

    private void Update()
    {
        // Atualiza posição do attackPoint de acordo com a direção do player
        if (attackPoint != null)
        {
            attackPoint.localPosition = new Vector3(
                spriteRenderer.flipX ? -attackOffsetX : attackOffsetX,
                attackPoint.localPosition.y,
                attackPoint.localPosition.z
            );
        }
    }
}
