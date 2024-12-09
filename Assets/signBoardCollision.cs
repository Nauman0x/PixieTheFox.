using UnityEngine;
using UnityEngine.SceneManagement;
public class SignboardCollision : MonoBehaviour
{
    public GameObject panel; 
    private Collider2D signCollider;  

    void Start()
    {
        signCollider = GetComponent<Collider2D>(); 
    }

    private void OnCollisionEnter2D(Collision2D collision)  
    {
        if (collision.gameObject.name.StartsWith("player"))  
        {
            panel.SetActive(true);  
            signCollider.enabled = false;  
        }
    }
    public void OpenPanel()
    {
        panel.SetActive(true);
    }
    public void ClosePanel()
    {
        panel.SetActive(false);  
    }
    public void BossScene()
    {
        SceneManager.LoadScene("BossScene");
    }
}