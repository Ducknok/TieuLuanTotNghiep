using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUse : ItemUse
{
    public float healthToGive;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void LoadPlayerController()
    {
        base.LoadPlayerController();
    }
    public override void UseButton()
    {
        base.UseButton();
        if (this.itemSO.itemName == "HealthPotion (Use)")
        {
            this.player.PlayerSta.currentHealth += healthToGive;
        }
    }
}
