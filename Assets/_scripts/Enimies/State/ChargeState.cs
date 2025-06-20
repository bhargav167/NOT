using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tero.CoreSystem;
using Tero;
public class ChargeState : States {
     protected Movement Movement {get=>movement ?? core.getCoreComponents(ref movement);} 
    protected CollisionSences CollisionSences {get=>collisionSences ?? core.getCoreComponents(ref collisionSences);} 
    private Movement movement;
    private CollisionSences collisionSences;
    protected D_ChargeState stateData;
    protected bool isPlayerMinAgroFrontRange;
    protected bool isPlayerMinAgroBackRange; 
    protected bool isDetectedLedge;
    protected bool isDetectedWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;
    protected bool ishiding;
    protected RaycastHit2D closestHit;
    
    public ChargeState(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_ChargeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoCheck () {
        base.DoCheck ();
        isPlayerMinAgroFrontRange = entity.CheckPlayerInFrontMinAgroRange();
        isPlayerMinAgroBackRange = entity.CheckPlayerInBackMinAgroRange();
        performCloseRangeAction =entity.CheckPlayerInCloseRangeAction();
        closestHit = entity.GetClosestHitFromPlayerCheck();
        if (CollisionSences)
        {
            isDetectedWall = CollisionSences.Wall;
            isDetectedLedge = CollisionSences.LedgeVertical;
        }
    }

    public override void Enter()
    {
        base.Enter();
        isChargeTimeOver = false;
        Movement.SetVelocityX(stateData.chargeSpeed * Movement.FacingDirection);
    }

    public override void Exist () {
        base.Exist ();
    }

    public override void LogicUpdate () {
        base.LogicUpdate ();
        if (!ishiding)
        {
            Movement.SetVelocityX(stateData.chargeSpeed * Movement.FacingDirection);
            if (Time.time >= startTime + stateData.chargeTime)
            {
                isChargeTimeOver = true;
            }
        }
        else
        {
            Movement.SetVelocityX(0f); 
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
         if (closestHit.collider != null && isPlayerMinAgroFrontRange)
        {
            ishiding = closestHit.collider.gameObject.layer == LayerMask.NameToLayer("HideObject");
        }
    }
}