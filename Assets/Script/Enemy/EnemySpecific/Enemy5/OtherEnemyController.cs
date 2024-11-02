using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherEnemyController : DucMonobehavior
{
    [Header("EnemyHealth")]
    [SerializeField] protected EnemyHealth instance;
    public EnemyHealth Instance => instance;
    protected override void Start()
    {
        this.LoadEnemyDamageReceiver();
    }
    protected virtual void LoadEnemyDamageReceiver()
    {
        if (this.instance != null) return;
        this.instance = transform.GetComponent<EnemyHealth>();
    }
}
