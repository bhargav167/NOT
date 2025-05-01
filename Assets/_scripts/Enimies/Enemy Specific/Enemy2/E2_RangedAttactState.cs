using Tero.CoreSystem;
using UnityEngine;

public class E2_RangedAttactState : RangedAttactState {
    private Enemy2 enemy;
    protected KnockBackReceiver Combat { get => combat ?? core.getCoreComponents(ref combat); }
    private KnockBackReceiver combat;
    protected Death Death { get => death ?? core.getCoreComponents(ref death); }
    private Death death;
    public E2_RangedAttactState (Entity entity, FinateStateMachine stateMachine, string animBoolName, Transform attactPosition, D_RangedAttactState stateDate, Enemy2 enemy) : base (entity, stateMachine, animBoolName, attactPosition, stateDate) {
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

    public override void FinishAttact () {
        base.FinishAttact ();
    }

    public override void LogicUpdate () {
        base.LogicUpdate ();
        if (isAnimationFinished) {
            if (isPlayerIsInMinAgroFrontRange || isPlayerIsInMinAgroBackRange || isPlayerIsInMaxAgroBackRange) {
                stateMachine.ChangeState (enemy.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
            if (Combat.isKnockBackActive)
            {
                stateMachine.ChangeState(enemy.knockState);
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
    }

    public override void PhysicsUpdate () {
        base.PhysicsUpdate ();
    }

    public override void TriggerAttact () {
        base.TriggerAttact ();
    }
}