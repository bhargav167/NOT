using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState :States{
   
    public DeadState(Entity entity, FinateStateMachine stateMachine, string animBoolName, Transform attactPosition) : base(entity, stateMachine, animBoolName)
    {
    }
      public override void DoCheck () {
        base.DoCheck ();
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