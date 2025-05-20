using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;

        // Safety check
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Player took damage: " + amount + " | HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
        animator.Play("Death");

        // Disable movement
        GetComponent<PlayerMovement>()?.enabled = false;
        GetComponent<PlayerInput>()?.enabled = false;
    }
}
