using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBulletProjectile : Projectile
{
    public PurpleBulletSpawner spawner;
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected GameObject target;
    [SerializeField] protected GameObject hitParticle;
    protected override void Start()
    {
        base.Start();
        this.target = GameObject.FindGameObjectWithTag("Player");
        
        
    }
    public override void FireProjectTile(float speed, float travelDistance, float damage)
    {
        base.FireProjectTile(speed, travelDistance, damage);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Collider2D damageHit = Physics2D.OverlapCircle(this.damagePosition.position, this.damageRadius, this.whatIsPlayer);
        if (damageHit)
        {
            damageHit.transform.SendMessage("Damage", attackDetails);
            Instantiate(this.hitParticle, this.transform.position, this.transform.rotation);
            this.spawner.Despawn(this.gameObject.transform);
        }
        else
        {
            StartCoroutine(DestroyBullet());
        }
        
    }

    protected override void Update()
    {
        base.Update();
        this.transform.position = Vector2.MoveTowards(this.transform.position, this.target.transform.position, speed * Time.deltaTime);
        this.RotateTowardsPlayer(this.gameObject, target.transform);
    }
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(2f);
        this.spawner.Despawn(this.gameObject.transform);
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
