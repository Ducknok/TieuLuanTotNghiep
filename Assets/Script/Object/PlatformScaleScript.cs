using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScaleScript : DucMonobehavior
{
    //PLayer di theo moving platform
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(this.gameObject.GetComponent<ObjectMovement>().willDestroy)
            {
                this.gameObject.GetComponent<ObjectMovement>().startCountdown = true;
            }
            collision.transform.SetParent(this.transform);
        }
    }
    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

}
