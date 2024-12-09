using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;

    public AudioClip bulletSound;
    private AudioSource audioSource;
    private Rigidbody2D rb;
    public LayerMask groundLayer; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        if (bulletSound != null)
        {
            audioSource.PlayOneShot(bulletSound);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    public void InitializeBullet(Vector2 newDirection, float newSpeed)
    {
        direction = newDirection.normalized;
        speed = newSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                player.TakeDamage(1); 
            }

            Destroy(gameObject); 
        }

        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            Destroy(gameObject);
        }
    }
}