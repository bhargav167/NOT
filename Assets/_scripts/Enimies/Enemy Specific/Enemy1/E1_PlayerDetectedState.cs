using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tero.CoreSystem;
public class E1_PlayerDetectedState : PlayerDetectState
{
    private Enemy1 enemy;
    protected new Movement Movement {get=>movement ?? core.getCoreComponents(ref movement);} 
    private Movement movement;
    protected KnockBackReceiver Combat { get => combat ?? core.getCoreComponents(ref combat); }
    private KnockBackReceiver combat;
    public E1_PlayerDetectedState(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_PlayerDetctData stateData,Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy=enemy;
    }
    public override void Enter()
    {
        base.Enter();
    } 
    public override void Exist(){
        base.Exist();
    } 
    public override void LogicUpdate(){
        base.LogicUpdate();
        if (Combat.isKnockBackActive)
        {
            stateMachine.ChangeState(enemy.knockState);
        }
        if (ishiding){
            stateMachine.ChangeState(enemy.hideState);
        }
        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.meleeAttactState);
        }
        if (isPlayerMinAgroFrontRange)
        {
            stateMachine.ChangeState(enemy.chargeState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    } 
}
