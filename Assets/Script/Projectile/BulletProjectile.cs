using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : Projectile
{
    [SerializeField] protected BulletSpawner spawner;
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected GameObject target;
    [SerializeField] protected Transform shooter;

    public Transform Shooter => shooter;
    

    protected override void Start()
    {
        base.Start();
        this.target = GameObject.FindGameObjectWithTag("Player");
    }
    protected override void Update()
    {
        base.Update();
        this.transform.position = Vector2.MoveTowards(this.transform.position, this.target.transform.position, speed * Time.deltaTime);
        this.RotateTowardsPlayer(this.gameObject, target.transform);
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
            this.spawner.Despawn(this.gameObject.transform);
            //Destroy(gameObject);
            
        }
        if (groundHit)
        {
            this.hasHitGround = true;
            this.rb.gravityScale = 0f;
            this.rb.velocity = Vector2.zero;
            this.spawner.Despawn(this.gameObject.transform);
            
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
    public void RotateTowardsPlayer(GameObject spawnedObject, Transform playerTransform)
    {
        // Calculate the direction from the spawned object to the player
        Vector2 direction = (playerTransform.position - spawnedObject.transform.position).normalized;

        // Calculate the angle in degrees from the direction vector
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the spawned object to face the player
        spawnedObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    
}
