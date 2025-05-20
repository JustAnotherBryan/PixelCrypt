using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image HealthBar_Full;
    [SerializeField] private TMP_Text HealthText;

    private void Start()
    {
        if (playerHealth != null)
        {
            UpdateHealthBar(playerHealth.currentHealth, playerHealth.maxHealth);
        }
    }

    public void UpdateHealthBar(float current, float max)
    {
        float fill = current / max;
        HealthBar_Full.fillAmount = fill;
        HealthText.text = $"{Mathf.CeilToInt(current)} / {Mathf.CeilToInt(max)}";
    }
}
