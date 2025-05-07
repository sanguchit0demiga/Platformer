using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelsController: MonoBehaviour
{

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("Game"); 
    }

    public void Exit()
    {
       Application.Quit();
        
    }
}
