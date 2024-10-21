using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] protected ItemSO itemSO;
    protected Inventory inventory;
    protected GameManagerSingleton gameManager;
    // Start is called before the first frame update
    void Start()
    {
        this.gameManager = GameManagerSingleton.Instance;
        this.inventory = gameManager.GetComponent<Inventory>();
    }

    public virtual void UseItem()
    {
        this.inventory.UseInventoryItems(this.itemSO.itemName);
    }
}
