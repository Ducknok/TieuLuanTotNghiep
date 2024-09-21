using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public D_Entity entityData;
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGo { get; private set; }
    [SerializeField] protected Transform 
        wallCheck,
        ledgeCheck;
    public int facingDirection { get; private set; }
    [SerializeField] protected Vector2 velocityWorkspace;

    public virtual void Start()
    {
        this.facingDirection = 1;
        this.aliveGo = this.transform.Find("Alive").gameObject;
        this.rb = this.aliveGo.GetComponent<Rigidbody2D>();
        this.anim = this.aliveGo.GetComponent<Animator>();
        this.stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        this.stateMachine.currentState.LogicUpdate();
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
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(this.wallCheck.position, this.aliveGo.transform.right, this.entityData.wallCheckDistance, this.entityData.whatIsGround);
    }
    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(this.ledgeCheck.position, Vector2.down, this.entityData.ledgeCheckDistance, this.entityData.whatIsGround);
    }
    public virtual void Flip()
    {
        this.facingDirection *= -1;
        this.aliveGo.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.wallCheck.position, this.wallCheck.position + (Vector3)(Vector2.right * this.facingDirection * this.entityData.wallCheckDistance));
        Gizmos.DrawLine(this.ledgeCheck.position, this.ledgeCheck.position + (Vector3)(Vector2.down * this.entityData.ledgeCheckDistance));
    }
}
