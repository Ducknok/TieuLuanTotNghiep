using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropSpawner : Spawner
{
    private static ItemDropSpawner instance;
    public static ItemDropSpawner Instance => instance;
    

    protected virtual void Awake()
    {
        if (ItemDropSpawner.instance != null) Debug.LogError("Only 1 ItemDropSpawner allow to exist");
        ItemDropSpawner.instance = this;
    }
    public virtual void Drop(List<DropRate> dropList, Vector3 pos, Quaternion rot)
    { 
        ItemCode itemCode = dropList[1].itemSO.itemCode;
        Debug.Log(dropList[1].itemSO.itemCode);
        Transform itemDrop = this.Spawn(itemCode.ToString(), pos, rot);
        if (itemDrop == null) return;
        itemDrop.gameObject.SetActive(true);
    }
}
