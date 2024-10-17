using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBulletAboveSpawner : Spawner
{
    private static PurpleBulletAboveSpawner instance;
    public static PurpleBulletAboveSpawner Instance => instance;

    public static string purpleBulletAbove = "PurpleBulletAbove_1";

    protected virtual void Awake()
    {
        if (PurpleBulletAboveSpawner.instance != null) Debug.LogError("Only 1 PurpleBulletAboveSpawner allow to exist");
        PurpleBulletAboveSpawner.instance = this;
    }
}
