using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : Projectile
{
    [SerializeField] protected AnimationCurve trajectoryAnimationCurve;
    public override void FireProjectTile(float speed, float travelDistance, float damage)
    {
        base.FireProjectTile(speed, travelDistance, damage);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
    protected void InitializeAnimationCurves(AnimationCurve trajectoryAnimationCurve)
    {
        this.trajectoryAnimationCurve = trajectoryAnimationCurve;
    }
}
