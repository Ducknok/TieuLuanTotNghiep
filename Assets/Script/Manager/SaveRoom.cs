using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveRoom : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerStats.Instance.DataToSave();
            Debug.Log("Game Saved");
        }
    }
}
