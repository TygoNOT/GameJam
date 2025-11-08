using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("PlayerStats")]
    public int playerMovementSpeed = 5;
    public float playerAttackSpeed = 1f;
    public int playerBaseAttackDmg = 10;
    public int playerLevel = 1;
    
    [Header("XP System")]
    public int currentXP = 0;
    public int requiredXP = 100;

    [Header("UI Elements")]
    public Slider xpSlider;
    public TMP_Text xpText;
    public TMP_Text levelText;
    public GameObject levelUpPanel;

    private void Start()
    {
        UpdateXPUI();
        UpdateLevelUI();
        levelUpPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GainXP(40);
        }
    }

    public void GainXP(int amount)
    {
        currentXP += amount;
        if (currentXP >= requiredXP)
        {
            currentXP -= requiredXP;
            LevelUp();
        }
        UpdateXPUI();
    }

    private void LevelUp()
    {
        playerLevel++;
        requiredXP = Mathf.RoundToInt(requiredXP * 2f);
        levelUpPanel.SetActive(true);
        UpdateLevelUI();
    }

    private void UpdateXPUI()
    {
        if (xpSlider != null)
        {
            xpSlider.maxValue = requiredXP;
            xpSlider.value = currentXP;
        }

        if (xpText != null)
        {
            xpText.text = $"{currentXP} / {requiredXP}";
        }
    }

    private void UpdateLevelUI()
    {
        if (levelText != null)
            levelText.text = $"Level: {playerLevel}";
    }

    public void IncreaseAttackSpeed()
    {
        playerAttackSpeed += 0.2f;
        CloseLevelUpPanel();
    }

    public void IncreaseAttackPower()
    {
        playerBaseAttackDmg += 5;
        CloseLevelUpPanel();
    }

    public void IncreaseMovementSpeed()
    {
        playerMovementSpeed += 1;
        CloseLevelUpPanel();
    }

    private void CloseLevelUpPanel()
    {
        levelUpPanel.SetActive(false);
        UpdateXPUI();
    }
}
