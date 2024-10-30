using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    public PlayerController PlayerController => playerController;
    [SerializeField] protected GameObject 
        wallJumpOrb,
        dashOrb,
        axeThrowingOrb,
        shieldOrb;
    private void OnEnable()
    {

        this.wallJumpOrb.SetActive(true);
        if (this.playerController.PlayerMove.unlockedDash)
        {
            this.dashOrb.SetActive(true);
        }
        else
        {
            this.dashOrb.SetActive(false);
        }
        if (this.playerController.PlayerCom.unlockedAxeThrowing)
        {
            this.axeThrowingOrb.SetActive(true);
        }
        else
        {
            this.axeThrowingOrb.SetActive(false);
        }
        if (this.playerController.PlayerCom.unlockedShield)
        {
            this.shieldOrb.SetActive(true);
        }
        else
        {
            this.shieldOrb.SetActive(false);
        }
    }
}
