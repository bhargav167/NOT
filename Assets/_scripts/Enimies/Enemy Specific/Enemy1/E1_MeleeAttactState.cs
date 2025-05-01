using Tero.CoreSystem;
using UnityEngine;

public class E1_MeleeAttactState : MeleeAttactState
{
     private Enemy1 enemy;
    protected Movement Movement { get => movement ?? core.getCoreComponents(ref movement); }
    private Movement movement;
    protected KnockBackReceiver Combat { get => combat ?? core.getCoreComponents(ref combat); }
    private KnockBackReceiver combat;
    protected Stats Stats { get => stats ?? core.getCoreComponents(ref stats); }
    private Stats stats;
    public E1_MeleeAttactState(Entity entity, FinateStateMachine stateMachine, string animBoolName, Transform attactPosition, D_MeleeAttact stateData,Enemy1 enemy) : base(entity, stateMachine, animBoolName, attactPosition, stateData)
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

    public override void FinishAttact()
    {
        base.FinishAttact();
    }
 
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Combat.isKnockBackActive)
        {
            stateMachine.ChangeState(enemy.knockState);
        }
        if (isAnimationFinished){
           
            if (isPlayerIsInMinAgroFrontRange){
                stateMachine.ChangeState(enemy.playerDetectedState);
            }else{
                stateMachine.ChangeState(enemy.lookingForPlayerState);
            }
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void TriggerAttact()
    {
        base.TriggerAttact();
    }
}
