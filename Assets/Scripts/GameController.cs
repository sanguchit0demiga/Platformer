using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public float initialTime = 90f;
    private float timeRemaining;
    public Text timeReamainingText;
    public GameObject finalPanel;
    private bool countdownStarted = false;

    void Start()
    {
        finalPanel.SetActive(false);
    }

    void Update()
    {
        if (countdownStarted && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timeReamainingText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else if (countdownStarted && timeRemaining <= 0)
        {
            ShowFinalPanel();
        }
    }

    public void StartCountdown()
    {
        countdownStarted = true;
        timeRemaining = initialTime;
    }

    private void ShowFinalPanel()
    {

        finalPanel.SetActive(true);
    }

public void PlayerDefeated()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
