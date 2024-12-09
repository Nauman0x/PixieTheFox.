using UnityEngine;

public class BabyMovement : MonoBehaviour
{
    public float speed = 3f; 
    private GameObject player;
    public float stopDistance = 1f; 
    public GameObject wonPanel; 
    private AudioSource audioSource; 
    public AudioClip winSound; 
    private bool hasPlayedWinSound = false; 

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player object not found.");
        }

        if (wonPanel != null)
        {
            wonPanel.SetActive(false);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on this GameObject.");
        }
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("BossFrog") == null && GameObject.FindGameObjectWithTag("Enemy") == null && player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, player.transform.position) <= stopDistance)
            {
                speed = 0f;

                if (wonPanel != null)
                {
                    wonPanel.SetActive(true);
                }

                if (!hasPlayedWinSound && audioSource != null && winSound != null)
                {
                    audioSource.PlayOneShot(winSound);
                    hasPlayedWinSound = true; 
                }
            }
        }
    }
}
