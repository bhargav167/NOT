using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class E1_JumpState : JumpState{
    private Enemy1 enemy;
    protected float jumpUp = 0.5f;
    public E1_JumpState(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_JumpState stateData,Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData){
        this.enemy=enemy;
    }
    public override void Enter(){
        base.Enter();
    }
    public override void Exist(){
        base.Exist();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        jumpUp -= Time.deltaTime;
        if(jumpUp < 0f){
            Movement.SetjumpVelocityY(-stateData.velocity, stateData.jumpInX* Time.deltaTime, stateData.jumpInY);
        }else{
            Movement.SetjumpVelocityY(stateData.velocity, stateData.jumpInX*Time.deltaTime, stateData.jumpInY);
        }

        if (isDetectedLedger)
        {
            stateMachine.ChangeState(enemy.chargeState);
            jumpUp = 0.5f;
        }
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    } 
}
