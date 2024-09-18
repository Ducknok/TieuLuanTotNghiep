using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
    //[SerializeField] protected Transform ledgeCheck;
    [SerializeField] protected LayerMask whatIsGround;

    [Header("Movement")]
    [SerializeField] protected float movementImputDirection;
    [SerializeField] protected float movementSpeed = 10.0f;
    [SerializeField] protected float turnTimer;
    [SerializeField] protected float turnTimerSet = 0.1f;
    [SerializeField] protected bool isWalking;
    [SerializeField] protected bool isFacingRight = true;
    [SerializeField] protected bool canMove;
    [SerializeField] protected bool canFlip;

    [Header("Jump")]
    [SerializeField] protected float jumpForce = 16.0f;
    [SerializeField] protected float groundCheckRadius;
    [SerializeField] protected float jumpTimer;
    [SerializeField] protected float jumpTimerSet = 0.15f;
    [SerializeField] protected int amountOfJumps = 1;
    [SerializeField] protected int amountOfJumpsLeft;
    [SerializeField] protected bool isGroundCheck;
    [SerializeField] protected bool canJump;
    [SerializeField] protected bool isAttempingToJump;
    [SerializeField] protected bool canNormalJump;
    [SerializeField] protected bool canWallJump;
    [SerializeField] protected bool checkJumpMultiplier;

    [Header("Wall Check")]
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected float wallSlidingSpeed;
    [SerializeField] protected float moveForeceInAir;
    [SerializeField] protected float airDragMultiplier = 0.95f;
    [SerializeField] protected float variableJumpHeightmultiplier = 0.5f;
    [SerializeField] protected float wallHopForce;
    [SerializeField] protected float wallJumpFore;
    [SerializeField] protected float wallJumpTimer;
    [SerializeField] protected float wallJumpTimerSet = 0.5f;
    [SerializeField] protected int facingDirection = 1;
    [SerializeField] protected int lastWallJumpDirection;
    [SerializeField] protected Vector2 wallHopDirection;
    [SerializeField] protected Vector2 wallJumpDirection;
    [SerializeField] protected bool isTouchingWall;
    [SerializeField] protected bool isWallSliding;
    [SerializeField] protected bool hasWallJumped;
    

    protected virtual void Start()
    {
        this.rb = transform.GetComponentInParent<Rigidbody2D>();
        this.anim = FindObjectOfType<Animator>();
        this.amountOfJumpsLeft = this.amountOfJumps;
        this.wallHopDirection.Normalize();
        this.wallJumpDirection.Normalize();
    }
    protected virtual void Update()
    {
        this.CheckInput();
        this.CheckMovementDirection();
        this.UpdateAnimation();
        this.CheckIfCanJump();
        this.CheckIfWallSliding();
        this.CheckJump();
    }
    protected virtual void FixedUpdate()
    {
        this.ApplyMovement();
        this.CheckSurroundings();
    }
    protected virtual void CheckIfWallSliding()
    {
        if(this.isTouchingWall && this.movementImputDirection == this.facingDirection && this.rb.velocity.y < 0)
        {
            this.isWallSliding = true;
        }
        else
        {
            this.isWallSliding = false;
        }
    }

    protected virtual void CheckSurroundings()
    {
        this.isGroundCheck = Physics2D.OverlapCircle(this.groundCheck.position, this.groundCheckRadius, this.whatIsGround);

        this.isTouchingWall = Physics2D.Raycast(this.wallCheck.position, this.transform.right, this.wallCheckDistance, this.whatIsGround);

        
    }
   
    protected virtual void CheckIfCanJump()
    {
        if(this.isGroundCheck && this.rb.velocity.y <= 0.01f)
        {
            this.amountOfJumpsLeft = this.amountOfJumps;
        }
        if (this.isTouchingWall)
        {
            this.canWallJump = true;
        }
        if(this.amountOfJumpsLeft <= 0)
        {
            this.canNormalJump = false;
        }
        else
        {
            this.canNormalJump = true;
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
        anim.SetBool("isWallSliding", this.isWallSliding);
    }
    protected virtual void CheckInput()
    {
        this.movementImputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (this.isGroundCheck || (this.amountOfJumpsLeft > 0 && this.isTouchingWall))
            {
                this.NormalJump();
            }
            else
            {
                this.jumpTimer = this.jumpTimerSet;
                this.isAttempingToJump = true;
            }
        }
        if(Input.GetButtonDown("Horizontal") && this.isTouchingWall)
        {
            if(!this.isGroundCheck && this.movementImputDirection != facingDirection)
            {
                this.canMove = false;
                this.canFlip = false;
                this.turnTimer = this.turnTimerSet;
            }
        }
        if (this.turnTimer >= 0)
        {
            turnTimer -= Time.deltaTime;

            if(this.turnTimer <= 0)
            {
                this.canMove = true;
                this.canFlip = true;
            }
        }
        if (this.checkJumpMultiplier && !Input.GetButton("Jump"))
        {
            this.checkJumpMultiplier = false;
            this.rb.velocity = new Vector2(this.rb.velocity.x, this.rb.velocity.y * this.variableJumpHeightmultiplier);
        }
    }
    protected virtual void CheckJump()
    {
        if (this.jumpTimer > 0)
        {
            //wall jump
            if (!this.isGroundCheck && this.isTouchingWall && this.movementImputDirection != 0 && this.movementImputDirection != facingDirection)
            {
                this.WallJump();
            }
            else if (this.isGroundCheck)
            {
                this.NormalJump();
            }
        }
        else
        {
            this.jumpTimer -= Time.deltaTime;
        }
        if (this.isAttempingToJump)
        {
            this.jumpTimer -= Time.deltaTime;
        }
        if(wallJumpTimer > 0)
        {
            if (hasWallJumped && movementImputDirection == -lastWallJumpDirection)
            {
                this.rb.velocity = new Vector2(this.rb.velocity.y, 0.0f);
                this.hasWallJumped = false;
            }
            else if (this.wallJumpTimer <= 0)
            {
                this.hasWallJumped = false;
            }
            else
            {
                this.wallJumpTimer -= Time.deltaTime;
            }
        }
        //Wall hop
        //else if (this.isWallSliding && this.movementImputDirection == 0 && canJump)
        //{
        //    this.isWallSliding = false;
        //    this.amountOfJumpsLeft--;
        //    Vector2 forceToadd = new Vector2(this.wallHopForce * this.wallHopDirection.x * -this.facingDirection, this.wallHopForce * this.wallHopDirection.y);
        //    this.rb.AddForce(forceToadd, ForceMode2D.Impulse);
        //}
    }
    protected virtual void NormalJump()
    {
        if (this.canNormalJump)
        {
            this.rb.velocity = new Vector2(this.rb.velocity.x, this.jumpForce);
            this.amountOfJumpsLeft--;
            this.jumpTimer = 0;
            this.isAttempingToJump = false;
            this.checkJumpMultiplier = true;
        }
    }
    protected virtual void WallJump()
    {
        if (this.canWallJump)
        {
            this.rb.velocity = new Vector2(this.rb.velocity.x, 0.0f);
            this.isWallSliding = false;
            this.amountOfJumpsLeft = this.amountOfJumps;
            this.amountOfJumpsLeft--;
            Vector2 forceToadd = new Vector2(this.wallJumpFore * this.wallJumpDirection.x * this.movementImputDirection, this.wallJumpFore * this.wallJumpDirection.y);
            this.rb.AddForce(forceToadd, ForceMode2D.Impulse);
            this.jumpTimer = 0;
            this.isAttempingToJump = false;
            this.checkJumpMultiplier = true;
            this.turnTimer = 0;
            this.canMove = true;
            this.canFlip = true;
            this.hasWallJumped = true;
            this.wallJumpTimer = this.wallJumpTimerSet;
            lastWallJumpDirection = -facingDirection;
        }
    }
    protected virtual void ApplyMovement()
    {
        if (!this.isGroundCheck && !this.isWallSliding && this.movementImputDirection == 0)
        {
            this.rb.velocity = new Vector2(this.rb.velocity.x * this.airDragMultiplier, this.rb.velocity.y);
        }
        else if(this.canMove)
        {
            this.rb.velocity = new Vector2(this.movementSpeed * this.movementImputDirection, this.rb.velocity.y);
        }
        //else if (!this.isGroundCheck && !this.isWallSliding && this.movementImputDirection != 0)
        //{
        //    Vector2 forceToAdd = new Vector2(this.moveForeceInAir * this.movementImputDirection, 0);
        //    this.rb.AddForce(forceToAdd);

        //    if(Mathf.Abs(this.rb.velocity.x) > this.movementSpeed)
        //    {
        //        this.rb.velocity = new Vector2(this.movementSpeed * this.movementImputDirection, this.rb.velocity.y);
        //    }
        //}
        

        if (this.isWallSliding)
        {
            if(this.rb.velocity.y < -this.wallSlidingSpeed)
            {
                this.rb.velocity = new Vector2(this.rb.velocity.x, -this.wallSlidingSpeed);
            }
        }
    }
    protected virtual void Flip()
    {
        if (!this.isWallSliding && this.canFlip)
        {
            facingDirection *= -1;
            this.isFacingRight = !this.isFacingRight;
            this.transform.parent.Rotate(0.0f, 180.0f, 0.0f);
        }
        
    }
    protected void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.groundCheck.position, this.groundCheckRadius);

        Gizmos.DrawLine(this.wallCheck.position, new Vector3(this.wallCheck.position.x + this.wallCheckDistance, this.wallCheck.position.y, this.wallCheck.position.z));
    }
}
