using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState {
    public PlayerIdleState (Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base (player, stateMachine, playerData, animBoolName) { }

    public override void DoCheck () {
        base.DoCheck ();
    }

    public override void Enter () {
        base.Enter ();
          Movement?.SetVelocityX (0.0f);
    }
    public override void Exit () {
        base.Exit ();
    }
    public override void LogicUpdate () {
        base.LogicUpdate ();
        if (!IsExistingState) {
            if (xInput != 0.0f) {
                stateMachine.ChangeState (Player.MoveState);
            } else if (yInput == -1) {
                stateMachine.ChangeState (Player.CrouchIdleState);
            }
        }
    }
    public override void PhysicsUpdate () {
        base.PhysicsUpdate ();
    }
}