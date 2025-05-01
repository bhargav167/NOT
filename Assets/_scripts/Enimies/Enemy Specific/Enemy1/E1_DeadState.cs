using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_DeadState:DeadState
{
    private Enemy1 enemy;
    public E1_DeadState (Entity entity, FinateStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Enemy1 enemy) : base (entity, stateMachine, animBoolName, null) {
        this.enemy = enemy;
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
}
