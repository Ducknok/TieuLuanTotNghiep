using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private static PlayerController instance;
    public static PlayerController Instance => instance;
    [SerializeField] protected PlayerMovement playerMove;
    public PlayerMovement PlayerMove => playerMove;

    [SerializeField] protected PlayerCombat playerCom;
    public PlayerCombat PlayerCom => playerCom;

    [SerializeField] protected PlayerStats playerSta;
    public PlayerStats PlayerSta => playerSta;

    [SerializeField] protected GameManager gameManager;
    public GameManager GameManager => gameManager;
    protected void Start()
    {
        this.LoadPlayerMovement();
        this.LoadPlayerCombat();
        this.LoadPlayerStats();
        this.LoadGameManager();
    }
    protected virtual void LoadPlayerMovement()
    {
        if (this.playerMove != null) return;
        this.playerMove = this.transform.GetComponentInChildren<PlayerMovement>();
    }
    protected virtual void LoadPlayerCombat()
    {
        if (this.playerCom != null) return;
        this.playerCom = this.transform.GetComponentInChildren<PlayerCombat>();
    }
    protected virtual void LoadPlayerStats()
    {
        if (this.playerSta != null) return;
        this.playerSta = this.transform.GetComponent<PlayerStats>();
    }
    protected virtual void LoadGameManager()
    {
        if (this.gameManager != null) return;
        this.gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}
