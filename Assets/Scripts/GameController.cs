using UnityEngine;

public class GameController : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        
    }

    public void PlayerDefeated()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
