using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField]
    protected GameObject
        deathChunkParticle,
        deathBloodParticle;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Image healthImg;
    [SerializeField] protected GameObject hitPariticle;
    [SerializeField] public float maxHealth;
    [SerializeField] public float currentHealth;
    protected virtual void Start()
    {
        this.anim = transform.GetComponentInChildren<Animator>();
        this.currentHealth = this.maxHealth;
        this.healthImg.fillAmount = currentHealth / 100;
    }
    protected virtual void Update()
    {
        this.healthImg.fillAmount = currentHealth / 100;
        if (this.currentHealth <= 0.0f)
        {
            this.Die();
        }
    }
    public virtual void DecreaseHealth(float amount)
    {
        this.currentHealth -= amount;
        
        Instantiate(this.hitPariticle, this.transform.position, Quaternion.Euler(0f, 0f, 360f));
    }

    public virtual void Die()
    {
        Instantiate(this.deathChunkParticle, this.transform.position, this.deathChunkParticle.transform.rotation);
        Instantiate(this.deathBloodParticle, this.transform.position, this.deathBloodParticle.transform.rotation);
        Destroy(gameObject);
    }
}
