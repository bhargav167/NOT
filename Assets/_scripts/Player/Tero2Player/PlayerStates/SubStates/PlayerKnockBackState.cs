using Tero.CoreSystem;
public class PlayerKnockBackState :PlayerAbitityState
{ 
    protected KnockBackReceiver Combat {get=>combat ?? core.getCoreComponents(ref combat);} 
    private KnockBackReceiver combat;

    protected Stats Stats { get => stats ?? core.getCoreComponents(ref stats); }
    private Stats stats;
    public PlayerKnockBackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base (player, stateMachine, playerData, animBoolName){
    }
    public override void Enter () {
        base.Enter (); 
    }
    public override void Exit () {
        base.Exit ();
    }
    public override void LogicUpdate(){
         base.LogicUpdate();
        if (Combat.isKnockBackActive == false)
        {
            stateMachine.ChangeState(Player.IdleState);
        }
    }
    #region Animation Trigger
    public void AnimationFinishTrigger()
    {
        isAbilityDone = true;
    }
    #endregion
}
