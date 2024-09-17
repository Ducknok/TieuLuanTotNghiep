using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float movementImputDirection;
    [SerializeField] protected float movementSpeed = 10.0f;
    [SerializeField] protected bool isFacingRight = true;
    [SerializeField] protected float jumpForce = 16.0f;



    protected virtual void Start()
    {
        this.rb = transform.GetComponentInParent<Rigidbody2D>();
    }
    protected virtual void Update()
    {
        this.CheckInput();
        this.CheckMovementDirection();
    }
    protected virtual void FixedUpdate()
    {
        this.ApplyMovement();
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
        this.rb.velocity = new Vector2(rb.velocity.x, this.jumpForce);
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
}
