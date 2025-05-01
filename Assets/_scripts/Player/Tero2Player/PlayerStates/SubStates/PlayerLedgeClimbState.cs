using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tero.CoreSystem;
public class PlayerLedgeClimbState : PlayerState {
    protected Movement Movement {get=>movement ?? core.getCoreComponents(ref movement);} 
    protected CollisionSences CollisionSences {get=>collisionSences ?? core.getCoreComponents(ref collisionSences);} 
    private Movement movement;
    private CollisionSences collisionSences;
    private Vector2 detectedPos;
    private Vector2 cornorPos;
    private Vector2 workSpace;
    private Vector2 startPos;
    private Vector2 stopPos;
    private bool isHanging;
    private bool isClimbing;
    private int xInput;
    private int yInput;
     
    public PlayerLedgeClimbState (Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base (player, stateMachine, playerData, animBoolName) { }

    public override void AnimationFinishTrigger() {
        base.AnimationFinishTrigger();
        Player.animator.SetBool ("climbLedge", false);
    }

    public override void AnimationTrigger (){
        base.AnimationTrigger ();
        isHanging = true;
    }

    public override void DoCheck () {
        base.DoCheck ();
    }

    public override void Enter () {
        base.Enter ();
        Movement.SetVelocityZero ();
        Player.transform.position = detectedPos;
        cornorPos = DeterminCornerPosition ();

        startPos.Set (cornorPos.x - (Movement.FacingDirection * playerData.startOffSet.x), cornorPos.y - playerData.startOffSet.y);
        stopPos.Set (cornorPos.x + (Movement.FacingDirection * playerData.stopOffset.x), cornorPos.y + playerData.stopOffset.y);
        Player.transform.position = startPos;
    }

    public override void Exit () {
        base.Exit (); 
        isHanging = false;
        if (isClimbing) {
            Player.transform.position = stopPos;
            isClimbing = false;
        }
    }
    public override void LogicUpdate () {
        base.LogicUpdate ();
        if (isAnimationFinished ){ 
            stateMachine.ChangeState (Player.IdleState);
        } else{
            xInput = Player.InputHandeler.NormInputx;
            yInput = Player.InputHandeler.NormInputy;
            Movement.SetVelocityZero ();
            Player.transform.position = startPos;

            if (xInput == Movement.FacingDirection && isHanging && !isClimbing) {
                isClimbing = true;
                Player.animator.SetBool ("climbLedge", true);
            }
            else if (yInput == -1 && isHanging && !isClimbing) {
                stateMachine.ChangeState (Player.InAirState);
            }
        }
    }

    public override void PhysicsUpdate () {
        base.PhysicsUpdate ();
    }

    public void SetDetectedPosition (Vector2 pos) => detectedPos = pos;
    private Vector2 DeterminCornerPosition () {
        RaycastHit2D xHit = Physics2D.Raycast (CollisionSences.WallCheck.position, Vector2.right * Movement.FacingDirection, CollisionSences.WallCheckDistance, CollisionSences.WhatIsGround);
        float xDis = xHit.distance;
        workSpace.Set ((xDis + 0.05f) *  Movement.FacingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast (CollisionSences.LedgeCheckHorizontal.position + (Vector3) (workSpace), Vector2.down, CollisionSences.LedgeCheckHorizontal.position.y - CollisionSences.WallCheck.position.y + 0.05f, CollisionSences.WhatIsGround);
        float yDis = yHit.distance;
        workSpace.Set (CollisionSences.WallCheck.position.x + (xDis *  Movement.FacingDirection), CollisionSences.LedgeCheckHorizontal.position.y - yDis);
        return workSpace;
    } 
}