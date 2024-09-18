using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    [SerializeField] protected Transform player;
    [SerializeField] protected SpriteRenderer sr;
    [SerializeField] protected SpriteRenderer playerSR;
    [SerializeField] protected Color color;

    [SerializeField] protected float activeTime = 0.1f;
    [SerializeField] protected float timeActivated;
    [SerializeField] protected float alpha;
    [SerializeField] protected float alphaSet = 0.8f;
    [SerializeField] protected float alphaMultiplier = 0.85f;

    protected virtual void OnEnable()
    {
        this.sr = GetComponent<SpriteRenderer>();
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
        this.playerSR = player.GetComponentInChildren<SpriteRenderer>();

        this.alpha = this.alphaSet;
        this.sr.sprite = this.playerSR.sprite;
        this.transform.position = this.player.position;
        this.transform.rotation = this.player.rotation;
        this.timeActivated = Time.time;
    }
    protected virtual void Update()
    {
        this.alpha *= this.alphaMultiplier;
        this.color = new Color(1f, 1f, 1f, alpha);
        this.sr.color = this.color;

        if(Time.time >= (timeActivated + activeTime))
        {
            //Add back to pool
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
