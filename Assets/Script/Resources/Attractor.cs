using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : DucMonobehavior
{
    [SerializeField] protected Transform playerTransform;
    [SerializeField] protected GameObject items;
    [SerializeField] protected float speed = 5f;          

    protected override void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            this.playerTransform = player.transform;
        }
    }

    protected override void Update()
    {
        this.StartCoroutine(MoveToPlayer());
    }

    IEnumerator MoveToPlayer()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(RotateTowardsPlayer());
        this.transform.parent.position = Vector3.MoveTowards(this.transform.parent.position, this.playerTransform.position, speed * Time.deltaTime);
    }
    IEnumerator RotateTowardsPlayer()
    {
        yield return new WaitForSeconds(0f);
        // Calculate the direction from the spawned object to the player
        Vector2 direction = (this.playerTransform.position - this.items.transform.position).normalized;

        // Calculate the angle in degrees from the direction vector
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the spawned object to face the player
        this.items.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
