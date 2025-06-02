using UnityEngine;
using Tero.CoreSystem;

public class DeadState :States{
    protected Movement Movement {get=>movement ?? core.getCoreComponents(ref movement);}
    private Movement movement;
    public DeadState(Entity entity, FinateStateMachine stateMachine, string animBoolName, Transform attactPosition) : base(entity, stateMachine, animBoolName)
    {}
    public override void DoCheck()
    {
        base.DoCheck();
        Movement.SetVelocity(0, Vector2.zero, 0);
        Movement.CanSetVelocity = false;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public virtual void TriggerAttact () {

    }
    public virtual void FinishAttact () {
        
    }
}