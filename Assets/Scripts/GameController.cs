using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public float initialTime = 120f;
    private float timeRemaining;
    public Text timeReamainingText;
    public GameObject losePanel;
    private bool countdownStarted = false;
    public int starsCollected = 0;
    public ScoreManager scoreManager;
    public GameObject[] stars;
    public GameObject[] hearts;
    public Text finalScoreText;
    public GameObject winPanel;
    public GameObject[] enemies;

    void Start()
    {
        stars = GameObject.FindGameObjectsWithTag("Star");
        hearts = GameObject.FindGameObjectsWithTag("Heart");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        
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
           
            timeRemaining = 0;
            Debug.Log("Tiempo agotado, activando el panel de derrota.");
            ShowLosePanel();
            countdownStarted = false;
        }
    }

    public void StartCountdown()
    {
        countdownStarted = true;
        timeRemaining = initialTime;
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
        if (losePanel != null)
        {
            losePanel.SetActive(true);
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
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                Enemy enemyScript = enemies[i].GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.ResetEnemy();
                }
                else
                {
                    enemies[i].SetActive(true);
                }
            }
        }
    }
        public void ShowWinPanel()
    {
       Cursor.lockState= CursorLockMode.None;
        Cursor.visible = true;

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
        if (finalScoreText != null)
        {
            finalScoreText.text = "Final score: " + starsCollected.ToString();
           
        }
    }
    public void ShowLosePanel()
    {
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }
    }
}

