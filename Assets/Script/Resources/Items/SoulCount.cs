using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulCount : MonoBehaviour
{
    [SerializeField] protected float cashToGive;
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BankAccount.Instance.Soul(cashToGive);
            AudioManager.Instance.PlayAudio(AudioManager.Instance.gems);
            Destroy(gameObject);
        }
    }
}
