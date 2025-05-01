using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_StunState : StunState
{
    private Enemy2 enemy;
    public E2_StunState(Entity entity, FinateStateMachine stateMachine, string animBoolName,D_StunState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
         if (isStunTimeOver)
        {
            if (isPlayerInMinAgroFrontRange || isPlayerInMinAgroBackRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    } 
}
