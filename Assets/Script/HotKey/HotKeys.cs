using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotKeys : DucMonobehavior
{
    //[SerializeField] protected GameData instance;
    //public GameData Instance => instance;
    [SerializeField] protected HotKeyButton hotkeyOne;
    [SerializeField] protected HotKeyButton hotkeyTwo;
    [SerializeField] protected GameObject attackHotkey;
    [SerializeField] protected GameObject axeThrowingHotkey;
    [SerializeField] protected GameObject blockHotkey;
    [SerializeField] protected Inventory inventory;

    protected override void Start()
    {
        this.inventory = GameManagerSingleton.Instance.GetComponent<Inventory>();
        this.axeThrowingHotkey.SetActive(false);
        this.blockHotkey.SetActive(false);
    }
    protected override void Update()
    {
        // Kiểm tra nhấn phím số 1
        this.AlphaHotkey();
        this.CheckAttackHotkey();
        this.GetItemByName();

    }
    //TODO: Sua loi khi het health, mana potion thi ve amount.text = 0
    protected virtual void GetItemByName()
    {
        foreach(KeyValuePair<string, int> item in this.inventory.inventoryItems)
        {
            
            if (item.Key == hotkeyOne.itemName)
            {
                this.hotkeyOne.amount.text = item.Value.ToString();
                
            }
            if(item.Key == this.hotkeyTwo.itemName)
            {
                this.hotkeyTwo.amount.text = item.Value.ToString();         
                //Debug.Log(item.Value.ToString());
            }
        }
    }
    protected virtual void AlphaHotkey()
    {
        //Check press 1 number to use health potion
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.hotkeyOne.UseItem();
            inventory.UseInventoryItems(hotkeyOne.itemName);
        }

        //Check press 2 number to use mana potion
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            this.hotkeyTwo.UseItem();
            inventory.UseInventoryItems(hotkeyTwo.itemName);
        }
    }
    protected virtual void CheckAttackHotkey()
    {
        if (GameData.Instance.saveData.playerUnlockedAxeThrowing == true)
        {
            this.axeThrowingHotkey.SetActive(true);
        }
        if(GameData.Instance.saveData.playerUnlockedShield == true)
        {
            this.blockHotkey.SetActive(true);
        }
    }

}
