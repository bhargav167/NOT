using System.Collections;
using System.Collections.Generic;
using Tero.CoreSystem;
using UnityEngine;

public class StunState : States
{
    protected D_StunState stateData;
    protected bool isStunTimeOver;
    protected bool isGrounded;
    protected bool isMovementStopped;
    protected bool performCloseRangeAction;
    protected bool isPlayerInMinAgroFrontRange;
    protected bool isPlayerInMinAgroBackRange;
    public StunState(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_StunState stateData) : base(entity, stateMachine, animBoolName)
    {
         this.stateData = stateData;
    }

    public override void DoCheck()
    {
        base.DoCheck();
      //  isGrounded = core.CollisionSenses.Ground;
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMinAgroFrontRange = entity.CheckPlayerInFrontMinAgroRange();
        isPlayerInMinAgroBackRange = entity.CheckPlayerInBackMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
         isStunTimeOver = false;
        isMovementStopped = false;
        Debug.Log("stunt");
       // core.Movement.SetVelocity(stateData.stunKnockbackSpeed, stateData.stunKnockbackAngle, entity.lastDamageDirection);
    } 
    public override void Exist()
    {
        base.Exist();
          if(Time.time >= startTime + stateData.stunTime)
        {
            isStunTimeOver = true;
        }

        if(isGrounded && Time.time >= startTime + stateData.stunKnockbackTime && !isMovementStopped)
        {
            isMovementStopped = true;
           
        }
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
