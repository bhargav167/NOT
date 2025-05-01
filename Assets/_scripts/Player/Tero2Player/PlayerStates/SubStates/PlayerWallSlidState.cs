using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlidState : PlayerTouchingWallState {
    public PlayerWallSlidState (Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base (player, stateMachine, playerData, animBoolName) { }
    public override void LogicUpdate () {
        base.LogicUpdate ();
        if (!IsExistingState) {
               Movement.SetVelocityY (-playerData.wallSlideVelocity *3);
            if (grabInput && yInput == 0) {
                stateMachine.ChangeState (Player.WallGrabState);
            }
        }
    }
}