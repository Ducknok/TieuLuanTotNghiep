using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerController
{
    [SerializeField] protected PlayerController playerController;
    public PlayerController PlayerController => playerController;
    protected virtual void LoadDisableFlip()
    {
        PlayerController.PlayerM.DisableFlip();
    }
    protected virtual void LoadEnableFlip()
    {
        PlayerController.PlayerM.EnableFlip();
    }
    protected virtual void LoadCheckAttackHitBox()
    {
        PlayerController.PlayerC.CheckAttackHitBox();
    }
    protected virtual void LoadFinishAttack1()
    {
        PlayerController.PlayerC.FinishAttack1();
    }
    
}
