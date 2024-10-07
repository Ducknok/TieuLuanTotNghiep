using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropSpawner : Spawner
{
    private static ItemDropSpawner instance;
    public static ItemDropSpawner Instance => instance;
    [SerializeField] protected float gameDropRate;
    [SerializeField] private float dropRadius = 2f; // Phạm vi drop item

    protected virtual void Awake()
    {
        ItemDropSpawner.instance = this;
    }
    public virtual List<ItemDropRate> Drop(List<ItemDropRate> dropList, Vector3 pos, Quaternion rot)
    {
        List<ItemDropRate> dropItems = new List<ItemDropRate>();
        Vector2 randomPos = Random.insideUnitCircle * dropRadius;

        if (dropList.Count < 1) return dropItems;
        dropItems = this.DropItems(dropList);

        foreach(ItemDropRate itemDropRate in dropItems)
        {
            ItemCode itemCode = itemDropRate.itemSO.itemCode;
            Transform itemDrop = this.Spawn(itemCode.ToString(),pos, rot);
            if (itemDrop == null) continue;
            itemDrop.gameObject.SetActive(true);
        }
        return dropItems;
    }
    protected virtual List<ItemDropRate> DropItems(List<ItemDropRate> items)
    {
        List<ItemDropRate> dropedItems = new List<ItemDropRate>();
        float rate, itemRate;
        int itemDropMore;
        
        foreach (ItemDropRate item in items)
        {
            rate = Random.Range(0, 1f);
            if(item.itemSO.itemCode == ItemCode.Gold)
            {
                this.gameDropRate = Random.Range(3f, 6f);
            }
            else
            {
                this.gameDropRate = 1f;
            }
            itemRate = item.dropRate/1000000f * this.gameDropRate;
            itemDropMore = Mathf.FloorToInt(itemRate);

            if(itemDropMore > 0)
            {
                itemRate -= itemDropMore;
                for(int i = 0; i<itemDropMore; i++)
                {
                    dropedItems.Add(item);
                }
            }
            //Debug.Log(item.dropRate / 100000f);
            if (rate <= itemRate)
            {
                dropedItems.Add(item);
            }
        }
        return dropedItems;
    }
}
