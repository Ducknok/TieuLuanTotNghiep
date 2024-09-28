using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivation : MonoBehaviour
{
    [SerializeField] protected PlayerController playerCtrl;
    public PlayerController PlayerCtrl => playerCtrl;
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            BossUI.Instance.BossActivation();
            StartCoroutine(WaitForBoss());
            
        }
    }

    IEnumerator WaitForBoss()
    {
        var currentSpeed = this.playerCtrl.PlayerMove.movementSpeed;
        this.playerCtrl.PlayerMove.movementSpeed = 0f;
        yield return new WaitForSeconds(3f);
        this.playerCtrl.PlayerMove.movementSpeed = currentSpeed;
        Destroy(gameObject);
    }
}
