using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapHealth : MonoBehaviour
{
    [SerializeField]
    protected GameObject
        deathChunkParticle,
        deathBloodParticle;
    [SerializeField] protected GameObject hitPariticle;
    [SerializeField] public float maxHealth;
    [SerializeField] public float currentHealth;
    protected virtual void Start()
    {
        this.currentHealth = this.maxHealth;
    }
    protected virtual void Update()
    {
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
