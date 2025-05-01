using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : States
{
    protected bool isPlayerIsInMinAgroFrontRange;
    protected bool isPlayerIsInMinAgroBackRange;
    protected bool isPlayerIsInMaxAgroBackRange;
    public KnockBack(Entity entity, FinateStateMachine stateMachine, string animBoolName, Transform attactPosition) : base(entity, stateMachine, animBoolName)
    {
    }
     public override void DoCheck () {
        base.DoCheck ();
        isPlayerIsInMinAgroFrontRange = entity.CheckPlayerInFrontMinAgroRange();
        isPlayerIsInMinAgroBackRange = entity.CheckPlayerInBackMinAgroRange();
        isPlayerIsInMaxAgroBackRange = entity.CheckPlayerInMaxAgroRange();
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
    public virtual void TriggerAttact () {
    }
    public virtual void FinishAttact () {
    }
}
