using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : DucMonobehavior
{
    [SerializeField] protected OtherEnemyController enemyCtrl;
    public OtherEnemyController EnemyController => enemyCtrl;
    protected override void Start()
    {
        this.enemyCtrl = transform.GetComponent<OtherEnemyController>();
    }
    public virtual void Damage(AttackDetails attackDetails)
    {
        enemyCtrl.Instance.DecreaseHealth(attackDetails.damageAmount);
    }
}
