using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCombat : DummyController
{
    [SerializeField] protected GameObject aliveGO, brokenTopGO, brokenBotGO;
    [SerializeField] protected Rigidbody2D rbAlive, rbBrokenTop, rbBrokenBot;
    [SerializeField] protected Animator aliveAnim;
    [SerializeField] protected GameObject hitParticle;

    [Header("Player")]
    [SerializeField] protected int playerFacingDirection;
    [SerializeField] protected bool playerOnLeft;

    [Header("Dummy")]
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float knockbackDuration;
    [SerializeField] protected float knockbackStart;
    [SerializeField] protected float knockbackSpeedX, knockbackSpeedY;
    [SerializeField] protected float knockbackDeathSpeedX, knockbackDeathSpeedY, deathTorque;
    [SerializeField] protected bool applyKnockBack;
    [SerializeField] protected bool knockback;

    protected override void Start()
    {
        this.currentHealth = this.maxHealth;

        this.aliveGO = transform.Find("Alive").gameObject;
        this.brokenTopGO = transform.Find("Broken Top").gameObject;
        this.brokenBotGO = transform.Find("Broken Bottom").gameObject;

        this.aliveAnim = this.aliveGO.GetComponentInChildren<Animator>();
        this.rbAlive = this.aliveGO.GetComponentInChildren<Rigidbody2D>();
        this.rbBrokenTop = this.rbBrokenTop.GetComponentInChildren<Rigidbody2D>();
        this.rbBrokenBot = this.rbBrokenBot.GetComponentInChildren<Rigidbody2D>();

        this.aliveGO.SetActive(true);
        this.brokenTopGO.SetActive(false);
        this.brokenBotGO.SetActive(false);
    }
    protected virtual void Update()
    {
        this.CheckKnockback();
    }
    protected virtual void Damage(AttackDetails attackDetails)
    {
        this.currentHealth -= attackDetails.damageAmount;

        if (attackDetails.position.x < this.aliveGO.transform.position.x)
        {
            this.playerFacingDirection = 1;
        }
        else
        {
            this.playerFacingDirection = -1;
        }

        Instantiate(this.hitParticle, this.aliveAnim.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        if (this.playerFacingDirection == 1)
        {
            this.playerOnLeft = true;
        }
        else
        {
            this.playerOnLeft = false;
        }
        this.aliveAnim.SetBool("playerOnLeft", this.playerOnLeft);
        this.aliveAnim.SetTrigger("damage");

        if (this.applyKnockBack && this.currentHealth > 0.0f)
        {
            //Knock back
            this.Knockback();
        }
        if (this.currentHealth <= 0.0f)
        {
            //Die
            this.Die();
        }
    }
    protected virtual void Knockback()
    {
        this.knockback = true;
        this.knockbackStart = Time.time;
        this.rbAlive.velocity = new Vector2(this.knockbackSpeedX * this.playerFacingDirection, this.knockbackSpeedY);
    }
    protected virtual void CheckKnockback()
    {
        if (Time.time >= this.knockbackStart + this.knockbackDuration && this.knockback)
        {
            this.knockback = false;
            this.rbAlive.velocity = new Vector2(0.0f, this.rbAlive.velocity.y);
        }
    }

    protected virtual void Die()
    {
        this.aliveGO.SetActive(false);
        this.brokenTopGO.SetActive(true);
        this.brokenBotGO.SetActive(true);

        this.brokenTopGO.transform.position = this.aliveGO.transform.position;
        this.brokenBotGO.transform.position = this.aliveGO.transform.position;

        this.rbBrokenBot.velocity = new Vector2(this.knockbackSpeedX * this.playerFacingDirection, this.knockbackSpeedY);
        this.rbBrokenTop.velocity = new Vector2(this.knockbackDeathSpeedX * this.playerFacingDirection, this.knockbackDeathSpeedY);
        this.rbBrokenTop.AddTorque(this.deathTorque * -this.playerFacingDirection, ForceMode2D.Impulse);
    }

}
