using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float _currentHealth;

    [Header("References")]
    public Animator animator;
    [SerializeField] private Healthbar healthbar;

    private bool isDead = false;

    public float currentHealth
    {
        get => _currentHealth;
        private set => _currentHealth = Mathf.Clamp(value, 0, maxHealth);
    }

    private void Start()
    {
        currentHealth = maxHealth;

        if (animator == null)
            animator = GetComponent<Animator>();

        if (healthbar != null)
        {
            healthbar.UpdateHealthBar(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDead || amount <= 0) return;

        currentHealth -= amount;
        Debug.Log("Player took damage: " + amount + " | HP: " + currentHealth);

        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }

        if (healthbar != null)
        {
            healthbar.UpdateHealthBar(currentHealth, maxHealth);
        }

        if (currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Player died!");

        if (animator != null)
        {
            animator.ResetTrigger("Hit");
            animator.Play("Death");
        }

        // Stop player movement
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.velocity = Vector2.zero;

        // Disable movement and input scripts
        PlayerMovement movement = GetComponent<PlayerMovement>();
        if (movement != null) movement.enabled = false;

        PlayerInput input = GetComponent<PlayerInput>();
        if (input != null) input.enabled = false;
    }

    public void Heal(float amount)
    {
        if (isDead || amount <= 0) return;

        currentHealth += amount;

        if (healthbar != null)
        {
            healthbar.UpdateHealthBar(currentHealth, maxHealth);
        }
    }
}
