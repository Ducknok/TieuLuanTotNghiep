using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Holder")]
    [SerializeField] protected Transform holder;
    [SerializeField] protected List<Transform> prefabs;
    [SerializeField] protected List<Transform> poolObjects;
    [SerializeField] protected int spawnedCount = 0;
    public int SpawnedCount => spawnedCount;


    protected virtual void Start()
    {
        this.LoadHolder();
        this.LoadPrefabs();
    }
    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Holder");
    }
    protected virtual void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) return;
        Transform prefabObj = transform.Find("Prefabs");
        foreach (Transform prefab in prefabObj)
        {
            this.prefabs.Add(prefab);
        }
        this.HidePrefabs();
        Debug.Log(transform.name + ": LoadPrefabs ", gameObject);
    }
    protected virtual void HidePrefabs()
    {
        foreach (Transform prefab in this.prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }
    public virtual Transform Spawn(string prefabName, Vector3 spawnPos, Quaternion rotation)
    {
        Transform prefab = GetPrefabByName(prefabName);
        if (prefab == null)
        {
            Debug.LogWarning("Prefab was not found: " + prefab);
            return null;
        }
        return this.Spawn(prefab, spawnPos, rotation);
    }
    public virtual Transform Spawn(Transform prefab, Vector3 spawnPos, Quaternion rotation)
    {
        Transform newPrefab = this.GetObjectFromPool(prefab);
        newPrefab.SetPositionAndRotation(spawnPos, rotation);
        newPrefab.parent = this.holder;
        this.spawnedCount++;
        return newPrefab;
    }
    protected virtual Transform GetObjectFromPool(Transform prefab)
    {
        foreach (Transform poolObj in this.poolObjects)
        {
            if (poolObj.name == prefab.name)
            {
                this.poolObjects.Remove(poolObj);
                return poolObj;
            }
        }

        Transform newPrefab = Instantiate(prefab);
        newPrefab.name = prefab.name;
        return newPrefab;
    }
    public virtual void Despawn(Transform obj)
    {
        this.poolObjects.Add(obj);
        obj.gameObject.SetActive(false);
        this.spawnedCount--;
    }
    //Tim vien dan duoc goi
    public virtual Transform GetPrefabByName(string prefabName)
    {
        foreach (Transform prefab in this.prefabs)
        {
            if (prefab.name == prefabName) return prefab;
        }
        return null;
    }
}
