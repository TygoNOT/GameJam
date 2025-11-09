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
    [SerializeField] private AudioClip buySound;
    [SerializeField] private AudioSource audioSource;


    [Header("Input Binds")]
    private string addMoneyKey = "AddMoney";
    private string spendMoneyKey = "SpendMoney";

    [Header("Garden and Flowers")]
    [SerializeField] private int gardenHP;
    public List<FlowerItem> availableFlowers;

    [Header("Variables")]
    private float time = 0f;

    void Start()
    {
        timerText = GameObject.Find("Timer")?.GetComponent<Text>();
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
        time += Time.deltaTime;
        int hours = (int)(time / 3600);
        int minutes = (int)((time % 3600) / 60);
        int seconds = (int)(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

        playerGold_Text.text = playerGold.ToString() ;

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
    
    private void UpdateMoneyUI()
    {
        if (playerGold_Text != null)
            playerGold_Text.text = playerGold.ToString();
    }


    public void BuyFlower(int flowerID)
    {
        if (flowerID < 0 || flowerID >= availableFlowers.Count)
        {
            return;
        }

        FlowerItem item = availableFlowers[flowerID];

        if (playerGold < item.price)
        {
            return;
        }

        if (GardenManager.Instance.HasFlowerWithID(flowerID))
        {
            return;
        }

        if (!GardenManager.Instance.HasFreeBed())
        {
            return;
        }
        audioSource.PlayOneShot(buySound);
        playerGold -= item.price;
        UpdateMoneyUI();
        GardenManager.Instance.PlantFlowerInFirstFreeBed(item.flowerPrefab);
    }


}
