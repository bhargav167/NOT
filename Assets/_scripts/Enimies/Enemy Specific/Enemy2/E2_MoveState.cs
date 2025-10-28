using System.Collections;
using System.Collections.Generic;
using Tero.CoreSystem;
using UnityEngine;

public class E2_MoveState : MoveState
{
    private Enemy2 enemy;
    protected KnockBackReceiver Combat { get => combat ?? core.getCoreComponents(ref combat); }
    private KnockBackReceiver combat;
    protected Death Death { get => death ?? core.getCoreComponents(ref death); }
    private Death death;
    public E2_MoveState(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_MoveState stateData,Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy=enemy;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
    } 
    public override void Exist()
    {
        base.Exist();
    } 
    public override void LogicUpdate()
    {
        base.LogicUpdate();
       
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
         if(isPlayerMinAgroFrontRange || isPlayerMinAgroBackRange && !isDetectedLedger){
            stateMachine.ChangeState(enemy.jumpState);
        }
    } 
}
