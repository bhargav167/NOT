using UnityEngine;
using Tero.CoreSystem;
public class E1_ChargeState : ChargeState {
    private Enemy1 enemy;
    protected KnockBackReceiver Combat { get => combat ?? core.getCoreComponents(ref combat); }
    private KnockBackReceiver combat;
    protected HeadKnockbackReciver HeadCombat {get=>headcombat ?? core.getCoreComponents(ref headcombat);} 
    private HeadKnockbackReciver headcombat;
    public E1_ChargeState (Entity entity, FinateStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Enemy1 enemy) : base (entity, stateMachine, animBoolName, stateData) {
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
        if (Combat.isKnockBackActive)
        {
            stateMachine.ChangeState(enemy.knockState);
        }
        if (ishiding){
            stateMachine.ChangeState(enemy.hideState);
        }
        if (HeadCombat.isKnockBackActive)
        {
            stateMachine.ChangeState(enemy.headknockState);
        }
        if (performCloseRangeAction) {
            stateMachine.ChangeState(enemy.meleeAttactState);
        } 
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}