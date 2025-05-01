using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tero.CoreSystem; 
public class RangedAttactState : AttactState
{
     protected Movement Movement {get=>movement ?? core.getCoreComponents(ref movement);} 
     private Movement movement;
    protected KnockBackReceiver Combat { get => combat ?? core.getCoreComponents(ref combat); }
    private KnockBackReceiver combat;
    protected D_RangedAttactState stateData;
    protected int ShootingDirection;
    protected GameObject projectile; 
    
    
    public RangedAttactState(Entity entity, FinateStateMachine stateMachine, string animBoolName, Transform attactPosition,D_RangedAttactState stateData) : base(entity, stateMachine, animBoolName, attactPosition)
    {
        this.stateData=stateData; 
    } 
    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
    } 
    public override void Exist()
    {
        base.Exist();
    }

    public override void FinishAttact()
    {
        base.FinishAttact();
    } 
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        ShootingDirection=Movement.FacingDirection;
    } 

    public override void TriggerAttact()
    {
        base.TriggerAttact();
        projectile = GameObject.Instantiate(stateData.projectile, attactPosition.position, attactPosition.rotation);
        projectile.GetComponent<Tero.ProjectileSystem.Components.Movement>().Speed *= ShootingDirection;
    }
}
