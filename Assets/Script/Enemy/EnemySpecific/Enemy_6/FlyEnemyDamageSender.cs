using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyDamageSender : MonoBehaviour
{
    [SerializeField] protected AttackDetails attackDetails;
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected Transform attackHitBox;
    [SerializeField] protected float rangeAttack;
    [SerializeField] protected float damage;
    [SerializeField] protected Vector3 attackPosition;
    
    public virtual void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapBoxAll(this.attackHitBox.position, this.attackPosition, this.whatIsPlayer);
        this.damage = Mathf.Round(Random.Range(10f, 20f));
        this.attackDetails.damageAmount = this.damage;
        this.attackDetails.position = this.transform.position;

        foreach (Collider2D col in detectedObjects)
        {
            Debug.Log("danh may ne");
            col.transform.SendMessage("Damage", this.attackDetails);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(this.attackHitBox.position, this.attackPosition);
    }
}
