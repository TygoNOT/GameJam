using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text hpText;
    private int maxHP;

    public void SetMaxHealth(int maxHealth)
    {
        maxHP = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        UpdateText(maxHealth);
    }

    public void SetHealth(int currentHealth)
    {
        slider.value = currentHealth;
        UpdateText(currentHealth);
    }

    private void UpdateText(int currentHealth)
    {
        if (hpText != null)
            hpText.text = $"{currentHealth} / {maxHP}";
    }

    public void FollowTarget(Transform target, Vector3 offset)
    {
        transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
    }
}
