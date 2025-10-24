using System;
using Tero;
using Tero.CoreSystem;
using UnityEditor.Overlays;
using UnityEngine;
using static Tero.PolicHidePosition;

public class HideState : States
{
    protected Movement Movement { get => movement ?? core.getCoreComponents(ref movement); }
    protected CollisionSences CollisionSences { get => collisionSences ?? core.getCoreComponents(ref collisionSences); }
    private Movement movement;
    private CollisionSences collisionSences;
    protected Stats Stats { get => stats ?? core.getCoreComponents(ref stats); }
    private Stats stats;
    public bool isHidden = false;

    protected bool isPlayerRight;
    public HideState(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_HideStateData stateData) : base(entity, stateMachine, animBoolName)
    {
    }
    public override void DoCheck()
    {
        base.DoCheck();
        isPlayerRight = entity.StaticCastRayForPlayerRight();

    } 
    public override void Enter()
    {
        base.Enter();
        entity.currentHideStatus = HideStatus.MovingToHide;
        if (entity.currentHittingDirection == HittingDirection.Front) { }

        if (entity.currentHittingDirection == HittingDirection.Back)
            Movement.Flip();
    }
    public override void Exist()
    {
        base.Exist();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        entity.MoveToHidePosition(isPlayerRight);
        if (entity.currentHideStatus == HideStatus.Returning)
            entity.ReturnToOriginalPosition();

        if (entity.currentHideStatus == HideStatus.Hiding)
            isHidden = true;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
} 