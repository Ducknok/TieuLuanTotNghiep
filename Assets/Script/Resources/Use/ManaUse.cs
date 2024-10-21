using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaUse : ItemUse
{
    public float manaToGive;

    protected override void LoadPlayerController()
    {
        base.LoadPlayerController();
    }

    protected override void Awake()
    {
        base.Awake();
    }
    public override void UseButton()
    {
        base.UseButton();
            if (this.itemSO.itemName == "ManaPotion (Use)")
            {
                this.player.PlayerSta.currentMana += manaToGive;
            }
        
    }


}
