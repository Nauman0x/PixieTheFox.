using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private bool isJumping = false;

    public GameObject bullet;
    private Animator animator;
    private Rigidbody2D rb;
    private float moveDirection = 0f;

    private Vector3 bulletOffset = new Vector3(0.5f, 0, 0);  

   
    private int cherryCount = 0; 

    public Text bulletCountText;  
    public int initialBullets = 20;  
    private int currentBullets;  

    // Health System
    public int maxHealth = 5; 
    private int currentHealth;
    public Image[] hearts; 

    public Text cherryCountText;  

    public GameObject gameOverPanel;  
    public Text gameOverText;

    public LayerMask groundLayer; 
    public Transform groundCheck; 
    public float groundCheckRadius = 0.2f; 

    public AudioClip jumpSound;  
    public AudioClip hurtSound;     
    public AudioClip cherrySound;   
    public AudioClip shroomSound;   

    private AudioSource audioSource;  

    void Start()
    {
        
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();  

        currentHealth = maxHealth;  
        UpdateHealthUI();  

        gameOverPanel.SetActive(false);  

        cherryCountText.text = "x " + cherryCount.ToString();  

        currentBullets = initialBullets;  
        UpdateBulletCountUI();  
    }

    void Update()
    {
        if (currentHealth <= 0) 
        {
            Debug.Log("Health is 0, triggering Game Over");
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }

        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);

        if (moveDirection != 0)
        {
            animator.SetBool("isRunning", true);
            transform.localScale = new Vector3(Mathf.Sign(moveDirection), 1, 1);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

       
    }

    private void LoadBossScene()
    {
        SceneManager.LoadScene("BossScene");  
    }


    public void MoveLeft() => moveDirection = -1f;
    public void MoveRight() => moveDirection = 1f;
    public void StopMoving() => moveDirection = 0f;
    
    

    public void Jump()
    {
        if (IsGrounded() && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("isJumping", true);
            isJumping = true;

            if (jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);  
            }
        }
    }

    void FixedUpdate()
    {
        if (IsGrounded() && rb.velocity.y <= 0)
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void Attack()
    {
        if (currentBullets > 0)  
        {
            animator.SetTrigger("Attack");
            float facingDirection = transform.localScale.x;
            Vector3 spawnPosition = transform.position + bulletOffset * facingDirection;
            GameObject newBullet = Instantiate(bullet, spawnPosition, Quaternion.identity);
            newBullet.GetComponent<Bullet>().SetDirection(facingDirection);

            currentBullets--;  
            UpdateBulletCountUI(); 
        }
        else
        {
            Debug.Log("Out of bullets!");  
        }
    }

    private void UpdateBulletCountUI()
    {
        bulletCountText.text = "x " + currentBullets.ToString();  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("cherry"))
        {
            CollectCherry(collision.gameObject);
        }

        if (collision.gameObject.name.StartsWith("shrooms"))
        {
            CollectOrb(collision.gameObject);
            animator.Play("Item-feedback-Animation");
        }

        if (collision.gameObject.name.StartsWith("opossum") || collision.gameObject.name.StartsWith("vulture")|| 
            collision.gameObject.name.StartsWith("bat")||collision.gameObject.name.StartsWith("dino")
            ||collision.gameObject.name.StartsWith("frog")||collision.gameObject.name.StartsWith("Spikes"))
        {
            TakeDamage(1);
        }
    }

    private void CollectOrb(GameObject orb)
    {
        Destroy(orb);
        currentBullets += 10;
        UpdateBulletCountUI();

        if (shroomSound != null)
        {
            audioSource.PlayOneShot(shroomSound);
        }

        animator.Play("Item-feedback-Animation");
    }


    private void CollectCherry(GameObject cherry)
    {
        Destroy(cherry);
        cherryCount++;

        if (cherrySound != null)
        {
            audioSource.PlayOneShot(cherrySound);
        }

        cherryCountText.text = "x " + cherryCount.ToString();

        if (cherryCount >= 5)
        {
            cherryCount = 0;
            cherryCountText.text = "x " + cherryCount.ToString();

            if (currentHealth < maxHealth)
            {
                currentHealth++;
                UpdateHealthUI();
            }
        }
    }


    private void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = (i < currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            animator.SetTrigger("Hurt");

            if (hurtSound != null)
            {
                audioSource.PlayOneShot(hurtSound);
            }

            if (currentHealth < 0) currentHealth = 0;
            UpdateHealthUI();
        }
    }


    private void GameOver()
    {
        animator.SetTrigger("Die");
        rb.velocity = Vector2.zero;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("ForestScene");
    }
    public void RestartSkyScene()
    {
        SceneManager.LoadScene("SkyScene");
    }
}