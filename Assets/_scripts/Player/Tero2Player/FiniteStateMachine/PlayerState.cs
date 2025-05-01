using UnityEngine;
using Tero.CoreSystem;
public class PlayerState : MonoBehaviour {
    protected Core core;
    protected Player Player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    protected bool isAnimationFinished;
    protected bool IsExistingState;
    protected float startTime;
    private string animBoolName; 
    public PlayerState (Player player, PlayerStateMachine stateMachine, PlayerData playerData,string animBoolName) {
        this.Player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName; 
        core=Player.Core;
    }
    public virtual void Enter () {
        DoCheck();
        startTime=Time.time;
        Player.animator.SetBool(animBoolName,true);
        isAnimationFinished=false;
        IsExistingState=false;
    }
    public virtual void Exit () {
        Player.animator.SetBool(animBoolName,false);
        IsExistingState=true;
    }
    public virtual void LogicUpdate () {

    }
    public virtual void PhysicsUpdate () {
        DoCheck();
    }
    public virtual void DoCheck () {}
    public virtual void AnimationTrigger(){}

    public virtual void AnimationFinishTrigger() =>isAnimationFinished=true;
}