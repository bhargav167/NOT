using UnityEngine;
using Tero.CoreSystem;
public class LookForPlayerState : States {
     protected Movement Movement {get=>movement ?? core.getCoreComponents(ref movement);} 
     private Movement movement;
    protected D_LookForPlayer stateData;
    protected bool turnImmediatly;
    protected bool isPlayerMinAgroFrontRange;
    protected bool isPlayerMinAgroBackRange;
    protected bool isAllTurnDone;
    protected bool isAllTurnTimeDone;
    protected float lastTurnTime;
    protected int amountOfTurnDone;
    public LookForPlayerState (Entity entity, FinateStateMachine stateMachine, string animBoolName, D_LookForPlayer stateData) : base (entity, stateMachine, animBoolName) {
        this.stateData = stateData;
    }

    public override void DoCheck () {
        base.DoCheck ();
        isPlayerMinAgroFrontRange = entity.CheckPlayerInFrontMinAgroRange ();
        isPlayerMinAgroBackRange = entity.CheckPlayerInBackMinAgroRange();
    }

    public override void Enter () {
        base.Enter ();
        isAllTurnDone = false;
        isAllTurnTimeDone = false;
        lastTurnTime = startTime;
        amountOfTurnDone = 0;

        Movement.SetVelocityX (0f);
    }

    public override void Exist () {
        base.Exist ();
    }
    public override void LogicUpdate () {
        base.LogicUpdate ();
        Movement.SetVelocityX (0f);
        if (turnImmediatly) {
            Movement.Flip ();
            lastTurnTime = Time.time;
            amountOfTurnDone++;
            turnImmediatly = false;
        } else if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && !isAllTurnDone) {
            Movement.Flip ();
            lastTurnTime = Time.time;
            amountOfTurnDone++;
        }
        if (amountOfTurnDone >= stateData.amountOfTurn) {
            isAllTurnDone = true;
        }
        if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && isAllTurnDone) {
            isAllTurnTimeDone = true;
        }
    }

    public override void PhysicsUpdate () {
        base.PhysicsUpdate ();
    }
    public void SetTurnImmediatly (bool flip) {
        turnImmediatly = flip;
    }
}