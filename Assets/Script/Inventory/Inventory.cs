using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField] protected GameObject[] slots;
    [SerializeField] protected GameObject[] backPack;
    [SerializeField] protected bool isInstantiated;



    public Dictionary<string, int> inventoryItems = new Dictionary<string, int>();
    public void CheckSlotsAvailability(GameObject itemToAdd, string itemName, int itemAmount)
    {
        this.isInstantiated = false;
        for(int i = 0; i < this.slots.Length; i++)
        {
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
                    text.text = itemAmount.ToString();
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
                    this.slots[i].GetComponent<SlotsScript>().isUsed = true;
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
                for(int j = i + 1; j< slots.Length; j++)
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
}
