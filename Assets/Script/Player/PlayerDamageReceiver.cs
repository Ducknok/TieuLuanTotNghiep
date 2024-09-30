using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageReceiver : MonoBehaviour
{
    [SerializeField] protected PlayerController playerCtrl;
    public PlayerController PlayerCtrl => playerCtrl;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.playerCtrl = transform.GetComponent<PlayerController>();
    }
    public virtual void Damage(AttackDetails attackDetails)
    {
        //Debug.Log("PlayerDamageReceiver");
        if (!this.playerCtrl.PlayerMove.GetDashStatus())
        {
            int direction;
            
            playerCtrl.PlayerSta.DecreaseHealth(attackDetails.damageAmount);

            if (attackDetails.position.x < this.transform.position.x)
            {
                
                direction = 1;
            }
            else
            {
                //Debug.Log(this.transform.position.x);
                direction = -1;
            }
            this.playerCtrl.PlayerMove.Knockback(direction);
        }
    }

}
