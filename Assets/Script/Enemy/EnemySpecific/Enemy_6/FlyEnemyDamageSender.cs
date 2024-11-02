using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyDamageSender : DucMonobehavior
{
    public DamagePopup instance;
    [SerializeField] protected AttackDetails attackDetails;
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected Transform attackHitBox;
    [SerializeField] protected float rangeAttack;
    [SerializeField] protected float damage;
    [SerializeField] protected Vector2 attackPosition;
    
    public virtual void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapBoxAll(this.attackHitBox.position, this.attackPosition, this.whatIsPlayer);
        Debug.Log(this.whatIsPlayer.value);
        this.damage = Mathf.Round(Random.Range(10f, 20f));
        bool isCritical = Random.Range(0, 100) < 10;
        if (isCritical) this.damage *= 2;
        this.attackDetails.damageAmount = this.damage;
        foreach(Collider2D col in detectedObjects)
        {
            col.transform.SendMessage("Damage", this.attackDetails);
            this.instance.Create(this.attackHitBox.position, this.attackDetails.damageAmount, isCritical);
        }
        
    }
    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(this.attackHitBox.position, this.attackPosition);
    }
}
