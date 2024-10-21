using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    [SerializeField] protected PlayerController player;
    public PlayerController Player => player;
    public int ID;
    public ItemSO itemSO;

    //Khi active = true moi co the lien ket duoc
    //protected virtual void Start()
    //{
    //    this.LoadPlayerController();
    //}

    //Lien ket duoc ngay ca khi active = false
    protected virtual void Awake()
    {
        this.LoadPlayerController();
    }
    protected virtual void LoadPlayerController()
    {
        if (this.player != null) return;
        this.player = FindObjectOfType<PlayerController>();
    }
    public virtual void UseButton() 
    {
    }
}
