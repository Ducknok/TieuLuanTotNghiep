using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoins : MonoBehaviour
{
    
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected CircleCollider2D Col;
    [SerializeField] protected float cashToGive;
    [SerializeField] protected float force;

    protected virtual void Start()
    {
        this.rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Wooden"))
        {
            this.Col.isTrigger = false;
            this.rb.velocity = Vector2.zero;
        }
        if (collision.CompareTag("Player"))
        {
            BankAccount.Instance.Money(cashToGive);
            AudioManager.Instance.PlayAudio(AudioManager.Instance.gems);
            Destroy(gameObject);
        }
    }
    
}
