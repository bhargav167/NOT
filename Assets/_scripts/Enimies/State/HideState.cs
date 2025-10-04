using UnityEngine;
using Tero.CoreSystem;
using static Tero.PolicHidePosition;
using System;
using Tero;

public class HideState : States
{
    protected Movement Movement { get => movement ?? core.getCoreComponents(ref movement); }
    protected CollisionSences CollisionSences { get => collisionSences ?? core.getCoreComponents(ref collisionSences); }
    private Movement movement;
    private CollisionSences collisionSences;
    protected Stats Stats { get => stats ?? core.getCoreComponents(ref stats); }
    private Stats stats;
    public bool isHidden = false;
    public HideState(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_HideStateData stateData) : base(entity, stateMachine, animBoolName)
    {
    }
    public override void DoCheck()
    {
        base.DoCheck();
    } 
    public override void Enter()
    {
        base.Enter();
        entity.currentHideStatus = HideStatus.MovingToHide;
        if (entity.currentHittingDirection == HittingDirection.Front)
        {
        }
        if (entity.currentHittingDirection == HittingDirection.Back)
        {
            entity.Flip();
        }
    }
    public override void Exist()
    {
        base.Exist();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        entity.MoveToHidePosition();
        if (entity.currentHideStatus == HideStatus.Returning)
        {
            entity.ReturnToOriginalPosition();
        }
        if (entity.currentHideStatus == HideStatus.Hiding)
        {
            isHidden = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
} 