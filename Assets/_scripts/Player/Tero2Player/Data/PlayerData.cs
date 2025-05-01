using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject {
  [Header ("Move State")]
  public float movementVelocity = 4.4f;
  [Header ("Jump State")]
  public float jumpVelocity = 8f;
  
  [Header ("Wall Jump State")]
  public float wallJumpVelocity = 20;
  public float wallJumpTime = 0.4f;
  public Vector2 wallJumpAngle=new Vector2(1,2);
  
  public float coyoteTime=1f;
  public float variableJumpHeightMultiplier = 0.5f;

  [Header ("Wall Slide State")]
  public float wallSlideVelocity = 3f;
  [Header ("Wall Climb State")]
  public float wallClimbVelocity = 3.0f;

  [Header("Ledge Climb State")]
  public Vector2 startOffSet;
  public Vector2 stopOffset;

  //Shooting data
  [Header("Shooting State")] 
  public GameObject projectile; 
  public float projectileDamage = 10f;
  public float projectileSpeed = 12f;
  public float projectileTravelDistance;

  [Header("Stun State")]
  public float stunTime = 2f;

}