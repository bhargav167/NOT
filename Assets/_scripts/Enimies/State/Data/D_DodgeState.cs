 
using UnityEngine;
[CreateAssetMenu(fileName ="newDodgeStateData",menuName ="Data/ChrageState Data/Dodge Data")]
public class D_DodgeState : ScriptableObject
{ 
    public float dodgeSpeed=10f;
    public float dodgeTime=0.2f;
    public float dodgeCoolDown=2f;
    public Vector2 dodgeAngel;
}
