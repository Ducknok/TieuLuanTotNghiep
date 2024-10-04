using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BankAccount : MonoBehaviour
{
    [SerializeField] protected static BankAccount instance;
    public static BankAccount Instance => instance;
    [SerializeField] protected float coinBank;
    [SerializeField] protected float soulBank;
    [SerializeField] protected TextMeshProUGUI coinText;
    [SerializeField] protected TextMeshProUGUI soulText;
    
    protected virtual void Start()
    {
        this.coinText.text = "x " + this.coinBank.ToString();
    }
    protected virtual void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public virtual void Money(float cashCollected)
    {
        this.coinBank += cashCollected;
        this.coinText.text = "x " + this.coinBank.ToString();
    }
    public virtual void Soul(float cashCollected)
    {
        this.soulBank += cashCollected;
        this.soulText.text = "x " + this.soulBank.ToString();
    }
}
