using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : DucMonobehavior
{
    [Header("BossHealth")]
    [SerializeField] protected BossHealth instance;
    public BossHealth Instance => instance;
    protected override void Start()
    {
        this.LoadBossHealth();
    }
    protected virtual void LoadBossHealth()
    {
        if (this.instance != null) return;
        this.instance = transform.GetComponent<BossHealth>();
    }

}
