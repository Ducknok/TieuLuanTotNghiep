using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroy : MonoBehaviour
{
    [SerializeField] protected float timeToDestroy;
    protected virtual void Start()
    {
        StartCoroutine(Destroy());
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }
}
