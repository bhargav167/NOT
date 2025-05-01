using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tero.CoreSystem;
public class PlayerDetectState : States {
    protected Movement Movement { get => movement ?? core.getCoreComponents(ref movement); }
    protected CollisionSences CollisionSences { get => collisionSences ?? core.getCoreComponents(ref collisionSences); }
    private Movement movement;
    private CollisionSences collisionSences;
    protected Stats Stats { get => stats ?? core.getCoreComponents(ref stats); }
    private Stats stats;
    protected D_PlayerDetctData stateData;
    protected bool isPlayerMinAgroFrontRange;
    protected bool isPlayerMinAgroBackRange;
    protected bool isPlayerMaxAgroRange;
    protected bool performLongRangAction;
    protected bool performCloseRangeAction;
    protected bool isDetectedLedger;
    
    public PlayerDetectState (Entity entity, FinateStateMachine stateMachine, string animBoolName, D_PlayerDetctData stateData) : base (entity, stateMachine, animBoolName) {
        this.stateData = stateData;
    }

    public override void DoCheck () {
        base.DoCheck ();
         if(CollisionSences){
            isDetectedLedger = CollisionSences.LedgeVertical;
        }
        isPlayerMinAgroFrontRange = entity.CheckPlayerInFrontMinAgroRange();
        isPlayerMinAgroBackRange = entity.CheckPlayerInBackMinAgroRange();
        isPlayerMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter () {
        base.Enter ();
       
        performLongRangAction =false; 
         Movement.SetVelocityX (0f);
    }
    public override void Exist () {
        base.Exist ();
    }
    public override void LogicUpdate () {
        base.LogicUpdate ();
        Movement.SetVelocityX (0f);
        if(Time.time>=startTime+stateData.longRangeActionTime){
            performLongRangAction=true;
        }
    }

    public override void PhysicsUpdate () {
        base.PhysicsUpdate ();
    }
}