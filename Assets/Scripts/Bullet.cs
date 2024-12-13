using UnityEngine;
using UnityEngine.SceneManagement;  

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    private float direction = 1f;

    public AudioClip bulletSound;  
    private AudioSource audioSource;

    public LayerMask groundLayer;  

    private static int treeHitCount = 0;  

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 

        if (bulletSound != null)
        {
            audioSource.PlayOneShot(bulletSound);
        }
    }

    public void SetDirection(float dir)
    {
        direction = dir;
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.right * direction * speed * Time.fixedDeltaTime);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            Debug.Log("Bullet hit the ground!");
            Destroy(gameObject);  
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("opossum") || 
            collision.gameObject.name.StartsWith("vulture") || 
            collision.gameObject.name.StartsWith("bat") || 
            collision.gameObject.name.StartsWith("dino") || 
            collision.gameObject.name.StartsWith("frog"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            BossController boss = collision.gameObject.GetComponent<BossController>();
            BossFrog frog = collision.gameObject.GetComponent<BossFrog>();

            if (enemy != null)
            {
                enemy.TakeDamage(1);  
            }

            if (boss != null)
            {
                boss.TakeDamage(1); 
            }
            if (frog != null)
            {
                frog.TakeDamage(1);  
            }

            Destroy(gameObject);  
        }

        if (collision.gameObject.name.StartsWith("magic"))
        {
            treeHitCount++;
            Debug.Log($"Tree hit count: {treeHitCount}");

            if (treeHitCount >= 3)
            {
                Debug.Log("Tree hit 3 times! Loading SampleScene...");
                SceneManager.LoadScene("SkyScene"); 
            }

            Destroy(gameObject);  
        }
    }
}
