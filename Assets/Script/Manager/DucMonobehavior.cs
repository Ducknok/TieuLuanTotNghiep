using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DucMonobehavior : MonoBehaviour
{
    protected virtual void Start() { }     
    protected virtual void Update() { }
    protected virtual void Awake() { }
    protected virtual void FixedUpdate() { }
    protected virtual void OnDrawGizmos() { }
    protected virtual void OnTriggerEnter2D(Collider2D collider) { }
    protected virtual void OnTriggerExit2D(Collider2D collider) { }
    protected virtual void OnDrawGizmosSelected() { }
    protected virtual void OnEnable() { }
    protected virtual void OnDisable() { }
    public virtual void TriggerAttack() { }
    public virtual void FinishAttack() { }
}
