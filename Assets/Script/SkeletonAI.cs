using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SkeletonAI : MonoBehaviour
{
    [Header("Chase Settings")]
    public float moveSpeed = 2f;
    public float stopDistance = 0.5f;

    [Header("References")]
    public Animator animator;
    public Transform player;

    private Rigidbody2D rb;

    public float damage = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Auto-find player if not set manually
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

        // Stop moving if close enough
        if (distance > stopDistance)
        {
            rb.velocity = direction * moveSpeed;
            animator.Play("walk");

            // Optional: Flip sprite based on movement direction
            if (direction.x < 0)
                GetComponent<SpriteRenderer>().flipX = true;
            else if (direction.x > 0)
                GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.Play("idle");
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

}
