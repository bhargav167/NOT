using System.Collections;
using System.Collections.Generic;
using Tero.Combat.Damage;
using Tero.Combat.KnockBack;
using Tero.Combat.PoiseDamage;
using Tero.CoreSystem;
using UnityEngine;
public class MeleeAttactState : AttactState {
	private Movement Movement { get => movement ?? core.getCoreComponents(ref movement); }
	private CollisionSences CollisionSenses { get => collisionSenses ?? core.getCoreComponents(ref collisionSenses); }

	private Movement movement;
	private CollisionSences collisionSenses;

	protected D_MeleeAttact stateData;

	public MeleeAttactState(Entity etity, FinateStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttact stateData) : base(etity, stateMachine, animBoolName, attackPosition)
	{
		this.stateData = stateData;
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
	}

    public override void TriggerAttact()
    {
        base.TriggerAttact();

		Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attactPosition.position, stateData.attactRadius, stateData.whatIsPlayer);

		foreach (Collider2D collider in detectedObjects)
		{
			IDamageable damageable = collider.GetComponent<IDamageable>();

			if (damageable != null)
			{
				damageable.Damage(new DamageData(stateData.attactDamage, core.Root),null);
			}

			IKnockBackable knockBackable = collider.GetComponent<IKnockBackable>();

			if (knockBackable != null)
			{
				knockBackable.KnockBack(new KnockBackData(stateData.knockbackAngle, stateData.knockbackStrength, Movement.FacingDirection, core.Root));
			}

			if (collider.TryGetComponent(out IPoiseDamageable poiseDamageable))
			{
				poiseDamageable.DamagePoise(new PoiseDamageData(stateData.PoiseDamage, core.Root));
			}
		}
	}
}