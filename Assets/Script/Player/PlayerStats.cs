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
    [SerializeField] protected GameObject hitPariticle;
    [SerializeField] public float maxHealth;
    [SerializeField] public float currentHealth;
    protected virtual void Start()
    {
        this.playerCtrl = this.transform.GetComponent<PlayerController>();
        this.currentHealth = this.maxHealth;
        //this.healthImg.fillAmount = currentHealth / 100;
    }
    //protected virtual void Update()
    //{
    //    Debug.Log(this.transform.position);
    //}
    public virtual void DecreaseHealth(float amount)
    {
        this.currentHealth -= amount;
        this.healthImg.fillAmount = currentHealth / 100;
        Instantiate(this.hitPariticle, this.transform.position, Quaternion.Euler(0f, 0f, 360f));
        if(this.currentHealth <= 0.0f)
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
