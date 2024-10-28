using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : MonoBehaviour
{
    [SerializeField] protected OtherEnemyController enemyCtrl;
    public OtherEnemyController EnemyController => enemyCtrl;
    protected virtual void Start()
    {
        this.enemyCtrl = transform.GetComponent<OtherEnemyController>();
    }
    public virtual void Damage(AttackDetails attackDetails)
    {
        enemyCtrl.Instance.DecreaseHealth(attackDetails.damageAmount);
    }
}
