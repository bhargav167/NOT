using System.Collections;
using System.Collections.Generic;
using Tero.CoreSystem;
using UnityEngine;


public class E1_StunState : StunState
{
    private Enemy1 enemy;
    protected KnockBackReceiver Combat { get => combat ?? core.getCoreComponents(ref combat); }
    private KnockBackReceiver combat;
    public E1_StunState(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_StunState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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
        //if (Combat.isKnockBackActive == false)
        // {
        //  stateMachine.ChangeState(enemy.meleeAttactState);
        // }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

