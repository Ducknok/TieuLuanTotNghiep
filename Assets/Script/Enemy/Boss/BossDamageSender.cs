using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageSender : MonoBehaviour
{
    [SerializeField] protected Transform attack1HitBoxPos;
    [SerializeField] protected Transform attack2HitBoxPos;
    [SerializeField] protected LayerMask whatIsPlayer;
    
    [SerializeField] protected float attack1Damage;
    [SerializeField] protected float attack2Damage;
    [SerializeField] protected Vector3 attack1Position;
    [SerializeField] protected Vector3 attack2Position;
    [SerializeField] public AttackDetails attackDetails;
    public virtual void CheckAttack1HitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapBoxAll(this.attack1HitBoxPos.position, this.attack1Position, this.whatIsPlayer);
        this.attackDetails.damageAmount = this.attack1Damage;
        this.attackDetails.position = this.transform.position;
        
        foreach (Collider2D col in detectedObjects)
        {
                col.transform.SendMessage("Damage", this.attackDetails);
        }
    }
    public virtual void CheckAttack2HitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapBoxAll(this.attack2HitBoxPos.position, this.attack2Position, this.whatIsPlayer);
        this.attackDetails.damageAmount = this.attack1Damage;
        this.attackDetails.position = this.transform.position;

        foreach (Collider2D col in detectedObjects)
        {
                col.transform.SendMessage("Damage", this.attackDetails);
        }
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(this.attack1HitBoxPos.position, this.attack1Position);
        Gizmos.DrawWireCube(this.attack2HitBoxPos.position, this.attack2Position);
    }

}
