using UnityEngine;
[CreateAssetMenu(fileName ="meleeStateData",menuName ="Data/State Data/MeleeAttact State")]

public class D_MeleeAttact : ScriptableObject
{ 
    public float attactRadius=0.5f;
    public float attactDamage=5.0f;
    public Vector2 knockbackAngle= Vector2.one;
    public float knockbackStrength=6;
    public LayerMask whatIsPlayer;
    public float PoiseDamage;
}
