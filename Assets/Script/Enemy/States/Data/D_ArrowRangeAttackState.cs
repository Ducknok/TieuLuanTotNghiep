using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newArrowRangeAttackStateData", menuName = "Data/State Data/Arrow Range Attack State")]
public class D_ArrowRangeAttackState : ScriptableObject
{
    public GameObject projectile;
    public float projectileDamage = 10f;
    public float projectileSpeed = 12f;
    public float projectileTravelDistance;
}
