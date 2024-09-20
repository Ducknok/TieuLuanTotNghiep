using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
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

    [Header("Dash")]
    [SerializeField] protected float dashTime;
    [SerializeField] protected float dashSpeed;
    [SerializeField] protected float distanceBetweenImages;
    [SerializeField] protected float dashCoolDown;
    [SerializeField] protected float dashTimeLeft;
    [SerializeField] protected float lastImageXpos;
    [SerializeField] protected float lastDash = -100f;
    [SerializeField] protected bool isDashing = false;

    [Header("Knockback")]
    [SerializeField] protected float knockbackStartTime;
    [SerializeField] protected float knockbackDuration;
    [SerializeField] protected Vector2 knockbackSpeed;
    [SerializeField] protected bool knockback;


    protected virtual void Start()
    {
        this.rb = transform.GetComponentInParent<Rigidbody2D>();
        this.anim = transform.GetComponentInParent<Animator>();
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
        this.CheckDash();
        this.CheckKnockback();
    }
    protected virtual void FixedUpdate()
    {
        this.ApplyMovement();
        this.CheckSurroundings();
    }
    protected virtual void CheckIfWallSliding()
    {
        if (this.isTouchingWall && this.movementImputDirection == this.facingDirection && this.rb.velocity.y < 0)
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
        if (this.isGroundCheck && this.rb.velocity.y <= 0.01f)
        {
            this.amountOfJumpsLeft = this.amountOfJumps;
        }
        if (this.isTouchingWall)
        {
            this.canWallJump = true;
        }
        if (this.amountOfJumpsLeft <= 0)
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
        if (this.isFacingRight && this.movementImputDirection < 0)
        {
            this.Flip();
        }
        else if (!this.isFacingRight && this.movementImputDirection > 0)
        {
            this.Flip();
        }
        if (Mathf.Abs(this.rb.velocity.x) >= 0.01f)
        {
            this.isWalking = true;
        }
        else
        {
            this.isWalking = false;
        }
    }
    public virtual bool GetDashStatus()
    {
        //Debug.Log(isDashing);
        return isDashing;
        
    }
    public virtual void Knockback(int direction)
    {
        //Debug.Log("Knockback");
        this.knockback = true;
        this.knockbackStartTime = Time.time;
        this.rb.velocity = new Vector2(this.knockbackSpeed.x * direction, this.knockbackSpeed.y);
    }

    protected virtual void CheckKnockback()
    {
        if(Time.time >= this.knockbackStartTime + this.knockbackDuration && this.knockback)
        {
            this.knockback = false;
            this.rb.velocity = new Vector2(0.0f, this.rb.velocity.y);
        }
    }
    protected virtual void UpdateAnimation()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGroundCheck);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
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
        if (Input.GetButtonDown("Horizontal") && this.isTouchingWall)
        {
            if (!this.isGroundCheck && this.movementImputDirection != facingDirection)
            {
                this.canMove = false;
                this.canFlip = false;
                this.turnTimer = this.turnTimerSet;
            }
        }
        if (this.turnTimer >= 0)
        {
            turnTimer -= Time.deltaTime;

            if (this.turnTimer <= 0)
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
        if (Input.GetButton("Dash"))
        {
            Debug.Log("Da an dash");
            if (Time.time >= (this.lastDash + this.dashCoolDown))
                this.AttemptToDash();
        }
    }

    protected virtual void AttemptToDash()
    {
        this.isDashing = true;
        this.dashTimeLeft = this.dashTime;
        this.lastDash = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
        this.lastImageXpos = transform.position.x;
    }

    public int GetFacingDirection()
    {
        return facingDirection;
    }
    protected virtual void CheckDash()
    {
        if (this.isDashing)
        {
            if (this.dashTimeLeft > 0)
            {
                this.canMove = false;
                this.canFlip = false;
                this.rb.velocity = new Vector2(this.dashSpeed * this.facingDirection, 0);
                this.dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(this.transform.position.x - this.lastImageXpos) > this.distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    this.lastImageXpos = this.transform.position.x;
                }
            }
            if (this.dashTimeLeft <= 0 || this.isTouchingWall)
            {
                this.isDashing = false;
                this.canMove = true;
                this.canFlip = true;
            }
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
        if (wallJumpTimer > 0)
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
        if (!this.isGroundCheck && !this.isWallSliding && this.movementImputDirection == 0 && !this.knockback)
        {
            this.rb.velocity = new Vector2(this.rb.velocity.x * this.airDragMultiplier, this.rb.velocity.y);
        }
        else if (this.canMove && !this.knockback)
        {
            this.rb.velocity = new Vector2(this.movementSpeed * this.movementImputDirection, this.rb.velocity.y);
        }

        if (this.isWallSliding)
        {
            if (this.rb.velocity.y < -this.wallSlidingSpeed)
            {
                this.rb.velocity = new Vector2(this.rb.velocity.x, -this.wallSlidingSpeed);
            }
        }
    }
    protected virtual void Flip()
    {
        if (!this.isWallSliding && this.canFlip && !knockback)
        {
            facingDirection *= -1;
            this.isFacingRight = !this.isFacingRight;
            this.transform.parent.Rotate(0.0f, 180.0f, 0.0f);
        }

    }
    public virtual void DisableFlip()
    {
        this.canFlip = false;
    }
    public virtual void EnableFlip()
    {
        this.canFlip = true;
    }
    protected void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.groundCheck.position, this.groundCheckRadius);

        Gizmos.DrawLine(this.wallCheck.position, new Vector3(this.wallCheck.position.x + this.wallCheckDistance, this.wallCheck.position.y, this.wallCheck.position.z));
    }
}
