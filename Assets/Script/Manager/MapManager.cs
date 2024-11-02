using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : DucMonobehavior
{
    [SerializeField] private static MapManager instance;
    public static MapManager Instance => instance;
    [SerializeField] protected GameObject miniMap;
    [SerializeField] protected GameObject largeMap;

    public virtual bool IsLargeMapOpen { get; private set; }

    protected override void Awake()
    {
        if (instance != null) return;
        instance = this;
        this.CloseLargeMap();
    }
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!this.IsLargeMapOpen)
            {
                this.OpenLargeMap();
            }
            else
            {
                this.CloseLargeMap();
            }
        }
        
    }
    protected virtual void OpenLargeMap()
    {
        this.miniMap.SetActive(false);
        this.largeMap.SetActive(true);
        this.IsLargeMapOpen = true;
    }
    protected virtual void CloseLargeMap()
    {
        this.miniMap.SetActive(true);
        this.largeMap.SetActive(false);
        this.IsLargeMapOpen = false;
    }
}
