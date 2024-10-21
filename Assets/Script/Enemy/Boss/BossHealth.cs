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
    [SerializeField] protected GameObject hitPariticle;
    [SerializeField] protected Transform healthCut;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Image healthImg;
    [SerializeField] protected GameObject healPanel;
    [SerializeField] public float maxHealth;
    [SerializeField] public float currentHealth;
    [SerializeField] protected const float Bar_Width = 1500f;
    protected virtual void Start()
    {
        this.anim = transform.GetComponentInChildren<Animator>();
        //this.healthCut = transform.Find("HealthCut");
        this.currentHealth = this.maxHealth;
        this.healthImg.fillAmount = this.currentHealth / 100;
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
        
    }

    public virtual void Die()
    {
        this.healPanel.SetActive(false);
        this.anim.SetTrigger("dead");
        StartCoroutine(DestroyObject());
    }
    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1.8f);
        Instantiate(this.deathChunkParticle, this.transform.position, this.transform.rotation);
        Instantiate(this.deathBloodParticle, this.transform.position, this.transform.rotation);
        Destroy(gameObject);
    }
}
