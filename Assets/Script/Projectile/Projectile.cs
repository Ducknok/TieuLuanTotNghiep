using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected LayerMask whatIsGround;
    
    [SerializeField] protected Transform damagePosition;
    [SerializeField] protected AttackDetails attackDetails;
    protected float speed;
    protected float travelDistance;
    [SerializeField] protected float xStartPos;
    [SerializeField] protected float gravity;
    [SerializeField] protected float damageRadius;
    [SerializeField] protected bool isGravityOn;
    [SerializeField] protected bool hasHitGround;

    protected virtual void Start() 
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.rb.gravityScale = 0.0f;
        this.isGravityOn = false;
        this.xStartPos = this.transform.position.x;
    }
    protected virtual void Update(){}
    protected virtual void FixedUpdate(){}

    public virtual void FireProjectTile(float speed, float travelDistance, float damage)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        this.attackDetails.damageAmount = damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.damagePosition.position, this.damageRadius);
    }
}
