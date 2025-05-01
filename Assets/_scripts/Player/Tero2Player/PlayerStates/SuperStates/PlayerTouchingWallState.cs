using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tero.CoreSystem;
public class PlayerTouchingWallState : PlayerState
{
    protected Movement Movement { get => movement ?? core.getCoreComponents(ref movement); }
    protected CollisionSences CollisionSences { get => collisionSences ?? core.getCoreComponents(ref collisionSences); }
    private Movement movement;
    private CollisionSences collisionSences;
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool grabInput;
    protected bool JumpInput;
    protected int xInput;
    protected int yInput;
    protected bool isTouchingLedge;
    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,string animBoolName)
    : base(player, stateMachine, playerData, animBoolName) { }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoCheck()
    {
        base.DoCheck();
        if (CollisionSences)
        {
            isGrounded = CollisionSences.Grounded;
            isTouchingWall = CollisionSences.Wall;
            isTouchingLedge = CollisionSences.LedgeHorizontal;
        }
        if (isTouchingWall && !isTouchingLedge){
            Player.LedgeClimbState.SetDetectedPosition(Player.transform.position);
        }
    }

    public override void Enter()
    {
        base.Enter();
       
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate(){
        base.LogicUpdate();
        xInput = Player.InputHandeler.NormInputx;
        yInput = Player.InputHandeler.NormInputy;
        grabInput = Player.InputHandeler.GrabInput;
        JumpInput = Player.InputHandeler.JumpInput;
        if (JumpInput){
            Player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(Player.WallJumpState);
        }
        else if (isGrounded && !grabInput)
        {
            stateMachine.ChangeState(Player.IdleState);
        }
        else if (!isTouchingWall && (xInput != Movement.FacingDirection && !grabInput))
        {
            stateMachine.ChangeState(Player.InAirState);
        }
        else if (isTouchingWall && !isTouchingLedge)
        {
            stateMachine.ChangeState(Player.LedgeClimbState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}