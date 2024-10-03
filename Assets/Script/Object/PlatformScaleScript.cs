using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScaleScript : MonoBehaviour
{
    //PLayer di theo moving platform
    private void OnTriggerEnter2D(Collider2D collision)
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
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

}
