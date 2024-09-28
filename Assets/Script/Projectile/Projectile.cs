using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected Transform damagePosition;
    [SerializeField] protected AttackDetails attackDetails;
    [SerializeField] protected float speed;
    [SerializeField] protected float travelDistance;
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
    protected virtual void Update()
    {
        if (!hasHitGround)
        {
            this.attackDetails.position = this.transform.position;  
            if (isGravityOn)
            {
                //this.rb.gravityScale = 8.0f;
                float angle = Mathf.Atan2(this.rb.velocity.y, this.rb.velocity.x) * Mathf.Rad2Deg;
                this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
    protected virtual void FixedUpdate()
    {
        if (!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(this.damagePosition.position, this.damageRadius, this.whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(this.damagePosition.position, this.damageRadius, this.whatIsGround);

            if (damageHit)
            {
                damageHit.transform.SendMessage("Damage", attackDetails);
                Destroy(gameObject);
            }
            if (groundHit)
            {
                this.hasHitGround = true;
                this.rb.gravityScale = 0f;
                this.rb.velocity = Vector2.zero;
                Destroy(gameObject, 1f);
            }
            if (Mathf.Abs(this.xStartPos - this.transform.position.x) >= this.travelDistance && !this.isGravityOn)
            {
                this.isGravityOn = true;
                this.rb.gravityScale = this.gravity;
            }
        }
    }

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
