using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : MonoBehaviour
{
    [Header("BossHealth")]
    [SerializeField] protected BossHealth instance;
    public BossHealth Instance => instance;
    protected virtual void Start()
    {
        this.LoadBossHealth();
    }
    protected virtual void LoadBossHealth()
    {
        if (this.instance != null) return;
        this.instance = transform.GetComponent<BossHealth>();
    }

}
