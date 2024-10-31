using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : Projectile
{
    public DamagePopup instance;
    [SerializeField] protected LayerMask whatIsPlayer;
    protected override void Start()
    {
        base.Start();
        this.rb.velocity = this.transform.right * this.speed;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        
        if (!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(this.damagePosition.position, this.damageRadius, this.whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(this.damagePosition.position, this.damageRadius, this.whatIsGround);

            if (damageHit)
            {
                bool isCritical = Random.Range(0, 100) < 30;
                if (isCritical) this.attackDetails.damageAmount *= 2;
                damageHit.transform.SendMessage("Damage", attackDetails);
                this.instance.Create(this.damagePosition.position, this.attackDetails.damageAmount, isCritical);
                Destroy(gameObject);
            }
            if (groundHit)
            {
                this.hasHitGround = true;
                this.rb.gravityScale = 0f;
                this.rb.velocity = Vector2.zero;
                //this.spawner.Despawn(this.gameObject.transform);
                Destroy(gameObject);
            }
            if (Mathf.Abs(this.xStartPos - this.transform.position.x) >= this.travelDistance && !this.isGravityOn)
            {
                this.isGravityOn = true;
                this.rb.gravityScale = this.gravity;
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        
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
    public override void FireProjectTile(float speed, float travelDistance, float damage)
    {
        base.FireProjectTile(speed, travelDistance, damage);
    }
}
