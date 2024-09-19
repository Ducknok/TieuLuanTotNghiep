using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    [SerializeField] protected PlayerController pc;
    public PlayerController PC => pc;
    protected virtual void Start()
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
