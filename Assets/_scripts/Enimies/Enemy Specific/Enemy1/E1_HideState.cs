using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tero.CoreSystem;
using Tero;
public class E1_HideState : HideState
{
    private Enemy1 enemy;
    protected new Movement Movement { get => movement ?? core.getCoreComponents(ref movement); }
    private Movement movement;
    protected KnockBackReceiver Combat { get => combat ?? core.getCoreComponents(ref combat); }
    private KnockBackReceiver combat;
    public E1_HideState(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_HideStateData stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
    public override void Exist(){
        base.Exist();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isHidden)
        {
            stateMachine.ChangeState(enemy.hiddenState);
            isHidden = false;
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    } 
} 