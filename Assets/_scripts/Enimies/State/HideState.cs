using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tero.CoreSystem;

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

    public override void Enter () {
        base.Enter (); 
    }
    public override void Exist () {
        base.Exist ();
    }
    public override void LogicUpdate () {
        base.LogicUpdate ();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        entity.MoveToHidePosition();
    }
} 