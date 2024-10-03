using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : Spawner
{
    private static BulletSpawner instance;
    public static BulletSpawner Instance => instance;

    public static string bullet = "Bullet_1";

    protected virtual void Awake()
    {
        if (BulletSpawner.instance != null) Debug.LogError("Only 1 BulletSpawner allow to exist");
        BulletSpawner.instance = this;
    }
}
