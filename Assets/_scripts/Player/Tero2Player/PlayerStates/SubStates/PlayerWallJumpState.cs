using UnityEngine;

public class PlayerWallJumpState : PlayerAbitityState {
    private int wallJumpDirections;
    public PlayerWallJumpState (Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base (player, stateMachine, playerData, animBoolName) { }
    public override void Enter () {
        base.Enter (); 
          Movement.SetVelocity(playerData.wallJumpVelocity,playerData.wallJumpAngle,wallJumpDirections);
           Movement.CheckIfShouldFlip(wallJumpDirections);
    }
    public override void LogicUpdate () {
        base.LogicUpdate ();
        Player.animator.SetFloat("yVelocity",Player.CurrectVelocity.y);
        Player.animator.SetFloat("xVelocity",Mathf.Abs(Player.CurrectVelocity.y));
        if(Time.time>=startTime+playerData.wallJumpTime){
            isAbilityDone=true;
        }
    }
    public void DetermineWallJumpDirection (bool isTouchingWall) {
        if (isTouchingWall) {
            wallJumpDirections = -Movement.FacingDirection;
        } else {
            wallJumpDirections = Movement.FacingDirection;
        }
    }
}