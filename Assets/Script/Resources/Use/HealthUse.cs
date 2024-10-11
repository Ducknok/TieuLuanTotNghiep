using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUse : ItemUse
{
    public float healthToGive;

    protected override void Start()
    {
        base.Start();
    }
    protected override void LoadPlayerController()
    {
        base.LoadPlayerController();
    }
    public override void UseButton()
    {
        base.UseButton();
        if (this.gameObject.name == "HealthPotion (Use)")
        {
            this.player.currentHealth += healthToGive;
        }
    }

    
}
