using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockDamage : DucMonobehavior
{
    [SerializeField] protected GameObject blockParticle;
    [SerializeField] protected Transform blockPosition;

    protected virtual void Damage()
    {
        Debug.Log(blockPosition.position.x + 10f);
        Instantiate(this.blockParticle, new Vector2(this.blockPosition.position.x, this.blockPosition.position.y + 1f), this.blockPosition.rotation);
    }
}
