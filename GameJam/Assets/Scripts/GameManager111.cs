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

    [Header("Input Binds")]
    private string addMoneyKey = "AddMoney";
    private string spendMoneyKey = "SpendMoney";

    [Header("Garden and Flowers")]
    [SerializeField] private int gardenHP;
    public List<FlowerItem> availableFlowers;

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
