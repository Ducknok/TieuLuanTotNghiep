using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : DucMonobehavior
{
    protected virtual void FinishAnim()
    {
        Destroy(gameObject);
    }
}
