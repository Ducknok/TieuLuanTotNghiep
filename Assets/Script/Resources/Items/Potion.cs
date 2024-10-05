using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] protected float healthToGive;
    
    protected  void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<PlayerStats>().currentHealth += healthToGive;
            Destroy(gameObject);
        }
    }
}
