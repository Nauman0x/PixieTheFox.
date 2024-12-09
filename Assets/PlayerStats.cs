using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public int cherryCount = 0;
    public int currentHealth = 5;
    public int score = 0;
    public int currentBullets = 20;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetStats()
    {
        cherryCount = 0;
        currentHealth = 5;
        score = 0;
        currentBullets = 20;
    }
}