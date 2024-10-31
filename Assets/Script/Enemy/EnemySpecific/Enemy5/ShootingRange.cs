using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRange : MonoBehaviour
{
    protected AttackDetails attackDetails;
    [SerializeField] protected Transform player;
    [SerializeField] protected Transform rangeAttackPosition;
    [SerializeField] protected Animator anim;
    [SerializeField] protected float shootingRange;
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected float travelDistance;

    protected virtual void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        float distanceFromPlayer = Vector2.Distance(this.player.position, this.transform.position);
        if (distanceFromPlayer <= shootingRange)
        {
            anim.SetBool("rangeAttack", true);
        }
        else
        {
            anim.SetBool("rangeAttack", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, shootingRange);
    }
    public virtual void TriggerAttack()
    {  
        Transform newBullet = BulletSpawner.Instance.Spawn(BulletSpawner.bullet, this.rangeAttackPosition.position, this.rangeAttackPosition.rotation);
        newBullet.gameObject.SetActive(true);
        BulletProjectile projectileScript = newBullet.GetComponent<BulletProjectile>();
        projectileScript.FireProjectTile(this.speed, this.travelDistance, this.attackDetails.damageAmount);
        projectileScript.SetShooter(this.rangeAttackPosition);
    }
}
