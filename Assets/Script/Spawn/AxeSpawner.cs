using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSpawner : Spawner
{
    private static AxeSpawner instance;
    public static AxeSpawner Instance => instance;

    public static string axe = "Axe_1";

    protected virtual void Awake()
    {
        if (AxeSpawner.instance != null) Debug.LogError("Only 1 AxeSpawner allow to exist");
        AxeSpawner.instance = this;
    }
}
