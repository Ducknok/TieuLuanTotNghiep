using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerController
{
    [SerializeField] protected PlayerController playerController;
    public PlayerController PlayerController => playerController;
    protected virtual void LoadDisableFlip()
    {
        PlayerController.PlayerMovement.DisableFlip();
    }
    protected virtual void LoadEnableFlip()
    {
        PlayerController.PlayerMovement.EnableFlip();
    }
    protected virtual void LoadCheckAttackHitBox()
    {
        PlayerController.PlayerCombat.CheckAttackHitBox();
    }
    protected virtual void LoadFinishAttack1()
    {
        PlayerController.PlayerCombat.FinishAttack1();
    }
    
}
