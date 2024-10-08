using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBag : MonoBehaviour
{
    [SerializeField] protected GameObject inventoryMenu;
    [SerializeField] protected bool isPaused;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        Time.timeScale = 1;
        this.inventoryMenu.SetActive(false);
        this.isPaused = false;
    }
    protected virtual void Update()
    {
        this.Bag();

    }
    public virtual void Bag()
    {
        if (Input.GetKeyDown(KeyCode.B) && !this.isPaused)
        {
            Time.timeScale = 0;

            this.inventoryMenu.SetActive(true);
            this.isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.B) && this.isPaused)
        {
            Time.timeScale = 1;
            this.inventoryMenu.SetActive(false);
            this.isPaused = false;
        }
    }
}
