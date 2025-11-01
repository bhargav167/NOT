using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tero.CoreSystem;
public class AttactState : States {
    protected Movement Movement {get=>movement ?? core.getCoreComponents(ref movement);} 
    private Movement movement;
    protected Transform attactPosition;
    protected bool isAnimationFinished;
    protected bool isPlayerIsInMinAgroFrontRange;
    protected bool isPlayerIsInMinAgroBackRange;
    protected bool isPlayerIsInMaxAgroBackRange;
    public AttactState (Entity entity, FinateStateMachine stateMachine, string animBoolName, Transform attactPosition) : base (entity, stateMachine, animBoolName) {
        this.attactPosition = attactPosition;
    }

    public override void DoCheck () {
        base.DoCheck ();
        isPlayerIsInMinAgroFrontRange = entity.CheckPlayerInFrontMinAgroRange();
        isPlayerIsInMinAgroBackRange = entity.CheckPlayerInBackMinAgroRange();
        isPlayerIsInMaxAgroBackRange = entity.CheckPlayerInMaxAgroRange();
    }

    public override void Enter () {
        base.Enter ();
        entity.atsm.attackState=this;
         Movement.SetVelocityX(0f);
        isAnimationFinished=false;
    }
    public override void Exist (){
        base.Exist ();
    }
    public override void LogicUpdate (){
        base.LogicUpdate ();
        Movement.SetVelocityX(0f);
    }

    public override void PhysicsUpdate () {
        base.PhysicsUpdate ();
    }
    public virtual void TriggerAttact () {
    }
    public virtual void FinishGranadeKnockback()
    {
    }
    public virtual void FinishAttact () {
        isAnimationFinished=true;
    }
}