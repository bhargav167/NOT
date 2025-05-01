using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tero.CoreSystem;
public class JumpState : States {
     protected Movement Movement {get=>movement ?? core.getCoreComponents(ref movement);} 
     private Movement movement;
     protected CollisionSences CollisionSences {get=>collisionSences ?? core.getCoreComponents(ref collisionSences);} 
     private CollisionSences collisionSences;
    protected D_JumpState stateData;
    protected bool isPlayerMinAgroFrontRange;
    protected bool isPlayerMinAgroBackRange;
    protected bool isPlayerMaxAgroRange;
    protected bool performLongRangAction;
    protected bool performCloseRangeAction;
    protected bool isDetectedLedger;
    public JumpState (Entity entity, FinateStateMachine stateMachine, string animBoolName, D_JumpState stateData) : base (entity, stateMachine, animBoolName) {
        this.stateData = stateData;
    }

    public override void DoCheck(){
        base.DoCheck();
        if (CollisionSences){
            isDetectedLedger = CollisionSences.LedgeVertical;
        }
        isPlayerMinAgroFrontRange = entity.CheckPlayerInFrontMinAgroRange();
        isPlayerMinAgroBackRange = entity.CheckPlayerInBackMinAgroRange();
        isPlayerMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter () {
        base.Enter ();
    }
    public override void Exist () {
        base.Exist ();
    }
    public override void LogicUpdate () {
        base.LogicUpdate ();
       
    }
    public override void PhysicsUpdate () {
        base.PhysicsUpdate ();
       
    }
}