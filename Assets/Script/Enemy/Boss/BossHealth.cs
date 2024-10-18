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
        Instantiate(this.hitPariticle, this.transform.position, Quaternion.Euler(0f, 0f, 360f));
        this.currentHealth -= amount;
        this.healthImg.fillAmount = currentHealth / 100;
        Transform damageBar = Instantiate(this.healthCut, this.healthImg.transform);
        damageBar.gameObject.SetActive(true);
        damageBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.healthImg.fillAmount * Bar_Width, damageBar.GetComponent<RectTransform>().anchoredPosition.y);
        damageBar.GetComponent<Image>().fillAmount = amount/100;
        Debug.Log(damageBar.GetComponent<Image>().fillAmount);
        damageBar.gameObject.AddComponent<HealthBarCutFallDown>();
    }

    public virtual void Die()
    {
        Instantiate(this.deathChunkParticle, this.transform.position, this.deathChunkParticle.transform.rotation);
        Instantiate(this.deathBloodParticle, this.transform.position, this.deathBloodParticle.transform.rotation);
        Destroy(gameObject);
    }
}
