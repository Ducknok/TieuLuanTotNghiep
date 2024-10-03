using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamageReceiver : MonoBehaviour
{
    [SerializeField] protected TrapController trapCtrl;
    public TrapController TrapController => trapCtrl;
    protected virtual void Start()
    {
        this.trapCtrl = transform.GetComponent<TrapController>();
    }
    public virtual void Damage(AttackDetails attackDetails)
    {
        trapCtrl.Instance.DecreaseHealth(attackDetails.damageAmount);
    }
}
