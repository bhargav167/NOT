
using Tero.CoreSystem;

namespace Tero.Weapons.Components
{
    public class Movement : WeaponComponent<MovementData, AttackMovement>{
        private CoreSystem.Movement coreMovement;

        private CoreSystem.Movement CoreMovement => coreMovement ? coreMovement : Core.getCoreComponents(ref coreMovement);
        private void HandleStartMovement()
        {
            CoreMovement.SetVelocity(currentAttackData.Velocity, currentAttackData.Direction, CoreMovement.FacingDirection);
        }

        private void HandleStopMovement()
        {
            CoreMovement.SetVelocityZero();
        }

        protected override void Start()
        {
            base.Start();
            AnimationEventHandler.OnStartMovement += HandleStartMovement;
            AnimationEventHandler.OnStopMovement += HandleStopMovement;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            AnimationEventHandler.OnStartMovement -= HandleStartMovement;
            AnimationEventHandler.OnStopMovement -= HandleStopMovement;
        }
    }
}
