using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpawner : Spawner
{
    private static SkeletonSpawner instance;
    public static SkeletonSpawner Instance => instance;

    public static string skeleton = "Enemy_1";

    protected virtual void Awake()
    {
        if (SkeletonSpawner.instance != null) Debug.LogError("Only 1 Skeleton allow to exist");
        SkeletonSpawner.instance = this;
    }
}
