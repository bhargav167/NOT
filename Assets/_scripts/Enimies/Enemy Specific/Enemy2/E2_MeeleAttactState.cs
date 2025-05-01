using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_MeeleAttactState : MeleeAttactState
{
    private Enemy2 enemy;
    public E2_MeeleAttactState(Entity entity, FinateStateMachine stateMachine, string animBoolName, Transform attactPosition, D_MeleeAttact stateData,Enemy2 enemy) : base(entity, stateMachine, animBoolName, attactPosition, stateData)
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

    public override void FinishAttact()
    {
        base.FinishAttact();
    } 
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isAnimationFinished){
            if(isPlayerIsInMinAgroFrontRange || isPlayerIsInMinAgroBackRange){
                stateMachine.ChangeState(enemy.playerDetectedState);
            }else if(!isPlayerIsInMinAgroFrontRange || !isPlayerIsInMinAgroBackRange){
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    } 

    public override void TriggerAttact()
    {
        base.TriggerAttact();
    }
}
