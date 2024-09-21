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
    public virtual void Damage(float[] attackDetails)
    {
        //Debug.Log("PlayerDamageReceiver");
        if (!this.playerCtrl.PlayerMove.GetDashStatus())
        {
            int direction;
            //Damage player here using attackDetails[0]
            playerCtrl.PlayerSta.DecreaseHealth(attackDetails[0]);

            //Debug.Log("attackdetail0: " + this.PlayerCtrl.PlayerCom.attackDetails[0]);
            //Debug.Log("attackdetail: " + this.PlayerCtrl.PlayerCom.attackDetails[1]);
            //Debug.Log("transform: " + attackDetails[1]);
            if (attackDetails[1] < this.transform.position.x)
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
