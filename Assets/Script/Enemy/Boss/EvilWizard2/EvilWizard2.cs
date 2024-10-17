using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossEW2State
{
    Idle,
    RangeAttack,
    RangeAttack2,
    RangeAttackCircle,
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
    [Header("Bullet from Above")]
    [SerializeField] protected PurpleBulletAboveProjectile projectileAboveScript;
    [SerializeField] protected GameObject purpleBulletFA;
    [SerializeField] protected GameObject[] randomPosition;
    [SerializeField] protected float startFA = 1.1f;
    [SerializeField] protected float faTime = 2f;
    [Header("Bullet Circle")]
    [SerializeField] protected Transform[] position;

    


    protected virtual void Start()
    {
        this.state = BossEW2State.Idle;
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
                this.state = BossEW2State.RangeAttackCircle;
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
                StartCoroutine(PurpleBulletFromAbove());
                StartCoroutine(BossStates());
                break;
            case BossEW2State.RangeAttackCircle:
                this.anim.SetBool("idle", false);
                this.anim.SetTrigger("rangeAttack3");
                StartCoroutine(PurpeBulletCircle());
                StartCoroutine(BossStates());
                break;
            default:
                break;
        }
    }
    
    IEnumerator BulletFollowPlayer()
    {
        this.changeState = 5f;
        int counter = 0;
        while (counter < 5)
        {
            yield return new WaitForSeconds(0.7f);
            Transform newBullet = PurpleBulletSpawner.Instance.Spawn(PurpleBulletSpawner.purpleBullet, this.bulletPosition.position, this.bulletPosition.rotation);
            newBullet.gameObject.SetActive(true);
            this.projectileScript = newBullet.GetComponent<PurpleBulletProjectile>();
            this.projectileScript.FireProjectTile(this.speed, this.travelDistance, this.damage);
            counter++;
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
            Transform newBullet = PurpleBulletAboveSpawner.Instance.Spawn(PurpleBulletAboveSpawner.purpleBulletAbove, this.randomPosition[position].transform.position , this.randomPosition[position].transform.rotation);
            newBullet.gameObject.SetActive(true);
            this.projectileAboveScript = newBullet.GetComponent<PurpleBulletAboveProjectile>();
            this.projectileAboveScript.FireProjectTile(this.speed, this.travelDistance, this.damage);
            counter++;
        }
        this.changeState = 5f;
    }
    IEnumerator PurpeBulletCircle()
    {
        this.changeState = 1f;
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < position.Length; i++)
        {
            Transform newBullet = PurpleBulletSpawner.Instance.Spawn(PurpleBulletSpawner.purpleBullet, this.position[i].position, this.position[i].rotation);
            newBullet.gameObject.SetActive(true);
            this.projectileScript = newBullet.GetComponent<PurpleBulletProjectile>();
            this.projectileScript.FireProjectTile(this.speed, this.travelDistance, this.damage);
        }
        this.changeState = 5f;
    }
}
