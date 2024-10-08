using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    [SerializeField] protected PlayerStats player;
    public PlayerStats Player => player;
    public float healthToGive;

    protected virtual void Start()
    {
        this.LoadPlayerController();
    }
    protected virtual void LoadPlayerController()
    {
        if (this.player != null) return;
        this.player = FindObjectOfType<PlayerStats>();
    }
    public virtual void UseButton()
    {
        if(this.gameObject.name == "HealthPotion (Use)")
        {
            this.player.currentHealth += healthToGive;
        }
    }
}
