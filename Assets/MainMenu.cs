using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject controlsPanel; 

   

    public void PlayGame()
    {
        SceneManager.LoadScene("ForestScene");
    }

    public void OpenPanel()
    {
        controlsPanel.SetActive(true);  
    }
    public void ClosePanel()
    {
        controlsPanel.SetActive(false); 
    }
    
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}