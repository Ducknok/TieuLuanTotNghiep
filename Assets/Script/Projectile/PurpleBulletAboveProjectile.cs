using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBulletAboveProjectile : Projectile
{
    public DamagePopup instance;
    public PurpleBulletAboveSpawner spawner;
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected GameObject hitParticle;
    protected override void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }
    

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Collider2D damageHit = Physics2D.OverlapCircle(this.damagePosition.position, this.damageRadius, this.whatIsPlayer);
        this.attackDetails.damageAmount = Random.Range(50f, 100f);
        bool isCritical = Random.Range(0, 100) < 30;
        if (isCritical) this.attackDetails.damageAmount *= 2;
        if (damageHit)
        {
                damageHit.transform.SendMessage("Damage", attackDetails);
                this.instance.Create(this.damagePosition.position, this.attackDetails.damageAmount, isCritical);
                Instantiate(this.hitParticle, this.transform.position, this.transform.rotation);
                this.spawner.Despawn(this.gameObject.transform);
        }
        else
        {
            StartCoroutine(DestroyBullet());
        }
    }
    public override void FireProjectTile(float speed, float travelDistance, float damage)
    {
        base.FireProjectTile(speed, travelDistance, damage);
    }
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(5f);
        this.spawner.Despawn(this.gameObject.transform);
    }


}
