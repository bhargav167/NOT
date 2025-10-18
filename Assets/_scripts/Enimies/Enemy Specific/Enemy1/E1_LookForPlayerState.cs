using System.Diagnostics;
using Tero.CoreSystem;
using UnityEngine;
public class E1_LookForPlayerState : LookForPlayerState
{
    private Enemy1 enemy;
    protected Movement Movement { get => movement ?? core.getCoreComponents(ref movement); }
    private Movement movement;
    protected KnockBackReceiver Combat { get => combat ?? core.getCoreComponents(ref combat); }
    private KnockBackReceiver combat;
    protected HeadKnockbackReciver HeadCombat { get => headcombat ?? core.getCoreComponents(ref headcombat); }
    private HeadKnockbackReciver headcombat;
    protected Death Death { get => death ?? core.getCoreComponents(ref death); }
    private Death death;
    public E1_LookForPlayerState(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_LookForPlayer stateData,Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy=enemy;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        Movement.SetVelocityX(0f);
    }
    public override void Exist()
    {
        base.Exist();
    } 
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (HeadCombat.isKnockBackActive)
            stateMachine.ChangeState(enemy.headknockState);

       if(isPlayerMinAgroFrontRange || isPlayerMinAgroBackRange)
            stateMachine.ChangeState(enemy.playerDetectedState);
         
        if (Death.IsDead)
            stateMachine.ChangeState(enemy.deadState);

        else if(isAllTurnTimeDone){
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    } 
}
