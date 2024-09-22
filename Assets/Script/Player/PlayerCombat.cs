using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //[SerializeField] protected PlayerController playerCtrl;
    //public PlayerController PlayerCtrl => playerCtrl;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Transform attack1HitBoxPos;
    [SerializeField] protected LayerMask whatIsDamageable;
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

    protected virtual void Start()
    {
        this.anim = transform.GetComponentInParent<Animator>();
        this.anim.SetBool("canAttack", this.combatEnabled);
    }
    protected virtual void Update()
    {
        this.CheckCombatInput();
        this.CheckAttack();
    }
    protected virtual void CheckCombatInput()
    {
        if (Input.GetKey("j"))
        {
            Debug.Log("attack");
            //this.combatEnabled = true;
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
    public virtual void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(this.attack1HitBoxPos.position, this.attack1Radius, this.whatIsDamageable);
        this.attackDetails.damageAmount = this.attack1Damage;
        this.attackDetails.position = this.transform.position;
        this.attackDetails.stunDamageAmount = this.stunDamageAmount;

        foreach (Collider2D col in detectedObjects)
        {
            col.transform.parent.SendMessage("Damage", this.attackDetails);
            //Instantiate hit particle
        }
    }
    public virtual void FinishAttack1()
    {
        this.isAttacking = false;
        this.anim.SetBool("isAttacking", this.isAttacking);
        this.anim.SetBool("attack1", false);
    }
    
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.attack1HitBoxPos.position, this.attack1Radius);
    }
}
