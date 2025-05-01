using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tero.CoreSystem;
public class PlayerJumpState : PlayerAbitityState
{
    protected float jumpDelay=0.1f;
    protected KnockBackReceiver Combat { get => combat ?? core.getCoreComponents(ref combat); }
    private KnockBackReceiver combat;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        Player.InputHandeler.UseJumpInput();
    } 
    public override void Exit(){
        base.Exit();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        if(!Player.KnockBackState){
             jumpDelay -= Time.deltaTime;
        if (jumpDelay < 0f){
            jumpDelay = 0.1f;
            Movement.SetVelocityY(playerData.jumpVelocity);
            isAbilityDone = true;
            Player.InAirState.SetIsJumping();
        }
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    } 
}
