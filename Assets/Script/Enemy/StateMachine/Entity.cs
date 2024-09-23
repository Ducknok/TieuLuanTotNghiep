using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    protected Transform
        wallCheck,
        ledgeCheck,
        playerCheck,
        groundCheck;
    public FiniteStateMachine stateMachine;
    public D_Entity entityData;
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGo { get; private set; }
    public AnimationToStateMachine atsm { get; private set; }
    public int facingDirection { get; private set; }
    public int lastDamageDirection { get; private set; }


    [SerializeField] protected float currentHealth;
    [SerializeField] protected float currentStunResistance;
    [SerializeField] protected float lastDamageTime;
    [SerializeField] protected Vector2 velocityWorkspace;
    [SerializeField] protected bool isStunned;
    [SerializeField] protected bool isDead;

    public virtual void Start()
    {
        this.facingDirection = 1;
        this.currentHealth = this.entityData.maxHealth;
        this.currentStunResistance = this.entityData.stunResistance;

        if (this.aliveGo != null) return;
        else this.aliveGo = this.transform.Find("Alive").gameObject;
        this.rb = this.aliveGo.GetComponent<Rigidbody2D>();
        this.anim = this.aliveGo.GetComponent<Animator>();
        this.atsm = this.aliveGo.GetComponent<AnimationToStateMachine>();
        this.stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        this.stateMachine.currentState.LogicUpdate();

        this.anim.SetFloat("yVelocity", this.rb.velocity.y);

        if (Time.time >= this.lastDamageTime + this.entityData.stunRecoveryTime)
        {
            this.ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        this.stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        this.velocityWorkspace.Set(this.facingDirection * velocity, this.rb.velocity.y);
        this.rb.velocity = this.velocityWorkspace;
    }

    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        this.velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
        this.rb.velocity = this.velocityWorkspace;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(this.wallCheck.position, this.aliveGo.transform.right, this.entityData.wallCheckDistance, this.entityData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(this.ledgeCheck.position, Vector2.down, this.entityData.ledgeCheckDistance, this.entityData.whatIsGround);
    }

    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(this.groundCheck.position, this.entityData.groundCheckRadius, this.entityData.whatIsGround);
    }
    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(this.playerCheck.position, this.aliveGo.transform.right, this.entityData.minAgroDistance, this.entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(this.playerCheck.position, this.aliveGo.transform.right, this.entityData.maxAgroDistance, this.entityData.whatIsPlayer);
    }

    public virtual void DamageHop(float velocity)
    {
        this.velocityWorkspace.Set(this.rb.velocity.x, velocity);
        this.rb.velocity = this.velocityWorkspace;
    }

    public virtual void ResetStunResistance()
    {
        this.isStunned = false;
        this.currentStunResistance = this.entityData.stunResistance;
    }

    public virtual void Damage(AttackDetails attackDetails)
    {
        this.lastDamageTime = Time.time;
        this.currentHealth -= attackDetails.damageAmount;
        this.currentStunResistance -= attackDetails.stunDamageAmount;
        this.DamageHop(this.entityData.damageHopSpeed);
        Instantiate(this.entityData.hitParticle, this.aliveGo.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        if(attackDetails.position.x > this.aliveGo.transform.position.x)
        {
            this.lastDamageDirection = -1;
        }
        else
        {
            this.lastDamageDirection = 1;
        }
        if(this.currentStunResistance <= 0)
        {
            this.isStunned = true;
        }
        if(this.currentHealth <= 0)
        {
            this.isDead = true;
        }
    }

    public virtual void Flip()
    {
        this.facingDirection *= -1;
        this.aliveGo.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(this.playerCheck.position, this.aliveGo.transform.right, this.entityData.closeRangeActionDistance, this.entityData.whatIsPlayer);
    }
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.wallCheck.position, this.wallCheck.position + (Vector3)(Vector2.right * this.facingDirection * this.entityData.wallCheckDistance));
        Gizmos.DrawLine(this.ledgeCheck.position, this.ledgeCheck.position + (Vector3)(Vector2.down * this.entityData.ledgeCheckDistance));

        Gizmos.DrawWireSphere((this.playerCheck.position + (Vector3)(Vector2.right * this.entityData.closeRangeActionDistance)), 0.2f);
        Gizmos.DrawWireSphere((this.playerCheck.position + (Vector3)(Vector2.right * this.entityData.minAgroDistance)), 0.2f);
        Gizmos.DrawWireSphere((this.playerCheck.position + (Vector3)(Vector2.right * this.entityData.maxAgroDistance)), 0.2f);

    }
}
