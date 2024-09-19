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
    [SerializeField] protected Transform groundCheck, wallCheck;
    [SerializeField] protected Animator aliveAnim;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField]
    protected float
        groundCheckDistance,
        wallCheckDistance,
        movementSpeed,
        maxHealth,
        currentHealth,
        knockbackDuration,
        knockbackStartTime;
    [SerializeField] protected int 
        facingDirection,
        damageDirection;
    [SerializeField] protected Vector2 movement;
    [SerializeField] protected Vector2 knockbackSpeed;

    [SerializeField] protected bool 
        groundDetected, 
        wallDetected;

    protected virtual void Start()
    {
        this.alive = this.transform.Find("Alive").gameObject;
        this.aliveRb = this.aliveRb.GetComponent<Rigidbody2D>();
        this.aliveAnim = this.aliveAnim.GetComponent<Animator>();

        this.currentHealth = this.maxHealth;
        this.facingDirection = 1;
        
    }
    protected virtual void Update()
    {
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
        if(Time.time >= this.knockbackStartTime + this.knockbackDuration)
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
    protected virtual void OnDrawGismos()
    {
        Gizmos.DrawLine(this.groundCheck.position, new Vector2(this.groundCheck.position.x, this.groundCheck.position.y - this.groundCheckDistance));
        Gizmos.DrawLine(this.wallCheck.position, new Vector2(this.wallCheck.position.x + this.wallCheckDistance, this.wallCheck.position.y));
    }

}
