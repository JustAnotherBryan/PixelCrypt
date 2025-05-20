using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 50f;
    private float currentHealth;

    [Header("Animation")]
    public Animator animator;

    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Enemy took damage: " + amount + " | HP: " + currentHealth);

        animator.Play("hit");

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log(gameObject.name + " died!");

        animator.Play("death");

        // Disable movement
        SkeletonAI ai = GetComponent<SkeletonAI>();
        if (ai != null)
        {
            ai.enabled = false;
        }
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // Destroy after delay (optional: allow death animation to finish)
        Destroy(gameObject, 1f);
    }

}
