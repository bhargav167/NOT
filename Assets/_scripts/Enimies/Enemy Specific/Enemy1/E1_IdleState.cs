using UnityEngine;
using Tero.CoreSystem;
public class E1_IdleState : IdleState {
    private Enemy1 enemy;
 
    protected Movement Movement { get => movement ?? core.getCoreComponents(ref movement); }
    private Movement movement;
    protected KnockBackReceiver Combat { get => combat ?? core.getCoreComponents(ref combat); }
    private KnockBackReceiver combat;
    protected HeadKnockbackReciver HeadCombat {get=>headcombat ?? core.getCoreComponents(ref headcombat);} 
    private HeadKnockbackReciver headcombat;
    protected LegsKnockbackReciver LegCombat { get => legcombat ?? core.getCoreComponents(ref legcombat); }
    private LegsKnockbackReciver legcombat;
    protected Death Death { get => death ?? core.getCoreComponents(ref death); }
    private Death death;
    public E1_IdleState (Entity entity, FinateStateMachine stateMachine, string animBoolName, D_IdleState stateData, Enemy1 enemy) : base (entity, stateMachine, animBoolName, stateData) {
        this.enemy = enemy;
    }
    public override void Enter () {
        base.Enter ();
    }
    public override void Exist () {
        base.Exist ();
    }
    public override void LogicUpdate () {
        base.LogicUpdate ();
        if (isPlayerInMinArgoFrontRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        if (isPlayerInMinArgoBackRange)
        {
            stateMachine.ChangeState(enemy.meleeAttactState);
        }
        if (Combat.isKnockBackActive)
        {
            stateMachine.ChangeState(enemy.knockState);
        }
        if (HeadCombat.isKnockBackActive)
        {
            stateMachine.ChangeState(enemy.headknockState);
        }
        if (LegCombat.isKnockBackActive)
        {
            stateMachine.ChangeState(enemy.legknockState);
        }
        if (Death.IsDead && !Death.IsHeadShot)
        {
            stateMachine.ChangeState(enemy.deadState);
        }
        if (Death.IsHeadShot)
        {
            stateMachine.ChangeState(enemy.headshotState);
        }
        else if (isIdleTimeOver) {
            stateMachine.ChangeState (enemy.moveState);
        }
    }

    public override void PhysicsUpdate () {
        base.PhysicsUpdate ();
    }
}