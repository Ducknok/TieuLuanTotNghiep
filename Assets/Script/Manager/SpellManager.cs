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
        axeThrowingOrb;
    private void OnEnable()
    {
        Debug.Log("hello");
        Debug.Log(this.playerController.PlayerMove.unlockedDash.ToString());
        Debug.Log(this.playerController.PlayerCom.unlockedAxeThrowing.ToString());

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
    }
}
