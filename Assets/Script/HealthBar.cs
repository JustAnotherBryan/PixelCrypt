using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private PlayerHealth currentHealth;
    [SerializeField] private Image HealthBar_Empty;
    [SerializeField] private Image HealthBar_Full;

    private void Start()
    {
        if (currentHealth != null)
        {
            HealthBar_Full.fillAmount = PlayerHealth.currentHealth / 10;
            UpdateHealthBar(currentHealth.currentHealth, currentHealth.maxHealth);
        }
    }


    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        HealthBar_Full.fillAmount = currentHealth / maxHealth;
    }
}

