using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody2D rb;
    public float moveSpeed = 5f;

    private Vector2 movementInput;

    [Header("Animation")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    void Update()
    {
        // Move the player
        rb.velocity = movementInput * moveSpeed;

        // Handle animation
        HandleAnimation();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        Debug.Log("Movement Input : " + movementInput);
    }

    private void HandleAnimation()
    {
        // Play "Run" if moving, otherwise "Idle"
        if (movementInput != Vector2.zero)
        {
            animator.Play("Run");

            // Flip sprite left/right based on horizontal movement
            if (movementInput.x < 0)
                spriteRenderer.flipX = true;
            else if (movementInput.x > 0)
                spriteRenderer.flipX = false;
        }
        else
        {
            animator.Play("Idle");
        }

    

    }
}
