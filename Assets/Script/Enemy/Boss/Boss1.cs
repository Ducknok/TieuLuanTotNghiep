using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Boss1State
{
    Idle,
    MeleeAttack,
    RangeAttack1,
    SpawnAttack,
    Dead,
}
public class Boss1 : MonoBehaviour
{
    [SerializeField] protected Boss1State stateData;
    [SerializeField] protected Animator anim;
    [SerializeField] protected float changeState;
    [Header("Range attack")]
    [SerializeField] protected Transform player;
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected FireballProjectile projectileScript;
    [SerializeField] protected Transform fireballPosition;
    [SerializeField] protected float speed;
    [SerializeField] protected float travelDistance;
    [SerializeField] protected float damage;
    [SerializeField] protected float shootingRange;
    [SerializeField] protected float fireballRate = 1f;
    [SerializeField] protected float nextFireTime;
    [Header("Skeleton spawn")]
    [SerializeField] protected Transform spawnPoint;
    [SerializeField] protected GameObject prejectile;
    //[Header("Dead")]
    //[SerializeField] protected Image healthBar;

    protected virtual void Start()
    {
        this.stateData = Boss1State.Idle;
        this.anim = GetComponent<Animator>();

        StartCoroutine(BossStates());
    }

    IEnumerator BossStates()
    {
        var RandomAttack = Random.Range(1, 5);
        yield return new WaitForSeconds(this.changeState);

        switch (RandomAttack)
        {
            case 1:
                this.stateData = Boss1State.Idle;
                break;
            case 2:
                this.stateData = Boss1State.MeleeAttack;
                break;
            case 3:
                this.stateData = Boss1State.RangeAttack1;
                break;
            case 4:
                this.stateData = Boss1State.SpawnAttack;
                break;
            default:
                break;
        }
        this.ChangeState();
    }

    public virtual void ChangeState()
    {
        switch (stateData)
        {
            case Boss1State.Idle:
                anim.SetBool("idle", true);
                StartCoroutine(BossStates());
                break;
            case Boss1State.MeleeAttack:
                anim.SetTrigger("meleeAttack");
                StartCoroutine(BossStates());
                break;
            case Boss1State.RangeAttack1:
                anim.SetTrigger("rangeAttack1");
                //Debug.Log("bumbumbum");
                StartCoroutine(FireBallShoot());
                StartCoroutine(BossStates());
                break;
            case Boss1State.SpawnAttack:
                StartCoroutine(SkeletonSpawn());
                anim.SetTrigger("spawnAttack");
                StartCoroutine(BossStates());
                break;
            default:
                break;
        }
    }
    IEnumerator FireBallShoot()
    {
        int counter = 0;
        while(counter < 5)
        {
            yield return new WaitForSeconds(1f);
            Transform newFireball = FireballSpawner.Instance.Spawn(FireballSpawner.fireball, this.fireballPosition.position, this.fireballPosition.rotation);
            newFireball.gameObject.SetActive(true);
            this.projectileScript = newFireball.GetComponent<FireballProjectile>();
            this.projectileScript.FireProjectTile(this.speed, this.travelDistance, this.damage);
            counter++;
            if(counter > 5)
            {
                break;
            }
        }
    }
    IEnumerator SkeletonSpawn()
    {
        int counter = 0;
        while (counter < 3)
        {
            yield return new WaitForSeconds(1f);
            Transform newSkeleton = SkeletonSpawner.Instance.Spawn(SkeletonSpawner.skeleton, this.spawnPoint.position, this.spawnPoint.rotation);
            newSkeleton.gameObject.SetActive(true);
            counter++;
            if (counter > 3)
            {
                break;
            }
        }
        this.changeState = 7f;
    }
}
