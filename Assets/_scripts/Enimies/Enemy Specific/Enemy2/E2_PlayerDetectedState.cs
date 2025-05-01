using Tero.CoreSystem;
using UnityEngine;
public class E2_PlayerDetectedState : PlayerDetectState {
    private Enemy2 enemy;
    protected KnockBackReceiver Combat { get => combat ?? core.getCoreComponents(ref combat); }
    private KnockBackReceiver combat;
    protected Death Death { get => death ?? core.getCoreComponents(ref death); }
    private Death death;
    public E2_PlayerDetectedState (Entity entity, FinateStateMachine stateMachine, string animBoolName, D_PlayerDetctData stateData, Enemy2 enemy) : base (entity, stateMachine, animBoolName, stateData) {
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
        if (performCloseRangeAction) {
            if (Time.time >= enemy.dodgeState.startTime + enemy.dodgeStateData.dodgeCoolDown) {
                stateMachine.ChangeState (enemy.dodgeState);
            } else {
                stateMachine.ChangeState (enemy.meeleAttactState);
            }
            } else if (performLongRangAction) {
                stateMachine.ChangeState (enemy.rangedAttactState);
            }
            if (Combat.isKnockBackActive)
            {
                stateMachine.ChangeState(enemy.knockState);
            }
            else if (!isPlayerMaxAgroRange) {
                stateMachine.ChangeState (enemy.moveState);
            }
        if (Death.IsDead && !Death.IsHeadShot)
        {
            stateMachine.ChangeState(enemy.deadState);
        }
        if (Death.IsHeadShot)
        {
            stateMachine.ChangeState(enemy.headshotState);
        }
    }

    public override void PhysicsUpdate () {
        base.PhysicsUpdate ();
    }
}