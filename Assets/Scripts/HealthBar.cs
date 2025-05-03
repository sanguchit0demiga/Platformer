using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image healthbar;
    public float currentHealth;
    public float maxHealth;
    void Start()
    {
        UpdateBar();
    }

    public void UpdateBar()
    {
        healthbar.fillAmount = currentHealth / maxHealth;
    }
}
