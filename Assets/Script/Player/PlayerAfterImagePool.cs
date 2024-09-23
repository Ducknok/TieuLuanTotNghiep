using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImagePool : MonoBehaviour
{
    [SerializeField] protected GameObject afterImagePrefab;
    [SerializeField] protected Queue<GameObject> availableObjects = new Queue<GameObject>();

    protected static PlayerAfterImagePool instance;
    public static PlayerAfterImagePool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        this.GrowPool();
    }
    protected virtual void GrowPool()
    {
        for(int i = 0; i < 10; i++)
        {
            var instanceToAdd = Instantiate(afterImagePrefab);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd);
        }
    }
    public virtual void AddToPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        this.availableObjects.Enqueue(gameObject);
    }

    public virtual GameObject GetFromPool()
    {
        if(this.availableObjects.Count == 0)
        {
            this.GrowPool();
        }
        var instance = this.availableObjects.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}
