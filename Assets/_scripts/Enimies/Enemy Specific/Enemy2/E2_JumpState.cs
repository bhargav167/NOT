using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_JumpState : JumpState
{
    private Enemy2 enemy;
    public E2_JumpState(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_JumpState stateData,Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
         //.setVerticallVelocity (stateData.JumpDistance,stateData.JumpDistance);
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
    } 
}
