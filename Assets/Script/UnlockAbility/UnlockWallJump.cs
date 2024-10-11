using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockWallJump : MonoBehaviour
{
    [SerializeField] protected GameObject canvasUI;
    [SerializeField] protected bool used;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !this.used)
        {
            this.used = true;
            StartCoroutine(ShowUI());
        }
    }
    IEnumerator ShowUI()
    {
        yield return new WaitForSeconds(0.5f);
        this.canvasUI.SetActive(true);
        yield return new WaitForSeconds(4f);
        canvasUI.SetActive(false);
    }
}
