using Tero;
using Tero.Assets._scripts.Core.CoreComponents;
using Tero.CoreSystem;
using UnityEngine;
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

    protected BaseKnockback _BaseCombat { get => _baseCombat ?? core.getCoreComponents(ref _baseCombat); }
    private BaseKnockback _baseCombat;
    protected Death Death { get => death ?? core.getCoreComponents(ref death); }
    private Death death;
    protected HeadKnockbackReciver HeadCombat { get => headcombat ?? core.getCoreComponents(ref headcombat); }
    private HeadKnockbackReciver headcombat;
    protected LegsKnockbackReciver LegCombat { get => legcombat ?? core.getCoreComponents(ref legcombat); }
    private LegsKnockbackReciver legcombat;
    protected KnockBackLeftReceiver Combat1 { get => combat1 ?? core.getCoreComponents(ref combat1); }
    private KnockBackLeftReceiver combat1;
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
    protected bool isPlayerRight;
    protected bool ishiding; 
    protected RaycastHit2D closestHit;
    public MoveState(Entity entity, FinateStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoCheck()
    {
        base.DoCheck();
        if (CollisionSences){
            isDetectedLedger = CollisionSences.LedgeVertical;
            isDetectedWall = CollisionSences.Wall;
        }
        isPlayerMinAgroFrontRange = entity.CheckPlayerInFrontMinAgroRange();
        isPlayerMinAgroBackRange = entity.CheckPlayerInBackMinAgroRange();
        isPlayerMinAgroUpRange = entity.CheckPlayerInUpMinAgroRange();
        isPlayerMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        isPlayerMaxRayHitting = entity.CastRayForPlayerCheck();
        closestHit = entity.GetClosestHitFromPlayerCheck();
        isPlayerRight = entity.StaticCastRayForPlayerRight(); // this will cast only right side (IMP)
    }

    public override void Enter()
    {
        base.Enter();
        Movement.SetVelocityX(stateData.moveSpeed * Movement.FacingDirection);
    }
    public override void Exist(){
        base.Exist();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        Movement.SetVelocityX(stateData.moveSpeed * Movement.FacingDirection);
        if (closestHit.collider != null && _BaseCombat.isKnockBackActive)
            ishiding = closestHit.collider.gameObject.layer == LayerMask.NameToLayer(ObjectName.HideObject.ToString());
        else
            ishiding = false;
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}