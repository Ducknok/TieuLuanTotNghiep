using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] protected Transform pointA, pointB;
    [SerializeField] protected float speed;
    [SerializeField] protected float timeToDestroy;
    [SerializeField] protected float destroyCountdown;
    [SerializeField] protected float timeToWait;
    [SerializeField] public bool startCountdown;
    [SerializeField] protected bool shouldMove;
    [SerializeField] protected bool shouldWait;
    [SerializeField] public bool willDestroy;
    [SerializeField] protected bool moveToA;
    [SerializeField] protected bool moveToB;
    [SerializeField] protected bool canContinue;

    protected virtual void Start()
    {
        this.moveToA = true;
        this.moveToB = false;
        this.canContinue = true;
        this.destroyCountdown = timeToDestroy;
    }
    protected virtual void Update()
    {
        if (this.shouldMove)
        {
            this.MoveObject();
        }
        if (this.startCountdown)
        {
            this.destroyCountdown -= Time.deltaTime;
            if(this.destroyCountdown <= 0)
            {
                StartCoroutine(ReactivatePlatform());
                this.destroyCountdown = timeToDestroy;
                this.startCountdown = false;
            }
        }
        
    }

    IEnumerator ReactivatePlatform()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        BoxCollider2D[] myColliders = gameObject.GetComponents<BoxCollider2D>();
        foreach(var collider in myColliders)
        {
            collider.enabled = false;
        }
        yield return new WaitForSeconds(2f);
        foreach (var collider in myColliders)
        {
            collider.enabled = true;
        }
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    protected virtual void MoveObject()
    {
        float distanceToA = Vector2.Distance(this.transform.position, this.pointA.position);
        float distanceToB = Vector2.Distance(this.transform.position, this.pointB.position);

        if(distanceToA > 0.1f && this.moveToA)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, this.pointA.position, this.speed * Time.deltaTime);
            if(distanceToA < 0.3f && this.canContinue)
            {

                if (shouldWait)
                {
                    StartCoroutine(Waiter());
                    this.moveToA = false;
                    this.moveToB = true;
                }
                else
                {
                    this.moveToA = false;
                    this.moveToB = true;
                }
                
            }
        }
        if(distanceToA > 0.1f && this.moveToB)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, this.pointB.position, this.speed * Time.deltaTime);
            if(distanceToB < 0.3f && this.canContinue)
            {
                if (shouldWait)
                {
                    StartCoroutine(Waiter());
                    this.moveToA = true;
                    this.moveToB = false;
                }
                else
                {
                    this.moveToA = true;
                    this.moveToB = false;
                }
                
                
            }
        }
    }
    
    IEnumerator Waiter()
    {
        this.shouldMove = false;
        this.canContinue = false;
        yield return new WaitForSeconds(timeToWait);
        this.shouldMove = true;
        this.canContinue = true;
    }
}
