using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] protected PlayerMovement playerM;
    public PlayerMovement PlayerM => playerM;

    [SerializeField] protected PlayerCombat playerC;
    public PlayerCombat PlayerC => playerC;
    protected void Start()
    {
        LoadPlayerMovement();
        LoadPlayerCombat();
    }
    protected virtual void LoadPlayerMovement()
    {
        if (playerM != null) return;
        playerM = transform.GetComponentInChildren<PlayerMovement>();
    }
    protected virtual void LoadPlayerCombat()
    {
        if (playerC != null) return;
        playerC = transform.GetComponentInChildren<PlayerCombat>();
    }
}
