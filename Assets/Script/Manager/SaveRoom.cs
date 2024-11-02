using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveRoom : DucMonobehavior
{
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerStats.Instance.DataToSave();
            Debug.Log("Game Saved");
        }
    }
}
