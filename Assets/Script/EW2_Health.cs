using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EW2_Health : BossHealth
{
    [SerializeField] protected GameObject pbSpawn;
    [SerializeField] protected GameObject pbAbSpawn;
    public override void DecreaseHealth(float amount)
    {
        base.DecreaseHealth(amount);
        Instantiate(this.hitPariticle, this.transform.position, Quaternion.Euler(0f, 0f, 360f));
        this.currentHealth -= amount;
        this.healthImg.fillAmount = currentHealth / 1000;
        Transform damageBar = Instantiate(this.healthCut, this.healthImg.transform);
        damageBar.gameObject.SetActive(true);
        damageBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.healthImg.fillAmount * Bar_Width, damageBar.GetComponent<RectTransform>().anchoredPosition.y);
        damageBar.GetComponent<Image>().fillAmount = amount / 1000;
        Debug.Log(damageBar.GetComponent<Image>().fillAmount);
        damageBar.gameObject.AddComponent<HealthBarCutFallDown>();
    }

    public override void Die()
    {
        this.anim.SetBool("idle", false);
        this.anim.SetBool("rangeAttack1", false);
        this.anim.SetBool("rangeAttack2", false);
        this.anim.SetBool("rangeAttack3", false);
        this.pbSpawn.SetActive(false);
        this.pbAbSpawn.SetActive(false);
        base.Die();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
