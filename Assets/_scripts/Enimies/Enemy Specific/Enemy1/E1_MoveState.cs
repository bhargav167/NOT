using System.Collections;
using System.Collections.Generic;
using Tero;
using Tero.CoreSystem;
using UnityEngine;
public class E1_MoveState : MoveState
{
    private Enemy1 enemy;
    public E1_MoveState(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_MoveState stateData,Enemy1 enemy) 
    : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy=enemy;
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
        if (ishiding && isPlayerMaxAgroRange)
            stateMachine.ChangeState(enemy.hideState);
           
        if (isPlayerMinAgroFrontRange)
            stateMachine.ChangeState(enemy.playerDetectedState);

        if (isPlayerMinAgroBackRange)
            stateMachine.ChangeState(enemy.meleeAttactState);

        if (HeadCombat.isKnockBackActive)
            stateMachine.ChangeState(enemy.headknockState);

        if (LegCombat.isKnockBackActive)
            stateMachine.ChangeState(enemy.legknockState);

        if (Combat.isKnockBackActive)
        {
            enemy.anim.SetInteger(PoliceAnimatinName.hurtType.ToString(), 1);
            stateMachine.ChangeState(enemy.knockState);
        }
        if (Combat1.isKnockBackActive)
        {
            enemy.anim.SetInteger(PoliceAnimatinName.hurtType.ToString(), 2);
            stateMachine.ChangeState(enemy.knockState);
        }

        if (Combat.isKnockBackByGranadeActive)
            stateMachine.ChangeState(enemy.granadeknockState);

        if (Death.IsDead && !Death.IsHeadShot)
            stateMachine.ChangeState(enemy.deadState);

        if (Death.IsHeadShot)
            stateMachine.ChangeState(enemy.headshotState);

        else if (isDetectedWall || isDetectedLedger){
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    } 
}
