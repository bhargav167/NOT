using System.Collections;
using System.Collections.Generic;
using Tero;
using Tero.Assets._scripts.Core.CoreComponents;
using Tero.CoreSystem;
using UnityEngine;
public class PlayerDetectState : States {
    protected Movement Movement { get => movement ?? core.getCoreComponents(ref movement); }
    protected CollisionSences CollisionSences { get => collisionSences ?? core.getCoreComponents(ref collisionSences); }
    private Movement movement;
    protected BaseKnockback _BaseCombat { get => _baseCombat ?? core.getCoreComponents(ref _baseCombat); }
    private BaseKnockback _baseCombat;
    private CollisionSences collisionSences;
    protected Stats Stats { get => stats ?? core.getCoreComponents(ref stats); }
    private Stats stats;
    protected D_PlayerDetctData stateData;
    protected bool isPlayerMinAgroFrontRange;
    protected bool isPlayerMinAgroBackRange;
    protected bool isPlayerMaxAgroRange;
    protected bool performLongRangAction;
    protected bool performCloseRangeAction;
    protected bool isPlayerMinAgroUpRange;
    protected bool isDetectedLedger;
    protected bool ishiding;
    private RaycastHit2D closestHit = new RaycastHit2D();
    public PlayerDetectState (Entity entity, FinateStateMachine stateMachine, string animBoolName, D_PlayerDetctData stateData) : base (entity, stateMachine, animBoolName) {
        this.stateData = stateData;
    }

    public override void DoCheck()
    {
        base.DoCheck(); 
        isPlayerMinAgroFrontRange = entity.CheckPlayerInFrontMinAgroRange();
        isPlayerMinAgroBackRange = entity.CheckPlayerInBackMinAgroRange();
        isPlayerMinAgroUpRange = entity.CheckPlayerInUpMinAgroRange();
        isPlayerMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        closestHit = entity.GetClosestHitFromPlayerCheck();
    }

    public override void Enter()
    {
        base.Enter();
        performLongRangAction = false;
        Movement.SetVelocityX(0f); 
    }
    public override void Exist () {
        base.Exist ();
    }
    public override void LogicUpdate () {
        base.LogicUpdate ();
        Movement.SetVelocityX (0f);
        if(Time.time>=startTime+stateData.longRangeActionTime){
            performLongRangAction=true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (closestHit.collider != null && _BaseCombat.isKnockBackActive)
            ishiding = closestHit.collider.gameObject.layer == LayerMask.NameToLayer(ObjectName.HideObject.ToString());
        else
            ishiding = false;
    }
}