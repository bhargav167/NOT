using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tero.CoreSystem;
using static Tero.PolicHidePosition;
using System;
using Unity.Mathematics;

public class HideState : States
{
    protected Movement Movement { get => movement ?? core.getCoreComponents(ref movement); }
    protected CollisionSences CollisionSences { get => collisionSences ?? core.getCoreComponents(ref collisionSences); }
    private Movement movement;
    private CollisionSences collisionSences;
    protected Stats Stats { get => stats ?? core.getCoreComponents(ref stats); }
    private Stats stats;
    public HideState(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_PlayerDetctData stateData) : base(entity, stateMachine, animBoolName)
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
            Flip();
        }
    }
    public override void Exist()
    {
        base.Exist();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        entity.MoveToHidePosition();
        if (entity.currentHideStatus == HideStatus.Returning)
        { 
            entity.ReturnToOriginalPosition();
        }
    }
    private void Flip()
    {
        if (entity.policeTransform.localScale.x == 0.5f && !entity.flippedToHide)
        {
            entity.flippedToHide = true;
            entity.policeTransform.localScale = new Vector3(-entity.policeTransform.localScale.x, entity.policeTransform.localScale.y, entity.policeTransform.localScale.z);
        }
        if (entity.policeTransform.localScale.x == -0.5f && !entity.flippedToHide)
        {
            entity.flippedToHide = true;
            entity.policeTransform.localScale = new Vector3(MathF.Abs(entity.policeTransform.localScale.x), entity.policeTransform.localScale.y, entity.policeTransform.localScale.z);
        }
    }
} 