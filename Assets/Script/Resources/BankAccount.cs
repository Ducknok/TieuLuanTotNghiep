using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BankAccount : MonoBehaviour
{
    [SerializeField] protected static BankAccount instance;
    public static BankAccount Instance => instance;
    [SerializeField] public float coinBank;
    [SerializeField] public float soulBank;
    [SerializeField] protected TextMeshProUGUI coinText;
    [SerializeField] protected TextMeshProUGUI soulText;
    
    protected virtual void Start()
    {
        this.coinBank = PlayerPrefs.GetFloat("Gold", 0f);
        this.soulBank = PlayerPrefs.GetFloat("Soul", 0f);
        this.coinText.text = "x " + this.coinBank.ToString();
        this.soulText.text = "x " + this.soulBank.ToString();
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
        DataManager.Instance.GoldDAta(coinBank);
        this.coinText.text = "x " + this.coinBank.ToString();
        this.coinBank = PlayerPrefs.GetFloat("Gold");
    }
    public virtual void Soul(float cashCollected)
    {
        this.soulBank += cashCollected;
        DataManager.Instance.SoulData(soulBank);
        this.soulText.text = "x " + this.soulBank.ToString();
        this.soulBank = PlayerPrefs.GetFloat("Soul");
    }
}
