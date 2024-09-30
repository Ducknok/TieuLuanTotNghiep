using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : MonoBehaviour
{
    [Header("BossDamageReceiver")]
    [SerializeField] protected BossHealth instance;
    public BossHealth Instance => instance;
    protected virtual void Start()
    {
        this.LoadBossDamageReceiver();
    }
    protected virtual void LoadBossDamageReceiver()
    {
        if (this.instance != null) return;
        this.instance = transform.GetComponent<BossHealth>();
    }

}
