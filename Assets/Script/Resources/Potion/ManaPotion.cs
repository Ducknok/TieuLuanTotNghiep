using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : Potion
{
    
    [SerializeField] protected float manaToGive;

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
    }

    protected override void Start()
    {
        base.Start();
    }
}
