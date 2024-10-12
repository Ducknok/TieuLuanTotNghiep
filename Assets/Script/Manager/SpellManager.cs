using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour
{
    //[SerializeField] private PlayerController playerController;
    //public PlayerController PlayerController => playerController;
    [SerializeField] protected GameObject 
        wallJumpOrb,
        dashOrb,
        axeThrowingOrb;
    private void OnEnable()
    {
        if (PlayerController.Instance.PlayerMove.unlockedDash)
        {
            this.dashOrb.SetActive(true);
        }
        else
        {
            this.dashOrb.SetActive(false);
        }
        if (PlayerController.Instance.PlayerCom.unlockedAxeThrowing)
        {
            this.axeThrowingOrb.SetActive(true);
        }
        else
        {
            this.axeThrowingOrb.SetActive(false);
        }
    }
}
