using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tero.CoreSystem;
using Tero.Weapons;

public class PlayerAbitityState : PlayerState {
    protected bool isAbilityDone;
    protected Movement Movement {get=>movement ?? core.getCoreComponents(ref movement);} 
    protected CollisionSences CollisionSences {get=>collisionSences ?? core.getCoreComponents(ref collisionSences);}
    private Movement movement;
    private CollisionSences collisionSences;
    private bool isGrounded;
    public PlayerAbitityState (Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base (player, stateMachine, playerData, animBoolName) { }
    public override void DoCheck () {
        base.DoCheck ();
        if(CollisionSences){
            isGrounded =  CollisionSences.Grounded;
        }
    }
    public override void Enter () {
        base.Enter ();
        isAbilityDone = false; 
    }
    public override void Exit () {
        base.Exit ();
    }
    public override void LogicUpdate () {
        base.LogicUpdate ();
        if (isAbilityDone) {
            if (isGrounded && Player.CurrectVelocity.y < 0.01f) { 
                stateMachine.ChangeState (Player.IdleState);
            }else {
                stateMachine.ChangeState (Player.InAirState);
            }
        }
    }

    public override void PhysicsUpdate () {
        base.PhysicsUpdate ();
    }
      public virtual void TriggerAttact () {

    }
}