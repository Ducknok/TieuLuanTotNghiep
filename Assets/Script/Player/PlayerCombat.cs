using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCombat : MonoBehaviour
{
    //[SerializeField] protected PlayerController playerCtrl;
    //public PlayerController PlayerCtrl => playerCtrl;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Transform attack1HitBoxPos;
    //[SerializeField] protected GameObject damageText;
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
    
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.attack1HitBoxPos.position, this.attack1Radius);
    }
    //public virtual float DamageInput(float enemyDefense, Transform hit)
    //{
    //    float totalAttack = this.attack1Damage + (100 / (100 + enemyDefense));
    //    float finalAttack = Mathf.Round(Random.Range(totalAttack - 10, totalAttack + 10));
    //    if(finalAttack > totalAttack + 4)
    //    {
    //        finalAttack *= 2;
    //        print("Critical");
    //    }
    //    if(finalAttack < 0)
    //    {
    //        finalAttack = 0;
    //        print("Attack blocked");
    //    }
    //    GameObject textGo = Instantiate(damageText, hit.transform.position, Quaternion.identity);
    //    textGo.GetComponent<TextMeshPro>().SetText(finalAttack.ToString());
    //    print(finalAttack);
    //    return finalAttack;
    //}
}
