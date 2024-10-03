using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : Spawner
{
    private static ArrowSpawner instance;
    public static ArrowSpawner Instance => instance;

    public static string arrow = "Arrow_1";

    protected virtual void Awake()
    {
        if (ArrowSpawner.instance != null) Debug.LogError("Only 1 ArrowSpawner allow to exist");
        ArrowSpawner.instance = this;
    }
}
