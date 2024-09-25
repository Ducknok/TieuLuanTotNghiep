using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoins : MonoBehaviour
{
    [SerializeField] protected float cashToGive;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BankAccount.Instance.Money(cashToGive);
            Destroy(gameObject);
        }
    }
}
