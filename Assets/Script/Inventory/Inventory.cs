 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField] public GameObject[] slots;
    [SerializeField] protected GameObject[] backPack;
    [SerializeField] protected bool isInstantiated;
    [SerializeField] protected ItemList itemList;

    public Dictionary<string, int> inventoryItems = new Dictionary<string, int>();

    protected virtual void Start()
    {
        if(itemList != null)
        {
            this.DataToInventory();
        }
        
    }

    public void CheckSlotsAvailability(GameObject itemToAdd, string itemName, int itemAmount)
    {
        this.isInstantiated = false;
        for(int i = 0; i < this.slots.Length; i++)
        {
            Debug.Log(slots[i]);
            if (this.slots[i].transform.childCount > 0)
            {
                this.slots[i].GetComponent<SlotsScript>().isUsed = true;
            }
            else if (!this.isInstantiated && !this.slots[i].GetComponent<SlotsScript>().isUsed)
            {
                if (!this.inventoryItems.ContainsKey(itemName))
                {
                    GameObject item = Instantiate(itemToAdd, this.slots[i].transform.position, Quaternion.identity);
                    item.transform.SetParent(this.slots[i].transform, false);
                    //Debug.Log(this.slots[i].transform);
                    item.transform.localPosition = new Vector3(0, 0, 0);
                    item.name = item.name.Replace("(Clone)", "");
                    this.isInstantiated = true;
                    this.slots[i].GetComponent<SlotsScript>().isUsed = true;
                    inventoryItems.Add(itemName, itemAmount);
                    this.text = this.slots[i].GetComponentInChildren<TextMeshProUGUI>();
                    this.text.text = itemAmount.ToString();
                    break;
                }
                else
                {
                    for(int j = 0; j < slots.Length; j++)
                    {
                        //Debug.Log("adding" + itemName);
                        if(this.slots[j].transform.GetChild(0).gameObject.name  == itemName)
                        {
                            this.inventoryItems[itemName] += itemAmount;
                            text = slots[j].GetComponentInChildren<TextMeshProUGUI>();
                            text.text = inventoryItems[itemName].ToString();
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }
    public void UseInventoryItems(string itemName)
    {
        for(int i = 0;i < slots.Length; i++)
        {
            if (!this.slots[i].GetComponent<SlotsScript>().isUsed)
            {
                continue;
            }
            if (this.slots[i].transform.GetChild(0).gameObject.name == itemName)
            {
                text = slots[i].GetComponentInChildren<TextMeshProUGUI>();
                this.inventoryItems[itemName]--;
                text.text = inventoryItems[itemName].ToString();
                if(inventoryItems[itemName] <= 0)
                {
                    Destroy(slots[i].transform.GetChild(0).gameObject);
                    this.slots[i].GetComponent<SlotsScript>().isUsed = false;
                    this.inventoryItems.Remove(itemName);
                    this.ReorganizeInventory();
                }
                break;
            }
        }
    }
    protected virtual void ReorganizeInventory()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (!this.slots[i].GetComponent<SlotsScript>().isUsed)
            {
                for(int j = i + 1; j < slots.Length; j++)
                {
                    if (this.slots[j].GetComponent<SlotsScript>().isUsed)
                    {
                        Transform itemToMove = this.slots[j].transform.GetChild(0).transform;
                        itemToMove.transform.SetParent(this.slots[i].transform, false);
                        itemToMove.transform.localPosition = new Vector3(0, 0, 0);
                        this.slots[i].GetComponent<SlotsScript>().isUsed = true;
                        this.slots[j].GetComponent<SlotsScript>().isUsed = false;
                        break;
                    }
                }
            }
        }
    }
    public void InventoryToData()
    {
        for(int i = 0; i < this.slots.Length; i++)
        {
            if (this.slots[i].GetComponent<SlotsScript>().isUsed)
            {
                if(!GameData.Instance.saveData.goToAddID.Contains(this.slots[i].GetComponentInChildren<ItemUse>().ID))
                {
                    GameData.Instance.saveData.goToAddID.Add(this.slots[i].GetComponentInChildren<ItemUse>().ID);
                    GameData.Instance.saveData.inventoryItemsName.Add(this.slots[i].GetComponentInChildren<ItemUse>().name);
                    GameData.Instance.saveData.inventoryItemsAmount.Add(this.inventoryItems[slots[i].GetComponentInChildren<ItemUse>().name]);
                }
            }
        }
    }
    public void DataToInventory()
    {
        Debug.Log(GameData.Instance.saveData.goToAddID.Count);
        for(int i = 0; i < GameData.Instance.saveData.goToAddID.Count; i++)
        {
            for(int j = 0; j < itemList.items.Count; j++)
            {
                if(itemList.items[j].ID == GameData.Instance.saveData.goToAddID[i])
                {
                    this.CheckSlotsAvailability(itemList.items[j].gameObject, GameData.Instance.saveData.inventoryItemsName[i], GameData.Instance.saveData.inventoryItemsAmount[i]);
                }
            }
        }
    }
}
