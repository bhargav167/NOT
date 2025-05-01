
using Tero.CoreSystem;

public class E2_IdleState : IdleState
{
     private Enemy2 enemy;
    protected KnockBackReceiver Combat { get => combat ?? core.getCoreComponents(ref combat); }
    private KnockBackReceiver combat;
    protected Death Death { get => death ?? core.getCoreComponents(ref death); }
    private Death death;
    public E2_IdleState(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_IdleState stateData,Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
    } 

    public override void Exist()
    {
        base.Exist();
    } 
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isPlayerInMinArgoFrontRange || isPlayerInMinArgoBackRange){
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        if (Combat.isKnockBackActive)
        {
            stateMachine.ChangeState(enemy.knockState);
        }
        else if(isIdleTimeOver){
            stateMachine.ChangeState(enemy.moveState);
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

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    } 
}
