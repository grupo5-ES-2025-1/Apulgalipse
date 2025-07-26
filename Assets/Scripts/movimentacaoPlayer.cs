using UnityEngine;
using UnityEngine.InputSystem;
public class movimentacaoPlayer : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public float speed = 3f;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private int isMovingHash = Animator.StringToHash("isMoving");
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
        animator.SetBool(isMovingHash, input != Vector2.zero);
        rb.linearVelocity = input * speed; // Adjust speed as necessary

    }
}
