using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tero.CoreSystem;
using System.Runtime.Serialization.Formatters;
public class MoveState : States
{
    private Movement Movement { get => movement ?? core.getCoreComponents(ref movement); }
    private Movement movement;
    private CollisionSences CollisionSences { get => collisionSences ?? core.getCoreComponents(ref collisionSences); }
    private CollisionSences collisionSences;
    private Stats Stats { get => stats ?? core.getCoreComponents(ref stats); }
    private Stats stats;
    protected KnockBackReceiver Combat { get => combat ?? core.getCoreComponents(ref combat); }
    private KnockBackReceiver combat;
    protected D_MoveState stateData;
    protected bool isDetectedWall;
    protected bool isDetectedHideObjectFront; 
    protected bool isDetectedHideObjectBack;
    protected bool isDetectedLedger;
    protected bool isPlayerMaxAgroRange;
    protected bool isPlayerMinAgroFrontRange;
    protected bool isPlayerMinAgroBackRange;
    protected bool isPlayerMinAgroUpRange;
    protected bool isPlayerMaxRayHitting;
    protected bool ishiding; 
    protected RaycastHit2D closestHit;
    public MoveState(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoCheck()
    {
        base.DoCheck();
        if (CollisionSences)
        {
            isDetectedLedger = CollisionSences.LedgeVertical;
            isDetectedWall = CollisionSences.Wall;
        }
        isPlayerMinAgroFrontRange = entity.CheckPlayerInFrontMinAgroRange();
        isPlayerMinAgroBackRange = entity.CheckPlayerInBackMinAgroRange();
        isPlayerMinAgroUpRange = entity.CheckPlayerInUpMinAgroRange();
        isPlayerMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        isPlayerMaxRayHitting = entity.CastRayForPlayerCheck();
        closestHit = entity.GetClosestHitFromPlayerCheck(); 
    }

    public override void Enter()
    {
        base.Enter();
        Movement.SetVelocityX(stateData.moveSpeed * Movement.FacingDirection);
        
    }
    public override void Exist()
    {
        base.Exist();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate(); 
        Movement.SetVelocityX(stateData.moveSpeed * Movement.FacingDirection);
        if (closestHit.collider != null)
            ishiding = closestHit.collider.gameObject.layer == LayerMask.NameToLayer("HideObject");
        else
            ishiding = false;
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}