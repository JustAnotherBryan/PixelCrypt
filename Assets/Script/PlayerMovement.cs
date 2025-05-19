using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody2D rb;
    public float moveSpeed = 5f;

    private Vector2 movementInput;
    private bool isAttacking = false;

    [Header("Animation")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    [Header("Combat")]
    public float attackDamage = 10f;

    private int attackIndex = 0;

    void Update()
    {
        if (!isAttacking)
        {
            rb.velocity = movementInput * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        HandleAnimation();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && !isAttacking)
        {
            isAttacking = true;

            string attackAnim = attackIndex % 2 == 0 ? "Attack1" : "Attack2";
            animator.Play(attackAnim);
            attackIndex++;

            Invoke(nameof(ResetAttack), 0.5f);
        }
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }

    private void HandleAnimation()
    {
        if (isAttacking) return;

        if (movementInput != Vector2.zero)
        {
            animator.Play("Run");

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
