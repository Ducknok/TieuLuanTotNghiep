using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : Spawner
{
    private static FireballSpawner instance;
    public static FireballSpawner Instance => instance;

    public static string fireball = "FireBall";

    protected virtual void Awake()
    {
        if (FireballSpawner.instance != null) Debug.LogError("Only 1 FireballSpawner allow to exist");
        FireballSpawner.instance = this;
    }
}
