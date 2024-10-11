using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Potion
{
    
    [SerializeField] protected float healthToGive;

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
    }

    protected override void Start()
    {
        base.Start();
    }
}
