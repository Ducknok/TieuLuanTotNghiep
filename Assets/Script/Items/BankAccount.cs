using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BankAccount : MonoBehaviour
{
    [SerializeField] protected static BankAccount instance;
    public static BankAccount Instance => instance;
    [SerializeField] protected float bank;
    [SerializeField] protected Text bankText;
    
    protected virtual void Start()
    {
        this.bankText.text = "x " + this.bank.ToString();
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
        this.bank += cashCollected;
        this.bankText.text = "x " + this.bank.ToString();
    }

}
