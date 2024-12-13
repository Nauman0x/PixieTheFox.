using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;  
    public AudioClip deathSound;      
    private float initialPositionX;
    public float moveRange = 2.5f;
    public float moveSpeed = 1f;
    public int health = 2; 

    private bool isDead = false;  
    

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();  
        initialPositionX = transform.position.x;
    }

    void Update()
    {
        if (isDead) return;  

        animator.SetBool("isWalking", true);
        float newPosX = initialPositionX + Mathf.PingPong(Time.time * moveSpeed, moveRange * 2) - moveRange;

        if (newPosX < initialPositionX)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (newPosX > initialPositionX)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;  

        health -= damage;
        Debug.Log($"Enemy hit! Health remaining: {health}");

        if (health <= 0 && !isDead)
        {
            isDead = true;

            if (deathSound != null)
            {
                audioSource.PlayOneShot(deathSound);  
            }

            animator.SetTrigger("Die");
            animator.Play("Enemt-death-Animation");

            Destroy(gameObject, 1f);  
            Debug.Log("Enemy destroyed!");
        }
    }
}
