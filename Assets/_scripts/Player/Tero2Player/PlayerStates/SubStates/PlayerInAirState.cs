using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tero.CoreSystem;
public class PlayerInAirState : PlayerState {
     protected Movement Movement {get=>movement ?? core.getCoreComponents(ref movement);} 
    protected CollisionSences CollisionSences {get=>collisionSences ?? core.getCoreComponents(ref collisionSences);} 
    private Movement movement;
    private CollisionSences collisionSences;
    private int xInput;
    private bool grabInput;
    private bool jumpInput;
    private bool isGrounded;
    private bool isJumping;
    private bool wallJumpCoyoteTime;
    private float startWallJumpCyoteTime;
    private bool jumpInputStop;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    private bool isTouchinLedge;

    public PlayerInAirState (Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base (player, stateMachine, playerData, animBoolName) { }

    public override void DoCheck () {
        base.DoCheck ();
        if (CollisionSences){
            // isGrounded = CollisionSences.Grounded;
            // isTouchingWall = CollisionSences.Wall;
            // isTouchingWallBack = CollisionSences.WallBack;
            // isTouchinLedge = CollisionSences.LedgeHorizontal;
            oldIsTouchingWall = isTouchingWall;
            oldIsTouchingWallBack = isTouchingWallBack;
        }
        if(isTouchingWall && !isTouchinLedge){
            Player.LedgeClimbState.SetDetectedPosition(Player.transform.position);
        }
        if(!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack)){
            StartWallJumpCyoteTime();
        }
    }

    public override void Enter () {
        base.Enter ();
    }
    public override void Exit () {
        base.Exit ();
        oldIsTouchingWallBack=false;
        oldIsTouchingWall=false;
        isTouchingWall=false;
        isTouchingWallBack=false;
    }
    public override void LogicUpdate () {
        base.LogicUpdate ();
        xInput = Player.InputHandeler.NormInputx;
        jumpInput=Player.InputHandeler.JumpInput;
        jumpInputStop = Player.InputHandeler.JumpInputStop;
        grabInput=Player.InputHandeler.GrabInput;
        CheckWallJumpCyoteTime();
        CheckJumpMultiplier ();
          if (Player.InputHandeler.AttackInput[(int) CombatInputs.primary] && !isTouchinLedge) {
            stateMachine.ChangeState (Player.PrimaryAttackState);
        } else if (Player.InputHandeler.AttackInput[(int) CombatInputs.secondry] && !isTouchinLedge) {
            stateMachine.ChangeState (Player.SecondryAttackState);
        }
      else if (isGrounded && Player.CurrectVelocity.y < 0.1f) {
            stateMachine.ChangeState (Player.IdleState);
        }
       else if(isTouchingWall && !isTouchinLedge && !isGrounded){ 
            stateMachine.ChangeState(Player.LedgeClimbState);
        }
        else if(jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime)){
            StopWallJumpCyoteTime();
           // isTouchingWall=CollisionSences.Wall;
            Player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(Player.WallJumpState);
        }
        else if(isTouchingWall && grabInput && isTouchinLedge){
            stateMachine.ChangeState(Player.WallGrabState);
        }
        
        
         else if (isTouchingWall && xInput ==  Movement.FacingDirection && Player.CurrectVelocity.y<=0.0f) {
            stateMachine.ChangeState(Player.WallSlideState);
        } else  {
              Movement.CheckIfShouldFlip (xInput);
            Movement.SetVelocityX (playerData.movementVelocity * xInput);
            Player.animator.SetFloat ("yVelocity", Mathf.Abs( movement.CurrectVelocity.y));
            Player.animator.SetFloat ("xVelocity", Mathf.Abs (movement.CurrectVelocity.x)); 
        }
    }
    private void CheckJumpMultiplier () {
        if (isJumping) {
            if (jumpInputStop) {
                  Movement.SetVelocityY (Player.CurrectVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            } else if (Player.CurrectVelocity.y <= 0f) {
                isJumping = false;
            }
        }
    }

    private void CheckWallJumpCyoteTime(){
        if(wallJumpCoyoteTime && Time.time>startWallJumpCyoteTime+playerData.coyoteTime){
            wallJumpCoyoteTime=false; 
        }
    }
         public void StartWallJumpCyoteTime(){
             wallJumpCoyoteTime=true;
             startWallJumpCyoteTime=Time.time;
         }
    public void StopWallJumpCyoteTime()=>wallJumpCoyoteTime=false;
    public override void PhysicsUpdate () {
        base.PhysicsUpdate ();
    }
    public void SetIsJumping () => isJumping = true;
}