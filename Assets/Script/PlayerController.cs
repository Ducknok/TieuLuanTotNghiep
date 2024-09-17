using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected LayerMask whatIsGround;

    [Header("Movement")]
    [SerializeField] protected bool isFacingRight = true;
    [SerializeField] protected float movementImputDirection;
    [SerializeField] protected float movementSpeed = 10.0f;
    [SerializeField] protected bool isWalking;
    

    [Header("Jump")]
    [SerializeField] protected float jumpForce = 16.0f;
    [SerializeField] protected float groundCheckRadius;
    [SerializeField] protected int amountOfJumps = 1;
    [SerializeField] protected int amountOfJumpsLeft;
    [SerializeField] protected bool isGroundCheck;
    [SerializeField] protected bool canJump;
    
    




    protected virtual void Start()
    {
        this.rb = transform.GetComponentInParent<Rigidbody2D>();
        this.anim = FindObjectOfType<Animator>();
        this.amountOfJumpsLeft = this.amountOfJumps;
    }
    protected virtual void Update()
    {
        this.CheckInput();
        this.CheckMovementDirection();
        this.UpdateAnimation();
        this.CheckIfCanJump();
    }
    protected virtual void FixedUpdate()
    {
        this.ApplyMovement();
        this.CheckSurroundings();
    }
    protected virtual void CheckSurroundings()
    {
        this.isGroundCheck = Physics2D.OverlapCircle(this.groundCheck.position, this.groundCheckRadius, this.whatIsGround);
    }
    protected virtual void CheckIfCanJump()
    {
        if(this.isGroundCheck && this.rb.velocity.y <= 0)
        {
            this.amountOfJumpsLeft = this.amountOfJumps;
        }
        if(this.amountOfJumpsLeft <= 0)
        {
            this.canJump = false;
        }
        else
        {
            this.canJump = true;
        }
    }
    protected virtual void CheckMovementDirection()
    {
        if(this.isFacingRight && this.movementImputDirection < 0)
        {
            this.Flip();
        }
        else if(!this.isFacingRight && this.movementImputDirection > 0)
        {
            this.Flip();
        }
        if(this.rb.velocity.x != 0)
        {
            this.isWalking = true;
        }
        else
        {
            this.isWalking = false;
        }
    }
    protected virtual void UpdateAnimation()
    {
        anim.SetBool("isWalking", this.isWalking);
        anim.SetBool("isGrounded", this.isGroundCheck);
        anim.SetFloat("yVelocity", this.rb.velocity.y);
    }
    protected virtual void CheckInput()
    {
        this.movementImputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            this.Jump();
        }
    }
    protected virtual void Jump()
    {
        if (canJump)
        {
            this.rb.velocity = new Vector2(this.rb.velocity.x, this.jumpForce);
            this.amountOfJumpsLeft--;
        }
        
    }
    protected virtual void ApplyMovement()
    {
        this.rb.velocity = new Vector2(this.movementSpeed * this.movementImputDirection, this.rb.velocity.y);
    }
    protected virtual void Flip()
    {
        this.isFacingRight = !this.isFacingRight;
        this.transform.parent.Rotate(0.0f, 180.0f, 0.0f);
    }
    protected void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.groundCheck.position, this.groundCheckRadius);
    }
}
