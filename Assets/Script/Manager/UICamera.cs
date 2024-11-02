using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICamera : DucMonobehavior
{
    [SerializeField] protected Transform player;
    [SerializeField] protected float xPos;
    [SerializeField] protected float yPos;
    [SerializeField] protected float zPos;

    protected override void Start()
    {
        this.transform.position = new Vector3(this.player.position.x + xPos, this.player.position.y + yPos, zPos);
    }
    protected override void Update()
    {
        this.transform.position = new Vector3(this.player.position.x + xPos, this.player.position.y + yPos, zPos);
    }
}
