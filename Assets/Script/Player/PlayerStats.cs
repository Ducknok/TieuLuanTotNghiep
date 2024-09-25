using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] protected PlayerController playerCtrl;
    public PlayerController PlayerCtrl => playerCtrl;
    [SerializeField] protected GameObject
        deathChunkParticle,
        deathBloodParticle;
    [SerializeField] protected Image healthImg;
    [SerializeField] public float maxHealth;
    [SerializeField] public float currentHealth;
    protected virtual void Start()
    {
        
        this.playerCtrl = this.transform.GetComponent<PlayerController>();
        this.currentHealth = this.maxHealth;
    }
    protected virtual void Update()
    {
        this.healthImg.fillAmount = currentHealth / 100;
    }
    public virtual void DecreaseHealth(float amount)
    {
        this.currentHealth -= amount;
        
        if (this.currentHealth <= 0.0f)
        {
            this.Die();
        }
    }
    public virtual void Die()
    {
        Instantiate(this.deathChunkParticle, this.transform.position, this.deathChunkParticle.transform.rotation);
        Instantiate(this.deathBloodParticle, this.transform.position, this.deathBloodParticle.transform.rotation);
        this.playerCtrl.GameManager.Respawn();
        Destroy(gameObject);
    }


}
