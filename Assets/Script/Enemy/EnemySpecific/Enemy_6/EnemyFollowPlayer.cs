using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Transform player;
    [SerializeField] protected Animator anim;
    [SerializeField] protected float dashSpeed;
    [SerializeField] protected float lineOfSite;
    [SerializeField] protected float dashDuration = 0.2f; // Thời gian dash
    [SerializeField] protected float dashCooldown = 2f;   // Thời gian giữa các lần dash 
    [SerializeField] protected float returnSpeed = 10f;   // Tốc độ quay về vị trí ban đầu
    [SerializeField] protected Vector3 initialPosition;

    [SerializeField] protected bool isDashing = false;
    [SerializeField] protected bool canDash = true;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
        this.rb = transform.GetComponentInParent<Rigidbody2D>();
        this.anim = transform.GetComponentInParent<Animator>();
        this.initialPosition = this.transform.parent.parent.position;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        this.FlipTowardsPlayer();
        float distanceFromPlayer = Vector2.Distance(this.player.position, this.transform.parent.parent.position);
        if(this.canDash && !this.isDashing && distanceFromPlayer < this.lineOfSite)
        {
           
            //this.transform.parent.position = Vector2.MoveTowards(this.transform.parent.position, this.player.position, speed * Time.deltaTime);
            this.StartCoroutine(DashTowardsPlayer());
        }
        else if(this.canDash && !this.isDashing && distanceFromPlayer > this.lineOfSite)
        {
            this.StartCoroutine(ReturnToInitialPosition());
        }
        
    }

    //protected virtual void Dash()
    //{
    //    this.rb.velocity = new Vector2(this.speed * -1, 0);
    //}
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.parent.parent.position, this.lineOfSite);
    }
    IEnumerator DashTowardsPlayer()
    {
        this.isDashing = true;
        this.canDash = false;
        
        // Xác định hướng từ enemy đến player
        Vector3 directionToPlayer = (this.player.position - this.transform.parent.parent.position).normalized;

        // Tính toán tốc độ dash trên mỗi frame
        Vector3 dashVelocity = directionToPlayer * dashSpeed;

        // Thời gian dash
        float elapsedTime = 0f;
        while (elapsedTime < dashDuration)
        {
            this.anim.SetTrigger("dash");
            this.transform.parent.parent.position += dashVelocity * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
        this.StartCoroutine(EnableCanDash());
    }
    IEnumerator EnableCanDash()
    {
        yield return new WaitForSeconds(2f);
        this.canDash = true;
    }
    IEnumerator ReturnToInitialPosition()
    {
        // Quay về vị trí ban đầu với tốc độ returnSpeed
        while (Vector3.Distance(this.transform.parent.parent.position, this.initialPosition) > 0.1f)
        {
            this.transform.parent.parent.position = Vector3.MoveTowards(this.transform.parent.parent.position, this.initialPosition, this.returnSpeed * Time.deltaTime);
            yield return null;
        }
    }
    private void FlipTowardsPlayer()
    {
        // Kiểm tra xem player đang ở bên trái hay bên phải của enemy
        if (this.player != null)
        {
            Vector3 scale = this.transform.parent.parent.localScale;
            if (this.player.position.x < this.transform.parent.parent.position.x)
            {
                // Player ở bên trái -> quay mặt về bên trái
                scale.x = Mathf.Abs(scale.x);
            }
            else
            {
                // Player ở bên phải -> quay mặt về bên phải
                scale.x = -Mathf.Abs(scale.x);
            }
            this.transform.parent.parent.localScale = scale;
        }
    }
}
