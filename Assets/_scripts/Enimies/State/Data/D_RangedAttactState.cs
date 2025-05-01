using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "RangedStateData", menuName = "Data/State Data/RangedAttack State")]
public class D_RangedAttactState : ScriptableObject
{ 
    public GameObject projectile;
    public float projectileDamage=10f;
    public float projectileSpeed=12f;
    public float projectileTravelDistance;
}
