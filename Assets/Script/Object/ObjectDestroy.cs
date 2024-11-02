using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroy : DucMonobehavior
{
    [SerializeField] protected float timeToDestroy;
    protected override void Start()
    {
        StartCoroutine(Destroy());
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }
}
