using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [Header("TrapHealth")]
    [SerializeField] protected TrapHealth instance;
    public TrapHealth Instance => instance;
    protected virtual void Start()
    {
        this.LoadTrapDamageReceiver();
    }
    protected virtual void LoadTrapDamageReceiver()
    {
        if (this.instance != null) return;
        this.instance = transform.GetComponent<TrapHealth>();
    }
}
