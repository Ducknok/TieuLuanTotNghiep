using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum State
    {
        Moving,
        Knockback,
        Dead
    }

    private State currentState;

    [SerializeField] protected GameObject alive;
    [SerializeField]
    protected GameObject
        hitParticle,
        deathChunkParticle,
        deathBloodParticle,
        deathBoneParticle;
    [SerializeField] protected Rigidbody2D aliveRb;
    [SerializeField] protected Transform 
        groundCheck, 
        wallCheck,
        touchDamageCheck;
    [SerializeField] protected Animator aliveAnim;
    [SerializeField] protected LayerMask 
        whatIsGround,
        whatIsPlayer;
    [SerializeField]
    protected float
        groundCheckDistance,
        wallCheckDistance,
        movementSpeed,
        maxHealth,
        currentHealth,
        knockbackDuration,
        knockbackStartTime,
        lastTouchDamageTime,
        touchDamageCooldown,
        touchDamage,
        touchDamageWidth,
        touchDamageHeight;
    [SerializeField] protected float[] attackDetails = new float[2];
    [SerializeField] protected int 
        facingDirection,
        damageDirection;
    [SerializeField] protected Vector2 
        movement,
        touchDamageBotLeft,
        touchDamageTopRight;
    [SerializeField] protected Vector2 knockbackSpeed;

    [SerializeField] protected bool 
        groundDetected, 
        wallDetected;

    protected virtual void Start()
    {
        this.alive = this.transform.Find("Alive").gameObject;
        this.aliveRb = this.aliveRb.GetComponent<Rigidbody2D>();
        this.aliveAnim = this.aliveAnim.GetComponent<Animator>();
        //this.whatIsPlayer = this.transform.parent.Find("Player").GetComponent<LayerMask>();
        this.currentHealth = this.maxHealth;
        this.facingDirection = 1;
        
    }
    protected virtual void Update()
    {
        //Debug.Log("Check: " + this.alive.transform.position.x);
        switch (this.currentState)
        {
            case State.Moving:
                this.UpdateMovingState();
                break;
            case State.Knockback:
                this.UpdateKnockbackState();
                break;
            case State.Dead:
                this.UpdateDeadState();
                break;
        }
    }

    //--Moving state---------------------
    private void EnterMovingState()
    {

    }
    private void UpdateMovingState()
    {
        this.groundDetected = Physics2D.Raycast(this.groundCheck.position, Vector2.down, this.groundCheckDistance, this.whatIsGround);
        this.wallDetected = Physics2D.Raycast(this.wallCheck.position, this.transform.right, this.wallCheckDistance, this.whatIsGround);

        this.CheckTouchDamage();
        if(!this.groundDetected || this.wallDetected)
        {
            //Flip
            this.Flip();
        }
        else
        {
            //Move
            this.movement.Set(this.movementSpeed * this.facingDirection, this.aliveRb.velocity.y);
            this.aliveRb.velocity = this.movement;
        }
    }
    private void ExitMovingState()
    {

    }
    //--Knockback state---------------------
    private void EnterKnockbackState()
    {
        this.knockbackStartTime = Time.time;
        this.movement.Set(knockbackSpeed.x * this.damageDirection, this.knockbackSpeed.y);
        this.aliveRb.velocity = this.movement;
        this.aliveAnim.SetBool("knockback", true);
    }
    private void UpdateKnockbackState()
    {
        if(Time.time >= this.knockbackStartTime + this.knockbackDuration )
        {
            this.SwitchState(State.Moving);
        }
    }
    private void ExitKnockbackState()
    {
        this.aliveAnim.SetBool("knockback", false);
    }
    //--Dead state---------------------
    private void EnterDeadState()
    {
        //Spawn chunks and blood
        Instantiate(this.deathBoneParticle, this.alive.transform.position, this.deathBoneParticle.transform.rotation);
        Instantiate(this.deathChunkParticle, this.alive.transform.position, this.deathChunkParticle.transform.rotation);
        Instantiate(this.deathBoneParticle, this.alive.transform.position, this.deathBoneParticle.transform.rotation);
        Destroy(this.gameObject);
    }
    private void UpdateDeadState()
    {
        
    }
    private void ExitDeadState()
    {

    }

    //Other function
    protected virtual void Damage(float[] attackDetails)
    {
        //Debug.Log("attackdetail nhan của player: " + attackDetails[0]);
        //Debug.Log("attackdetail nhan của player: " + attackDetails[1]);
        this.currentHealth -= attackDetails[0];

        Instantiate(this.hitParticle, this.alive.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if(attackDetails[1] > this.alive.transform.position.x)
        {
            this.damageDirection = -1;
        }
        else
        {
            this.damageDirection = 1;
        }
        //Hit particle
        if(this.currentHealth > 0.0f)
        {
            this.SwitchState(State.Knockback);
        }
        else if(this.currentHealth <= 0.0f)
        {
            this.SwitchState(State.Dead);
        }
    }

    protected virtual void CheckTouchDamage()
    {
        if (Time.time >= this.lastTouchDamageTime + this.touchDamageCooldown)
        {
            this.touchDamageBotLeft.Set(this.touchDamageCheck.position.x - (this.touchDamageWidth / 2), this.touchDamageCheck.position.y - (this.touchDamageHeight / 2));
            this.touchDamageTopRight.Set(this.touchDamageCheck.position.x + (this.touchDamageWidth / 2), this.touchDamageCheck.position.y + (this.touchDamageHeight / 2));

            Collider2D hit = Physics2D.OverlapArea(this.touchDamageBotLeft, this.touchDamageTopRight, this.whatIsPlayer);
            if (hit != null)
            {
                this.lastTouchDamageTime = Time.time;
                this.attackDetails[0] = this.touchDamage;
                this.attackDetails[1] = this.alive.transform.position.x;
                //Debug.Log("attackdetail truyen:" + this.attackDetails[1]);
                hit.SendMessage("Damage", this.attackDetails);
                
            }
        }
    }

    protected void Flip()
    {
        this.facingDirection *= -1;
        this.alive.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    private void SwitchState(State state)
    {
        switch (this.currentState)
        {
            case State.Moving:
                this.ExitMovingState();
                break;
            case State.Knockback:
                this.ExitKnockbackState();
                break;
            case State.Dead:
                this.ExitDeadState();
                break;
        }
        switch (state)
        {
            case State.Moving:
                this.EnterMovingState();
                break;
            case State.Knockback:
                this.EnterKnockbackState();
                break;
            case State.Dead:
                this.EnterDeadState();
                break;
        }
        this.currentState = state;
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.groundCheck.position, new Vector2(this.groundCheck.position.x, this.groundCheck.position.y - this.groundCheckDistance));
        Gizmos.DrawLine(this.wallCheck.position, new Vector2(this.wallCheck.position.x + this.wallCheckDistance, this.wallCheck.position.y));

        Vector2 botLeft = new Vector2(this.touchDamageCheck.position.x - (this.touchDamageWidth / 2), this.touchDamageCheck.position.y - (this.touchDamageHeight / 2));
        Vector2 botRight = new Vector2(this.touchDamageCheck.position.x + (this.touchDamageWidth / 2), this.touchDamageCheck.position.y - (this.touchDamageHeight / 2));
        Vector2 topRight = new Vector2(this.touchDamageCheck.position.x + (this.touchDamageWidth / 2), this.touchDamageCheck.position.y + (this.touchDamageHeight / 2));
        Vector2 topLeft = new Vector2(this.touchDamageCheck.position.x - (this.touchDamageWidth / 2), this.touchDamageCheck.position.y + (this.touchDamageHeight / 2));

        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);
    }

}
