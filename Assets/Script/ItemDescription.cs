using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
    [SerializeField] protected GameObject textDesc;
    // Start is called before the first frame update
    void Start()
    {
        this.textDesc.SetActive(false);
    }

    public virtual void Show()
    {
       this.textDesc.SetActive(true);
    }
    public virtual void Hide()
    {
        this.textDesc.SetActive(false);
    }
}
