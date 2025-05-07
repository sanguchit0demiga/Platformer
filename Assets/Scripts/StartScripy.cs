using UnityEngine;
using UnityEngine.UI;

public class StartScripy : MonoBehaviour
{
    public GameController gameController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Start"))
        {
            Destroy(other.gameObject);
            gameController.StartCountdown();
        }
    }
}