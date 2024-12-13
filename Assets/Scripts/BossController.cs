using UnityEngine;

public class BossController : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource; 
    public AudioClip deathSound;     
    public GameObject Player;        
    public GameObject enemyPrefab;   
    public float moveSpeed = 1f;     
    public float followRange = 20f;  
    public int health = 2;          
    public GameObject cherryPrefab;  
    public GameObject shroomPrefab;
    private bool isDead = false;     
    private float spawnInterval = 15f; 
    private float spawnTimer = 0f;    
    private float cherrySpawnInterval = 8f;
    private float shroomSpawnInterval = 20f; 
    private float cherrySpawnTimer = 0f;
    private float shroomSpawnTimer = 0f;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");

        if (Player == null)
        {
            Debug.LogError("Player object not found. Make sure the Player has the correct tag.");
        }

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

       
    }

    void FixedUpdate()
    {
        if (isDead || Player == null) return;

        float distanceToPlayer = Vector3.Distance(Player.transform.position, transform.position);

        if (distanceToPlayer <= followRange)
        {
            Vector3 direction = (Player.transform.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; 

        health -= damage;
        Debug.Log($"Boss hit! Health remaining: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true; 

        if (deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        if (animator != null)
        {
            animator.SetTrigger("Die");
            animator.Play("Enemy-death-Animation");
        }

      
        Destroy(gameObject, 1f); 
        Debug.Log("Boss destroyed!");
    }

    void Update()
    {
        GameObject bossFrog = GameObject.FindWithTag("BossFrog");
        GameObject enemy = GameObject.FindWithTag("Enemy");
        if (bossFrog == null && enemy == null)
        {
            return;
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            InstantiateEnemies();  
            spawnTimer = 0f;  
        }

        cherrySpawnTimer += Time.deltaTime;
        shroomSpawnTimer += Time.deltaTime;

        if (cherrySpawnTimer >= cherrySpawnInterval)
        {
            InstantiateCherry();
            cherrySpawnTimer = 0f; 
        }

        if (shroomSpawnTimer >= shroomSpawnInterval)
        {
            InstantiateShroom();
            shroomSpawnTimer = 0f;
        }
    }
    private void InstantiateEnemies()
    {
        if (enemyPrefab != null)
        {
            float spawnX = Random.Range(-4.25f, -4.75f); 
            float spawnY = 3.78f;                   
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, transform.position.z); 

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity); 

            Debug.Log($"Enemy instantiated at position: {spawnPosition}");
        }
      
    }



    private void InstantiateCherry()
    {
        if (cherryPrefab != null)
        {
            float spawnX = Random.Range(-11f, 1.5f); 
            float spawnY = Random.Range(-2.8f, -0.3f);
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, transform.position.z);

            Instantiate(cherryPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Cherry instantiated!");
        }
       
    }

    private void InstantiateShroom()
    {
        if (shroomPrefab != null)
        {
            float spawnX = Random.Range(-11f, 1.5f); 
            float spawnY = Random.Range(-2.8f, -0.3f);
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, transform.position.z);

            Instantiate(shroomPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Shroom instantiated!");
        }
      
    }



}
