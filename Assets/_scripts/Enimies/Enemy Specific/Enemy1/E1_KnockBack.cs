using System;
using Tero.CoreSystem;
using UnityEngine;
public class E1_KnockBack :KnockBack
{ 
    private Enemy1 _enemy;
    protected KnockBackReceiver Combat {get=>combat ?? core.getCoreComponents(ref combat);} 
    private KnockBackReceiver combat;
    protected Death Death { get => death ?? core.getCoreComponents(ref death); }
    private Death death;

    private float knockbacktime = 0;
    public E1_KnockBack (Entity entity, FinateStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Enemy1 enemy) : base (entity, stateMachine, animBoolName, null) {
        this._enemy = enemy;
    }
    public override void DoCheck () {
        base.DoCheck ();
    }
    public override void Enter()
    {
        base.Enter();
        entity.originalPosition = entity.policeTransform.transform.position;
        knockbacktime = entity.atsm.AnimationRunTime(entity.anim.runtimeAnimatorController, "Knockback");
    }
    public override void Exist (){
        base.Exist ();
    }
    public override void LogicUpdate () {
        base.LogicUpdate ();
        knockbacktime -= Time.deltaTime;
        if (knockbacktime > 0) return;
        if (isPlayerIsInCloseRangeAction)
            stateMachine.ChangeState(_enemy.meleeAttactState);

        if (ishiding)
            stateMachine.ChangeState(_enemy.hideState);

        else stateMachine.ChangeState(_enemy.stunState);

    }

    public override void PhysicsUpdate () {
        base.PhysicsUpdate ();
    }
} 