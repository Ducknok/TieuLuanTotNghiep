using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossEW2State
{
    Idle,
    RangeAttack,
    RangeAttack2,
    Walk,
    Dead,
}
public class EvilWizard2 : MonoBehaviour
{
    public BossEW2State state;
    [SerializeField] protected Animator anim;
    [SerializeField] protected float changeState;
    
    
    //[Header("Move")]
    [Header("Range attack")]
    [SerializeField] protected Transform player;
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected PurpleBulletProjectile projectileScript;
    [SerializeField] protected Transform bulletPosition;
    [SerializeField] protected float speed;
    [SerializeField] protected float travelDistance;
    [SerializeField] protected float damage;
    [SerializeField] protected float shootingRange;
    [SerializeField] protected float bulletRate = 1f;
    [SerializeField] protected float nextFireTime;
    [Header("Bullet from Above")]
    [SerializeField] protected GameObject purpleBulletFA;
    [SerializeField] protected GameObject[] randomPosition;
    [SerializeField] protected float startFA = 1.1f;
    [SerializeField] protected float faTime = 2f;
    


    protected virtual void Start()
    {
        this.state = BossEW2State.RangeAttack2;
        this.anim = GetComponentInChildren<Animator>();
        StartCoroutine(BossStates());
    }
    IEnumerator BossStates()
    {
        var RandomStates = Random.Range(1, 5);
        yield return new WaitForSeconds(this.changeState);

        switch (RandomStates)
        {
            case 1:
                this.state = BossEW2State.Idle;
                break;
            case 2:
                this.state = BossEW2State.RangeAttack;
                break;
            case 3:
                this.state = BossEW2State.RangeAttack2;
                break;
            case 4:
                this.state = BossEW2State.Walk;
                break;
            default:
                break;
        }
        this.ChangeState();
    }
    public virtual void ChangeState()
    {
        switch (state)
        {
            case BossEW2State.Idle:
                this.anim.SetBool("idle", true);
                StartCoroutine(BossStates());
                break;
            case BossEW2State.RangeAttack:
                this.anim.SetBool("idle", false);
                this.anim.SetTrigger("rangeAttack1");
                StartCoroutine(BulletFollowPlayer());
                StartCoroutine(BossStates());
                break;
            case BossEW2State.RangeAttack2:
                this.anim.SetBool("idle", false);
                this.anim.SetTrigger("rangeAttack2");
                StartCoroutine(PurbleBulletShooting());
                StartCoroutine(BossStates());
                break;
            case BossEW2State.Walk:
                this.anim.SetBool("idle", false);
                this.anim.SetTrigger("move");
                StartCoroutine(BossStates());
                break;
            default:
                break;
        }
    }
    
    IEnumerator BulletFollowPlayer()
    {
        int counter = 0;
        while (counter < 5)
        {
            yield return new WaitForSeconds(0.9f);
            Transform newBullet = PurpleBulletSpawner.Instance.Spawn(PurpleBulletSpawner.purpleBullet, this.bulletPosition.position, this.bulletPosition.rotation);
            newBullet.gameObject.SetActive(true);
            this.projectileScript = newBullet.GetComponent<PurpleBulletProjectile>();
            this.projectileScript.FireProjectTile(this.speed, this.travelDistance, this.damage);
            counter++;
            if (counter > 5)
            {
                break;
            }
        }
        this.changeState = 5f;
    }
    IEnumerator PurpleBulletFromAbove()
    {
        this.changeState = 12f;
        yield return new WaitForSeconds(startFA);
        StartCoroutine(PurbleBulletShooting());
    }
    IEnumerator PurbleBulletShooting()
    {
        int counter = 0;
        while (counter < 12)
        {
            var position = Random.Range(0, 3);
            yield return new WaitForSeconds(faTime);
            Transform newBullet = PurpleBulletSpawner.Instance.Spawn(PurpleBulletSpawner.purpleBullet, this.randomPosition[position].transform.position , this.randomPosition[position].transform.rotation);
            newBullet.gameObject.SetActive(true);
            this.projectileScript = newBullet.GetComponent<PurpleBulletProjectile>();
            this.projectileScript.FireProjectTile(this.speed, this.travelDistance, this.damage);
            counter++;
            if (counter > 12)
            {
                break;
            }
        }
        this.changeState = 5f;
    }
}
