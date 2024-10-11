using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] protected Inventory inventory;
    [SerializeField] protected GameManagerSingleton gameManager;
    [SerializeField] protected GameObject itemToAdd;
    [SerializeField] protected int amountToAdd;
    
    protected virtual void Start()
    {
        this.gameManager = GameManagerSingleton.Instance;
        this.inventory = gameManager.GetComponent<Inventory>();
    }
    protected virtual  void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            //TODO: Add sound effect here
            this.inventory.CheckSlotsAvailability(this.itemToAdd, this.itemToAdd.name, this.amountToAdd);
            //collider.GetComponent<PlayerStats>().currentHealth += healthToGive;
            Destroy(gameObject);
        }
    }
}
