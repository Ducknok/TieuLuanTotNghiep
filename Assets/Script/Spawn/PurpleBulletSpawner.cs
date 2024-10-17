using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBulletSpawner : Spawner
{
    private static PurpleBulletSpawner instance;
    public static PurpleBulletSpawner Instance => instance;

    public static string purpleBullet = "PurpleBullet_1";

    protected virtual void Awake()
    {
        if (PurpleBulletSpawner.instance != null) Debug.LogError("Only 1 PurpleBulletSpawner allow to exist");
        PurpleBulletSpawner.instance = this;
    }
}
