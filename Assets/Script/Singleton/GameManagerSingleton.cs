using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSingleton : MonoBehaviour
{
    [SerializeField] private static GameManagerSingleton instance;
    public static GameManagerSingleton Instance => instance;

    protected virtual void Awake()
    {
        GameManagerSingleton.instance = this;
    }
}
