using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelsController: MonoBehaviour
{
    public void TryAgain()
    {
        SceneManager.LoadScene("Game"); 
    }

    public void Exit()
    {
       Application.Quit();
        
    }
}
