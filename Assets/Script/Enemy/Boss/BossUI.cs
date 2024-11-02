using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossUI : DucMonobehavior
{
    public GameObject bossPanel;
    public GameObject lasers;
    [SerializeField] protected static BossUI instance;
    public static BossUI Instance => instance;

    protected override void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    protected override void Start()
    {
        this.bossPanel.SetActive(false);
        this.lasers.SetActive(false);
    }

    public virtual void BossActivation()
    {
        this.bossPanel.SetActive(true);
        this.lasers.SetActive(true);
    }
}
