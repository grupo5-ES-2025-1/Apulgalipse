using UnityEngine;
using UnityEngine.InputSystem;
public class movimentacaoPlayer : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        rb.linearVelocity = input * 5f; // Adjust speed as necessary
    }
}
