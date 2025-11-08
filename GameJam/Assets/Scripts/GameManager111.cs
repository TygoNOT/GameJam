using UnityEngine;
using UnityEngine.UI;

public class GameManager111 : MonoBehaviour
{
    [Header("Gameplay Stats")]
    [SerializeField]
    private short moneyAmount;
    [SerializeField]
    private short gardenHP;
    [SerializeField]
    private short flowerPrice = 20;

    [Header("Gameobjects")]
    private Text moneyText;

    [Header("Input Binds")]
    private string addMoneyKey = "AddMoney";
    private string spendMoneyKey = "SpendMoney";
    void Start()
    {
        if (moneyText == null)
        {
            moneyText = GameObject.Find("MoneyAmount")?.GetComponent<Text>();
        }

        if (moneyText != null)
            moneyText.text = moneyAmount.ToString();
        else
            Debug.LogError("Couldn't find 'MoneyAmount' Text object.");
    }   
    void Update()
    {
        if (Input.GetButtonDown(addMoneyKey))
        {
            AddMoney(100);
        }

        if (Input.GetButtonDown(spendMoneyKey))
        {
            SpentMoney(20);
        }
    }
    private void AddMoney(short amount)
    {
        moneyAmount += amount;
        UpdateMoneyUI();
    }
    private void SpentMoney(short amount)
    {
        if (moneyAmount >= amount)
        {
            moneyAmount -= amount;
            UpdateMoneyUI();
        }
        else Debug.Log("You cant afford that !!!");
    }
    private void UpdateMoneyUI()
    {
        if (moneyText != null)
            moneyText.text = moneyAmount.ToString();
    }

    public void GetSpendMoneyForShop()
    {
        SpentMoney(flowerPrice);
    }
}
