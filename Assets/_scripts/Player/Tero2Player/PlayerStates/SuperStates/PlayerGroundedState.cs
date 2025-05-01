using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tero.CoreSystem;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerState {
    protected int xInput;
    protected int yInput;
    private bool GrabInput;
    private bool JumpInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingLedge;
    private bool isTouchingCelling;
    private bool _isclickOnPlayer = false;
    protected Movement Movement {get => movement ?? core.getCoreComponents<Movement>(ref movement);}
    private Movement movement;
    private CollisionSences CollisionSences{
        get =>collisionSences ?? core.getCoreComponents<CollisionSences>(ref collisionSences);}
    private CollisionSences collisionSences;
    protected KnockBackReceiver Combat {get=>combat ?? core.getCoreComponents(ref combat);} 
    private KnockBackReceiver combat;
    protected Stats Stats {get=>stats ?? core.getCoreComponents(ref stats);} 
    private Stats stats;
    protected Death Death { get => death ?? core.getCoreComponents(ref death); }
    private Death death;
    public PlayerGroundedState (Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base (player, stateMachine, playerData, animBoolName) { }
    public override void DoCheck () {
        base.DoCheck ();
        if (CollisionSences){
            isGrounded = CollisionSences.Grounded;
            isTouchingWall = CollisionSences.Wall;
            isTouchingLedge = CollisionSences.LedgeHorizontal;
            isTouchingCelling = CollisionSences.Ceiling;
        }
    }
    public override void Enter () {
        base.Enter ();
    }
    public override void Exit () {
        base.Exit ();  
    }
    public override void LogicUpdate () {
        base.LogicUpdate ();
        Vector3 mousePosScreen = Mouse.current.position.ReadValue();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePosScreen.x, mousePosScreen.y));
        if ((Movement.FacingDirection==1 && mousePos.x >= Player.transform.position.x) || (Movement.FacingDirection == -1 && mousePos.x <= Player.transform.position.x))
            _isclickOnPlayer = true;
        else
            _isclickOnPlayer = false;

        xInput = Player.InputHandeler.NormInputx;
        yInput = Player.InputHandeler.NormInputy;
        JumpInput = Player.InputHandeler.JumpInput;
        GrabInput = Player.InputHandeler.GrabInput;
         
       
        if (Player.InputHandeler.AttackInput[(int)CombatInputs.primary] && !isTouchingCelling && Player.PrimaryAttackState.CanTransitionToAttackState() && !Combat.isKnockBackActive && _isclickOnPlayer)
        {
            stateMachine.ChangeState(Player.PrimaryAttackState);
        }
        else if (Player.InputHandeler.AttackInput[(int)CombatInputs.secondry])
        {
            stateMachine.ChangeState(Player.SecondryAttackState);
        }
        else if (JumpInput && !Player.KnockBackState)
        {
         stateMachine.ChangeState(Player.JumpState);
        }
        else if (!isGrounded)
        {
            stateMachine.ChangeState(Player.InAirState);
        }
        else if (isTouchingWall && GrabInput)
        {
            stateMachine.ChangeState(Player.WallGrabState);
        }
        else if (Combat.isKnockBackActive)
        {
            stateMachine.ChangeState(Player.KnockBackState);
        }
        else if (Combat.isKnockBackByGranadeActive)
        {
            stateMachine.ChangeState(Player.PlayerKnockByGrenadeState);
        }
        else if (Death.IsDead)
        {
            stateMachine.ChangeState(Player.DeadState);
        }

    }
    public override void PhysicsUpdate () {
        base.PhysicsUpdate ();
    }
}