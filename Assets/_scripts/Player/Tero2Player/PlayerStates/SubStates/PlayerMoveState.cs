using UnityEngine;

public class PlayerMoveState : PlayerGroundedState {
    public PlayerMoveState (Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base (player, stateMachine, playerData, animBoolName) { }

    public override void DoCheck () {
        base.DoCheck ();
    }

    public override void Enter () {
        base.Enter ();
    }
    public override void Exit () {
        base.Exit ();
    }
    public override void LogicUpdate () {
        base.LogicUpdate ();
        Movement?.CheckIfShouldFlip (xInput);
        if (!IsExistingState){
            if(xInput != 0.0f && playerData.movementVelocity<1.0f){
                playerData.movementVelocity += playerData.acceleration * Time.deltaTime;
                Player.animator.SetFloat("velocity", playerData.movementVelocity);
                Movement?.SetVelocityX(playerData.movementVelocity*1.4f * xInput);
            }if (xInput == 0){
                stateMachine.ChangeState (Player.IdleState);
            }else if (yInput == -1) {
                stateMachine.ChangeState (Player.CrouchMoveState);
            }
        }
    }
    public override void PhysicsUpdate () {
        base.PhysicsUpdate ();
    }
}