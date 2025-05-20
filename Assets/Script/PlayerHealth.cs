using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float _currentHealth;
    public Animator animator;
    private bool isDead = false;

    public float currentHealth
    {
        get => _currentHealth;
        private set
        {
            if (value < _currentHealth) // If health dropped
            {
                if (value > 0 && animator != null)
                {
                    Debug.Log("Playing Hit animation"); //debugging if the aninmation is playing
                    animator.SetTrigger("Hit"); 
                }
            }

            _currentHealth = Mathf.Clamp(value, 0, maxHealth);

            if (_currentHealth == 0 && !isDead)
            {
                Die();
            }
        }
    }


    private void Start()
    {
        currentHealth = maxHealth;

        if (animator == null)
            animator = GetComponent<Animator>(); // Fallback if not assigned in Inspector
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log("Player took damage: " + amount + " | HP: " + currentHealth);
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Player died!");

        if (animator != null)
        {
            animator.ResetTrigger("Hit"); // Prevents Hit animation from interrupting Death animation
            animator.Play("Death");
        }

        // Stop all movement immediately
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.velocity = Vector2.zero;

        // Disable movement and input scripts
        PlayerMovement movement = GetComponent<PlayerMovement>();
        if (movement != null) movement.enabled = false;

        PlayerInput input = GetComponent<PlayerInput>();
        if (input != null) input.enabled = false;
    }
}
