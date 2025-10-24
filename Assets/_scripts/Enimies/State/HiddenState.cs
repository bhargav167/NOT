using Tero.CoreSystem;
using Tero;
using UnityEngine;
using static Tero.PolicHidePosition;

public class HiddenState : States
{
    private Enemy1 _enemy;
    protected Movement Movement { get => movement ?? core.getCoreComponents(ref movement); }
    protected CollisionSences CollisionSences { get => collisionSences ?? core.getCoreComponents(ref collisionSences); }
    private Movement movement;
    private CollisionSences collisionSences;
    protected Stats Stats { get => stats ?? core.getCoreComponents(ref stats); }
    private Stats stats;
    private float hideTimeout = 4f;
    protected bool isPlayerMaxRayHitting;
    public HiddenState(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_HideStateData stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName)
    {
        this._enemy = enemy;
    }
    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        Movement.SetVelocityX(0f);
        entity.currentHideStatus = HideStatus.Hiding;
        //Detect which side to flip based on the hide position
        //Track police facing side and hitting ray at the time of shorted.
        //Based on hidden side need to play animation
    }
    public override void Exist()
    {
        base.Exist();
        ResetHideTimeout();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!entity._IsMovedtoOrignalPos)
            HideTimeout();
        else
            stateMachine.ChangeState(_enemy.lookingForPlayerState);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public void HideTimeout()
    {
        if (Time.time >= startTime + hideTimeout)
        {
            entity.currentHideStatus = HideStatus.Returning;
            entity.ReturnToOriginalPosition();
        }
    }
    public void ResetHideTimeout()
    {
        startTime = Time.time;
        entity.currentHideStatus = HideStatus.NotHiding;
        entity._IsMovedtoOrignalPos=false;
    }
} 