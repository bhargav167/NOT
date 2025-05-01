using UnityEngine;

[CreateAssetMenu(fileName ="JumpData",menuName ="Data/State Data/Jump State")]
public class D_JumpState : ScriptableObject
{ 
    public float lookforJumpTime = 3f;
    public float jumpTime=1.5f;
    public float JumpDistance=2.5f;
    public float velocity =3f;
    public float jumpInX =4f;
    public float jumpInY =4f;
}
