using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackState", menuName = "Data/State Data/Melee Attack State")]
public class D_MeleeAttack : ScriptableObject
{
    public LayerMask whatIsPlayer;
    public float attackRadius = 0.5f;
    public float attackDamage = 10f;
    
}
