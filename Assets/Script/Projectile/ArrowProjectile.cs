using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : Projectile
{

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
                damageHit.transform.SendMessage("Damage", attackDetails);
                Destroy(gameObject);
            }
            if (groundHit)
            {
                this.hasHitGround = true;
                this.rb.gravityScale = 0f;
                this.rb.velocity = Vector2.zero;
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
