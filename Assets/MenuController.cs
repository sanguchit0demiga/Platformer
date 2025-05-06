using UnityEngine;

public class MenuController : MonoBehaviour
{
    void Start()
    {
        
    }

    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");

    }
public void ExitGame()
{
    Application.Quit();
    
}
}
