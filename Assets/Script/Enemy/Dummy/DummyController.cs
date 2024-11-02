using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : DucMonobehavior
{
    [SerializeField] protected PlayerController pc;
    public PlayerController PC => pc;
    protected override void Start()
    {
        this.LoadPlayerController();
    }
    protected virtual void LoadPlayerController()
    {
        if (this.pc != null) return;
        PlayerController playerController = FindObjectOfType<PlayerController>();
        this.pc = playerController;
    }
}
