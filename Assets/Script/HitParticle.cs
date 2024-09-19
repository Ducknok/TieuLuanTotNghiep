using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : MonoBehaviour
{
    protected virtual void FinishAnim()
    {
        Destroy(gameObject);
    }
}
