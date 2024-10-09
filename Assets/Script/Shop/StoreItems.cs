using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreItems : MonoBehaviour
{
    [SerializeField] protected Inventory inventory;
    [SerializeField] protected GameManagerSingleton gameManager;
    [SerializeField] protected GameObject itemToAdd;
    [SerializeField] protected ItemSO itemSO;
    [SerializeField] protected int amountToAdd; 
    [SerializeField] protected int itemSellPrice;
    [SerializeField] protected int itemBuyPrice;
    protected TextMeshProUGUI buyPriceText;

    protected virtual void Start()
    {
        this.gameManager = GameManagerSingleton.Instance;
        this.inventory = gameManager.GetComponent<Inventory>();
        this.itemSO.itemName = itemToAdd.name;
        this.buyPriceText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        this.buyPriceText.text = "$ " + this.itemBuyPrice.ToString();
    }

    public virtual void BuyItem()
    {
        Debug.Log("da mua");
        if(this.itemBuyPrice <= BankAccount.Instance.coinBank)
        {
            
            BankAccount.Instance.Money(-this.itemBuyPrice);
            this.inventory.CheckSlotsAvailability(this.itemToAdd, this.itemToAdd.name, this.amountToAdd);
        }
    }

}
