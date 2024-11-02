using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeProjectile : Projectile
{
    //[SerializeField] public AxeSpawner spawner;
    [SerializeField] protected DamagePopup instance;
    [SerializeField] protected LayerMask whatIsEnemy;
    [SerializeField] protected GameObject axeHitParticle;

    protected override void Start()
    {
        base.Start();
        this.rb.velocity = this.transform.right * this.speed;
    }
    protected override void FixedUpdate()
    {
        
        if (!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(this.damagePosition.position, this.damageRadius, this.whatIsEnemy);
            Collider2D groundHit = Physics2D.OverlapCircle(this.damagePosition.position, this.damageRadius, this.whatIsGround);

            if (damageHit)
            {
                this.attackDetails.damageAmount = Mathf.Round(Random.Range(15f, 20f));
                bool isCritical = Random.Range(0, 100) < 30;
                if (isCritical) this.attackDetails.damageAmount *= 2;
                damageHit.transform.parent.SendMessage("Damage", attackDetails);
                Destroy(gameObject);
                this.instance.Create(this.attackDetails.position, this.attackDetails.damageAmount, isCritical);
                Instantiate(this.axeHitParticle, this.transform.position, this.transform.rotation);     
                //this.spawner.Despawn(this.gameObject.transform);
               
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

    public override void FireProjectTile(float speed, float travelDistance, float damage)
    {
        base.FireProjectTile(speed, travelDistance, damage);
    }

    protected override void Update()
    {
        base.Update();
        //this.rb.velocity = this.transform.right * this.speed;
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
}
