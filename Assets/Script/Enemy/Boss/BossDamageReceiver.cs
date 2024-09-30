using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageReceiver : MonoBehaviour
{
    [SerializeField] protected Boss1Controller bossCtrl;
    public Boss1Controller BossCtrl => bossCtrl;
    protected virtual void Start()
    {
        this.bossCtrl = transform.GetComponent<Boss1Controller>();
    }
    public virtual void Damage(AttackDetails attackDetails)
    {
        bossCtrl.Instance.DecreaseHealth(attackDetails.damageAmount);
    }

}
