using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageReceiver : DucMonobehavior
{
    [SerializeField] protected BossController bossCtrl;
    public BossController BossCtrl => bossCtrl;
    protected override void Start()
    {
        this.bossCtrl = transform.GetComponent<BossController>();
    }
    public virtual void Damage(AttackDetails attackDetails)
    {
        bossCtrl.Instance.DecreaseHealth(attackDetails.damageAmount);
    }

}
