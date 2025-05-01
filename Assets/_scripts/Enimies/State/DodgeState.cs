using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tero.CoreSystem;
public class DodgeState : States
{
    protected CollisionSences CollisionSences {get=>collisionSences ?? core.getCoreComponents(ref collisionSences);} 
    private CollisionSences collisionSences;
    protected D_DodgeState stateData;
    protected bool performCloseRangeAction;
    protected bool isPlayerInMaxAgroRange;
    protected bool isGrounded;
    protected bool isDodgeOver;
    public DodgeState(Entity entity, FinateStateMachine stateMachine, string animBoolName,D_DodgeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData=stateData;
    }

    public override void DoCheck()
    {
        base.DoCheck();

        performCloseRangeAction=entity.CheckPlayerInCloseRangeAction();
        isPlayerInMaxAgroRange=entity.CheckPlayerInMaxAgroRange();
        if(CollisionSences){
            isGrounded=CollisionSences.Wall;
        }
    }

    public override void Enter()
    {
        base.Enter();
        isDodgeOver=false;
       // entity.setVelocity(stateData.dodgeSpeed,stateData.dodgeAngel,-entity.facingDirection);
    } 
    public override void Exist()
    {
        base.Exist();
    } 
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time>=startTime+stateData.dodgeTime && isGrounded){
            isDodgeOver=true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    } 
}
