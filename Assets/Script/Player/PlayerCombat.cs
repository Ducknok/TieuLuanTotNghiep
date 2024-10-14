using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] protected PlayerController playerCtrl;
    public PlayerController PlayerCtrl => playerCtrl;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Transform attack1HitBoxPos;
    
    //[SerializeField] protected GameObject damageText;
    [SerializeField] protected LayerMask whatIsDamageable;
    [Header("Melee Attack")]
    [SerializeField] protected float inputTimer;
    [SerializeField] protected float attack1Radius;
    [SerializeField] protected float attack1Damage;
    [SerializeField] protected float lastInputTime = Mathf.NegativeInfinity;
    [SerializeField] protected float stunDamageAmount = 1f;
    [SerializeField] public AttackDetails attackDetails;
    [SerializeField] protected bool combatEnabled;
    [SerializeField] protected bool gotInput;
    [SerializeField] protected bool isAttacking;
    [SerializeField] protected bool isFirstAttack;
    [Header("Cast Spell")]
    [SerializeField] protected Transform castSpellPosition;
    [SerializeField] protected AxeProjectile projectile;
    [SerializeField] protected float manaSpellCost = 10f;
    [SerializeField] protected float timeBetweenCast = 0.5f;
    [SerializeField] protected float timeSinceCast;
    [SerializeField] protected float projectileDamage;
    [SerializeField] protected float projectileSpeed;
    [SerializeField] protected float projectileTravelDistance;
    [Header("Unlock Spell")]
    [SerializeField] public bool unlockedAxeThrowing;
    

    protected virtual void Start()
    {
        this.anim = transform.GetComponentInParent<Animator>();
        this.anim.SetBool("canAttack", this.combatEnabled);
        //this.unlockedAxeThrowing = false;
        this.LoadUnlockAxeThrowing();
    }
    protected virtual void Update()
    {
        this.CheckCombatInput();
        this.CheckAttack();
        this.CastSpell();
    }
    protected virtual void CheckCombatInput()
    {
        if (Input.GetKey("j"))
        {
            if (this.combatEnabled)
            {
                //Attemp combat
                
                this.gotInput = true;
                this.lastInputTime = Time.time;
            }
        }
    }
    protected virtual void CheckAttack()
    {
        if (this.gotInput)
        {
            //Perform attack1
            if (!isAttacking)
            {
                this.gotInput = false;
                this.isAttacking = true;
                this.isFirstAttack = !isFirstAttack;
               
                this.anim.SetBool("attack1", true);
                this.anim.SetBool("firstAttack", isFirstAttack);
                this.anim.SetBool("isAttacking", isAttacking);
                
            }
        }
        if (Time.time >= this.lastInputTime + this.inputTimer)
        {
            //wait for new input
            this.gotInput = false;
        }
    }
    protected virtual void CastSpell()
    {

        if (this.unlockedAxeThrowing)
        {
            if (Input.GetButton("CastSpell") && this.timeSinceCast >= this.timeBetweenCast && this.playerCtrl.PlayerSta.currentMana >= this.manaSpellCost)
            {
                //Debug.Log("riu ne ne di");
                //StartCoroutine(CastCourotine());
                this.anim.SetBool("isCasting", true);
                this.projectileDamage = Mathf.Round(Random.Range(15f, 20f));
                Transform newaxe = AxeSpawner.Instance.Spawn(AxeSpawner.axe, this.castSpellPosition.position, this.castSpellPosition.rotation);
                newaxe.gameObject.SetActive(true);
                this.projectile = newaxe.GetComponent<AxeProjectile>();
                this.projectile.FireProjectTile(this.projectileSpeed, this.projectileTravelDistance, this.projectileDamage);
                this.playerCtrl.PlayerSta.DecreaseMana(manaSpellCost);
                this.anim.SetBool("isCasting", false);
                timeSinceCast = 0;
            }
            else
            {
                timeSinceCast += Time.deltaTime;
            }
        }
        
    }
    public virtual void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(this.attack1HitBoxPos.position, this.attack1Radius, this.whatIsDamageable);
        this.attack1Damage = Mathf.Round(Random.Range(20f, 30f));
        this.attackDetails.damageAmount = this.attack1Damage;
        this.attackDetails.position = this.transform.position;
        this.attackDetails.stunDamageAmount = this.stunDamageAmount;

        foreach (Collider2D col in detectedObjects)
        {
            AudioManager.Instance.PlayAudio(AudioManager.Instance.attack);
            col.transform.parent.SendMessage("Damage", this.attackDetails);
            
        }
    }
    public virtual void FinishAttack1()
    {
        this.isAttacking = false;
        this.anim.SetBool("isAttacking", this.isAttacking);
        this.anim.SetBool("attack1", false);
    }
    public virtual void FinishCastSpell()
    {
        this.anim.SetBool("isCasting", false);
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.attack1HitBoxPos.position, this.attack1Radius);
    }
    protected virtual void LoadUnlockAxeThrowing()
    {
        this.unlockedAxeThrowing = GameData.Instance.saveData.playerUnlockAxeThrowing;
    }
}
