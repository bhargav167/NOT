using UnityEngine;
using Tero.CoreSystem;
public class IdleState : States {
    private Movement Movement {get=>movement ?? core.getCoreComponents(ref movement);} 
    private Movement movement;
    protected D_IdleState stateData;
    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected bool isPlayerInMinArgoFrontRange;
    protected bool isPlayerInMinArgoBackRange;
    protected bool isPlayerInMinArgoUpRange;
    protected float idleTime;
    public IdleState (Entity entity, FinateStateMachine stateMachine, string animBoolName, D_IdleState stateData) 
    : base (entity, stateMachine, animBoolName) {
        this.stateData = stateData;
    }
    public override void DoCheck()
    {
        base.DoCheck();
        isPlayerInMinArgoFrontRange = entity.CheckPlayerInFrontMinAgroRange();
        isPlayerInMinArgoBackRange = entity.CheckPlayerInBackMinAgroRange();
        isPlayerInMinArgoUpRange = entity.CheckPlayerInUpMinAgroRange();
        
    }

    public override void Enter () {
        base.Enter ();
        Movement.SetVelocityX (0); 
        isIdleTimeOver = false;
        SetIdleTime ();
    } 
    public override void Exist () {
        base.Exist ();
        if (flipAfterIdle) {
            Movement.Flip ();
        }
    } 
    public override void LogicUpdate () {
        base.LogicUpdate ();
        Movement.SetVelocityX (0); 
        if (Time.time >= startTime + idleTime) {
            isIdleTimeOver = true;
        }
    }
    public override void PhysicsUpdate () {
        base.PhysicsUpdate ();
    }
    public void SetFlipAfterIdle (bool flip) {
        flipAfterIdle = flip;
    }
    public void SetIdleTime () {
        idleTime = Random.Range (stateData.minIdleTime, stateData.maxIdleTime);
    } 
}