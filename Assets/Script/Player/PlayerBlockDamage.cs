using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockDamage : MonoBehaviour
{
    [SerializeField] protected GameObject blockParticle;
    [SerializeField] protected Transform blockPosition;

    protected virtual void Damage()
    {
        Debug.Log(blockPosition.position.x + 10f);
        Instantiate(this.blockParticle, new Vector2(this.blockPosition.position.x + 1f, this.blockPosition.position.y), this.blockPosition.rotation);
    }
}
