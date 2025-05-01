using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState {
    private Vector2 holdPosition;
    public PlayerWallGrabState (Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base (player, stateMachine, playerData, animBoolName) { }

    public override void AnimationFinishTrigger() {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger () {
        base.AnimationTrigger ();
    }

    public override void DoCheck () {
        base.DoCheck ();
    }

    public override void Enter () {
        base.Enter (); 
        holdPosition = Player.transform.position;
        HoldPosition ();
    }
    public override void Exit () {
        base.Exit ();
    }

    public override void LogicUpdate () {
        base.LogicUpdate (); 
        if (!IsExistingState) {
             HoldPosition ();
            if (yInput > 0) {
                stateMachine.ChangeState (Player.WallClimbState);
            } else if (yInput < 0 || !grabInput) {
                stateMachine.ChangeState (Player.WallSlideState);
            }
        }
    }
    private void HoldPosition () {
        Player.transform.position = holdPosition;
          Movement.SetVelocityX (0f);
          Movement.SetVelocityY (0f);
    }

    public override void PhysicsUpdate () {
        base.PhysicsUpdate ();
    }
}