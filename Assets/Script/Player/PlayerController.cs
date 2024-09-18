using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] protected PlayerMovement playerMovement;
    public PlayerMovement PlayerMovement => playerMovement;

    [SerializeField] protected PlayerCombat playerCombat;
    public PlayerCombat PlayerCombat => playerCombat;
    protected void Start()
    {
        this.LoadPlayerMovement();
        this.LoadPlayerCombat();
    }
    protected virtual void LoadPlayerMovement()
    {
        if (this.playerMovement != null) return;
        this.playerMovement = transform.GetComponentInChildren<PlayerMovement>();
    }
    protected virtual void LoadPlayerCombat()
    {
        if (this.playerCombat != null) return;
        this.playerCombat = transform.GetComponentInChildren<PlayerCombat>();
    }
}
