using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSingleton : DucMonobehavior
{
    [SerializeField] private static GameManagerSingleton instance;
    public static GameManagerSingleton Instance => instance;

    protected override void Awake()
    {
        GameManagerSingleton.instance = this;
    }
}
