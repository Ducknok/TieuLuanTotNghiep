using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : Projectile
{
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected GameObject target;
    [SerializeField] protected Transform shooter;

    public Transform Shooter => shooter;
    

    protected override void Start()
    {
        base.Start();
        this.target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (this.target.transform.position - transform.position).normalized * speed;
        this.rb.velocity = new Vector2(moveDir.x, moveDir.y);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Collider2D damageHit = Physics2D.OverlapCircle(this.damagePosition.position, this.damageRadius, this.whatIsPlayer);
        Collider2D groundHit = Physics2D.OverlapCircle(this.damagePosition.position, this.damageRadius, this.whatIsGround);
        if (damageHit)
        {
            damageHit.transform.SendMessage("Damage", attackDetails);
            //BulletSpawner.Instance.Despawn(this.transform);
            Destroy(gameObject);
            
        }
        if (groundHit)
        {
            this.hasHitGround = true;
            this.rb.gravityScale = 0f;
            this.rb.velocity = Vector2.zero;
            //BulletSpawner.Instance.Despawn(this.transform);
            Destroy(gameObject);
            
        }
    }
    public virtual void SetShooter(Transform shooter)
    {
        this.shooter = shooter;
    }
    public override void FireProjectTile(float speed, float travelDistance, float damage)
    {
        base.FireProjectTile(speed, travelDistance, damage);
    }
}
