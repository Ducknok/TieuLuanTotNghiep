using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : Projectile
{
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected GameObject target;
    protected override void Start()
    {
        base.Start();
        this.target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (this.target.transform.position - transform.position).normalized * speed;
        this.rb.velocity = new Vector2(moveDir.x, moveDir.y); 
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
            Destroy(gameObject);
        }
    }



    //protected virtual void UpdateProjectilePosition()
    //{
    //    Vector3 trajectoryRange = this.target.transform.position - this.trajectoryStartPoint;
    //    float nextPositionX = this.transform.position.x + this.speed * Time.deltaTime;
    //    float nextPositionXNormalized = (nextPositionX - this.trajectoryStartPoint.x) / trajectoryRange.x;
    //    float nextPositionYNormalized = this.trajectoryAnimationCurve.Evaluate(nextPositionXNormalized);
    //    float nextPositionY = this.trajectoryStartPoint.y  + nextPositionYNormalized * trajectoryMaxHeight;
    //    Vector3 newPosition = new Vector3(nextPositionX, nextPositionY, 0);
    //    this.transform.position = newPosition;
    //}
    //public void InitializeProjectile(GameObject target, float trajectoryMaxHeight)
    //{
    //    this.target = target;
    //    this.trajectoryMaxHeight = trajectoryMaxHeight;
    //}
    //public void InitializeAnimationCurves(AnimationCurve trajectoryAnimationCurve)
    //{
    //    this.trajectoryAnimationCurve = trajectoryAnimationCurve;
    //}
}
