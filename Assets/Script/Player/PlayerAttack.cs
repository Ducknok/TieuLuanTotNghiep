using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] protected PlayerController playerController;
    public PlayerController PlayerController => playerController;
    
    protected virtual void LoadDisableFlip()
    {
        PlayerController.PlayerMove.DisableFlip();
    }
    protected virtual void LoadEnableFlip()
    {
        PlayerController.PlayerMove.EnableFlip();
    }
    protected virtual void LoadCheckAttackHitBox()
    {
        PlayerController.PlayerCom.CheckAttackHitBox();
    }
    protected virtual void LoadFinishAttack1()
    {
        PlayerController.PlayerCom.FinishAttack1();
    }
    protected virtual void LoadFinishCastSpell()
    {
        PlayerController.PlayerCom.FinishCastSpell();
    }
}
