using UnityEngine;
using UnityEngine.UI;

public class BossFrog : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip deathSound;
    public GameObject player;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float shootInterval = 2f; 
    public float bulletSpeed = 5f; 
    public int health = 24; 

    public GameObject healthContainer; 
    private int currentHealthImageIndex = 0; 
    private int healthStep; 

    private bool isDead = false;
    private float shootTimer = 0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player object not found.");
        }

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (healthContainer == null)
        {
            Debug.LogError("Health container is not assigned.");
        }

        if (healthContainer != null && healthContainer.transform.childCount > 0)
        {
            healthStep = health / healthContainer.transform.childCount;
        }
    }

    void Update()
    {
        if (isDead) return;

        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            ShootRandomBullet();
            shootTimer = 0f;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        UpdateHealthImages();

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
    }

    private void ShootRandomBullet()
    {
        for (int i = 0; i < 3; i++)
        {
            float randomAngle = Random.Range(0f, 360f);
            Vector2 direction = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            bullet.GetComponent<enemyBullet>().InitializeBullet(direction, bulletSpeed);
        }
    }

    private void UpdateHealthImages()
    {
        if (healthContainer == null) return;

        int healthImageIndexToDisable = (24 - health) / healthStep;

        while (currentHealthImageIndex < healthImageIndexToDisable)
        {
            Transform healthImage = healthContainer.transform.GetChild(currentHealthImageIndex);
            healthImage.gameObject.SetActive(false);
            currentHealthImageIndex++;
        }
    }
}
