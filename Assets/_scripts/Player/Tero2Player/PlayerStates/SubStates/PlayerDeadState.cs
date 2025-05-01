using System.Collections;
using System.Collections.Generic;
using Tero.CoreSystem;
using UnityEngine;

public class PlayerDeadState :PlayerAbitityState
{
    public PlayerDeadState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,string animBoolName) : base (player, stateMachine, playerData, animBoolName){
    }
    public override void Enter () { 
        base.Enter ();
        
    }
    public override void Exit () {
        base.Exit (); 
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
    }
     

    #region Animation Trigger
    public void AnimationFinishTrigger () {
        isAbilityDone = true;
    }
    #endregion
}
