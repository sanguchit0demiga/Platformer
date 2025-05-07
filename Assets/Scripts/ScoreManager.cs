using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float score = 0;
    public Text scoreText;



    private void Update()
    {
        scoreText.text = "X " + score.ToString();
    }
}

