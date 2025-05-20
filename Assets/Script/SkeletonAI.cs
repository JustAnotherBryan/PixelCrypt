using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SkeletonAI : MonoBehaviour
{
    [Header("Chase Settings")]
    public float moveSpeed = 2f;
    public float stopDistance = 1f;

    [Header("Attack Settings")]
    public float damage = 10f;
    public float attackCooldown = 1.5f;

    [Header("References")]
    public Animator animator;
    public Transform player;

    private Rigidbody2D rb;
    private float lastAttackTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer)
                player = foundPlayer.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            rb.velocity = direction * moveSpeed;
            animator.Play("walk");

            if (direction.x < 0)
                GetComponent<SpriteRenderer>().flipX = true;
            else if (direction.x > 0)
                GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            rb.velocity = Vector2.zero;

            // Attack if enough time has passed
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                animator.Play("attack");
                lastAttackTime = Time.time;

                // Optional: delay actual damage to sync with animation
                Invoke(nameof(DamagePlayer), 0.4f); // 0.4s is animation hit frame
            }
            else
            {
                animator.Play("idle");
            }
        }
    }

    private void DamagePlayer()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= stopDistance + 0.1f)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
