using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotKeyButton : MonoBehaviour
{
    [SerializeField] public string itemName;
    [SerializeField] public Button hotkeyButton;
    [SerializeField] public Text amount;
    [SerializeField] protected ItemUse itemUse;
    [SerializeField] protected Inventory inventory;
    [SerializeField] protected static Action onItemUsed;
    protected virtual void Start()
    {
        this.inventory = GameManagerSingleton.Instance.GetComponent<Inventory>();
    }
    protected virtual void OnEnable()
    {
        
        //TODO: add sound effect healing here
    }
    protected virtual void OnDisable()
    {
        //TODO: remove sound effect healing 
    }
    public virtual void UseItem()
    {
        foreach (GameObject slot in inventory.slots)
        {
            if (slot.GetComponent<SlotsScript>().isUsed && slot.GetComponentInChildren<ItemUse>().itemSO.itemName == itemName)
            {
                    itemUse = slot.GetComponentInChildren<ItemUse>();
                    itemUse.UseButton();
                    onItemUsed?.Invoke();
                    break;
            } 
        }
    }
}
