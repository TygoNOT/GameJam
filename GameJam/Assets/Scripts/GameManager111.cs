using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

[System.Serializable]
public class FlowerItem
{
    public int FlowerID;
    public string flowerName;
    public GameObject flowerPrefab; 
    public int price;               
}

public class GameManager111 : MonoBehaviour
{
    [Header("Gameplay Stats")]
    [SerializeField]
    private int playerGold;


    [Header("Gameobjects")]
    private Text playerGold_Text;
    public Text timerText;

    [Header("Input Binds")]
    private string addMoneyKey = "AddMoney";
    private string spendMoneyKey = "SpendMoney";

    [Header("Garden and Flowers")]
    [SerializeField] private int gardenHP;
    public List<FlowerItem> availableFlowers;

    [Header("Timer Settings")]
    public bool timerIsRunning = false;
    public float currentTime = 0f; 

    [Header("Colors")]
    public Color runningColor = Color.green;
    public Color completedColor = Color.red;

    private Coroutine timerCoroutine;

    void Start()
    {

        if (playerGold_Text == null)
        {
            playerGold_Text = GameObject.Find("MoneyAmount")?.GetComponent<Text>();
        }

        if (playerGold_Text != null)
            playerGold_Text.text = playerGold.ToString();
        else
            Debug.LogError("Couldn't find 'MoneyAmount' Text object.");
    }   
    void Update()
    {
        playerGold_Text.text = playerGold.ToString() ;

        if (Input.GetButtonDown(addMoneyKey))
        {
            AddMoney(100);
        }

        if (Input.GetButtonDown(spendMoneyKey))
        {
            SpentMoney(20);
        }
    }
    public void RestartTimer()
    {
        ResetTimer();
        StartTimer();
    }
    public void ResetTimer()
    {
        currentTime = 0f;
        UpdateTimerDisplay();
    }
    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = FormatTime(currentTime);
        }
    }
    private string FormatTime(float timeInSeconds)
    {
        int hours = Mathf.FloorToInt(timeInSeconds / 3600f);
        int minutes = Mathf.FloorToInt((timeInSeconds % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        int milliseconds = Mathf.FloorToInt((timeInSeconds * 1000) % 1000);

        if (hours > 0)
            return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        else
            return string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }
    private void UpdateTimerColor(Color color)
    {
        if (timerText != null)
        {
            timerText.color = color;
        }
    }
    public float GetCurrentTime()
    {
        return currentTime;
    }
    private IEnumerator TimerRoutine()
    {
        UpdateTimerColor(runningColor);

        while (timerIsRunning)
        {
            currentTime += Time.deltaTime;
            UpdateTimerDisplay();
            yield return null;
        }
    }
    public void StartTimer()
    {
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);

        timerIsRunning = true;
        timerCoroutine = StartCoroutine(TimerRoutine());
    }
    public void GetAddMoney(int amount)
    {
        AddMoney(amount);
    }
    private void AddMoney(int amount)
    {
        playerGold += amount;
        UpdateMoneyUI();
    }
    private void SpentMoney(int amount)
    {
        if (playerGold >= amount)
        {
            playerGold -= amount;
            UpdateMoneyUI();
        }
        else Debug.Log("You cant afford that !!!");
    }
    private void UpdateMoneyUI()
    {
        if (playerGold_Text != null)
            playerGold_Text.text = playerGold.ToString();
    }


    public void BuyFlower(int flowerID)
    {
        if (flowerID < 0 || flowerID >= availableFlowers.Count)
        {
            Debug.LogError("Неверный ID цветка!");
            return;
        }

        FlowerItem item = availableFlowers[flowerID];

        if (playerGold < item.price)
        {
            return;
        }

        if (!GardenManager.Instance.HasFreeBed())
        {
            return;
        }

        playerGold -= item.price;
        GardenManager.Instance.PlantFlowerInFirstFreeBed(item.flowerPrefab);
    }

}
