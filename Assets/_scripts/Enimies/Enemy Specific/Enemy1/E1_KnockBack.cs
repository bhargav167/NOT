using UnityEngine;
using Tero.CoreSystem;
public class E1_KnockBack :KnockBack
{ 
    private Enemy1 _enemy;
    protected KnockBackReceiver Combat {get=>combat ?? core.getCoreComponents(ref combat);} 
    private KnockBackReceiver combat;
    protected Death Death { get => death ?? core.getCoreComponents(ref death); }
    private Death death;
    public E1_KnockBack (Entity entity, FinateStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Enemy1 enemy) : base (entity, stateMachine, animBoolName, null) {
        this._enemy = enemy;
    }
    public override void DoCheck () {
        base.DoCheck ();
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exist () {
       
        base.Exist ();
    }
    public override void LogicUpdate () {
        base.LogicUpdate ();
        if (isPlayerIsInMinAgroFrontRange || isPlayerIsInMinAgroBackRange || isPlayerIsInMaxAgroBackRange)
            {
                stateMachine.ChangeState(_enemy.meleeAttactState);
            }
            else
            {
                stateMachine.ChangeState(_enemy.lookingForPlayerState);
            }
    }

    public override void PhysicsUpdate () {
        base.PhysicsUpdate ();
    }
} 