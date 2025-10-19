using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tero.CoreSystem;
public class E1_MoveState : MoveState
{
    private Enemy1 enemy;
    private Movement Movement { get => movement ?? core.getCoreComponents(ref movement); }
    private Movement movement;
    private Stats Stats { get => stats ?? core.getCoreComponents(ref stats); }
    private Stats stats;
    protected Death Death { get => death ?? core.getCoreComponents(ref death); }
    private Death death;
    protected KnockBackReceiver Combat {get=>combat ?? core.getCoreComponents(ref combat);} 
    private KnockBackReceiver combat;
    protected HeadKnockbackReciver HeadCombat {get=>headcombat ?? core.getCoreComponents(ref headcombat);} 
    private HeadKnockbackReciver headcombat;
    protected LegsKnockbackReciver LegCombat { get => legcombat ?? core.getCoreComponents(ref legcombat); }
    private LegsKnockbackReciver legcombat;

    public E1_MoveState(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_MoveState stateData,Enemy1 enemy) 
    : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy=enemy;
    }
    public override void Enter()
    {
        base.Enter();
    } 

    public override void Exist()
    {
        base.Exist();
    }

    public override void LogicUpdate()
    { 
        base.LogicUpdate();
        if (ishiding && isPlayerMaxAgroRange)
            stateMachine.ChangeState(enemy.hideState);

        if (isPlayerMinAgroFrontRange)
            stateMachine.ChangeState(enemy.playerDetectedState);

        if (isPlayerMinAgroBackRange)
            stateMachine.ChangeState(enemy.meleeAttactState);

        if (HeadCombat.isKnockBackActive)
            stateMachine.ChangeState(enemy.headknockState);

        if (LegCombat.isKnockBackActive)
            stateMachine.ChangeState(enemy.legknockState);

        if (Combat.isKnockBackActive)
            stateMachine.ChangeState(enemy.knockState);

        if (Combat.isKnockBackByGranadeActive)
            stateMachine.ChangeState(enemy.granadeknockState);

        if (Death.IsDead && !Death.IsHeadShot)
            stateMachine.ChangeState(enemy.deadState);

        if (Death.IsHeadShot)
            stateMachine.ChangeState(enemy.headshotState);

        else if (isDetectedWall || isDetectedLedger){
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    } 
}
