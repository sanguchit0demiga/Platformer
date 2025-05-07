using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public float initialTime = 90f;
    private float timeRemaining;
    public Text timeReamainingText;
    public GameObject finalPanel;
    private bool countdownStarted = false;
    public int starsCollected = 0;
    public ScoreManager scoreManager;
    public GameObject[] stars;
    public GameObject[] hearts;

    void Start()
    {
        stars = GameObject.FindGameObjectsWithTag("Star");
        hearts = GameObject.FindGameObjectsWithTag("Heart");
        finalPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Start"))
        {

            Destroy(other.gameObject);



            StartCountdown();
        }
    }

    public void PlayerDefeated()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (finalPanel != null)
        {
            finalPanel.SetActive(true);
        }
    }
    public float GetTimeRemaining()
    {
        return timeRemaining;
    }

    public void ResetCollectibles()
    {
        starsCollected = 0;
        if (scoreManager != null)
        {
            scoreManager.score = 0;
        }

        for (int i = 0; i < stars.Length; i++)
        {
            if (stars[i] != null)
            {
                stars[i].SetActive(true);
            }
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
            {
                hearts[i].SetActive(true);
            }
        }
    }
}

