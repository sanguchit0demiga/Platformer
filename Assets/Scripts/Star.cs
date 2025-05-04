using UnityEngine;
using UnityEngine.SceneManagement;

public class Star : MonoBehaviour
{
    public float value = 1f;
    public int speed = 5;
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && scoreManager != null)
        {
            scoreManager.score += value;
            Destroy(gameObject);
        }
       
        }
    }
