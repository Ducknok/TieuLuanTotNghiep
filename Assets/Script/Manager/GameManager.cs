using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] protected Transform respawnPoint;
    [SerializeField] protected GameObject player;
    [SerializeField] protected float respawnTime;
    [SerializeField] protected float respawnTimeStart;
    [SerializeField] protected bool respawn;
    [SerializeField] protected CinemachineVirtualCamera cvc;

    protected virtual void Start()
    {
        cvc = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
    }
    protected virtual void Update()
    {
        this.CheckRespawn();
    }
    public virtual void Respawn()
    {
        this.respawnTimeStart = Time.time;
        this.respawn = true;
    }
    protected virtual void CheckRespawn()
    {
        if(Time.time >= this.respawnTimeStart + this.respawnTime && this.respawn)
        {
            var playerTemp = Instantiate(this.player, this.respawnPoint);
            cvc.m_Follow = playerTemp.transform;
            playerTemp.transform.parent = null;
            this.respawn = false;
        }
    }
}
