using UnityEngine;

namespace Tero.Weapons.Components
{
    public class
        ChargeToProjectileSpawner : WeaponComponent<ChargeToProjectileSpawnerData, AttackChargeToProjectileSpawner>
    {
        private ProjectileSpawner projectileSpawner;
        private Charge charge;

        private bool hasReadCharge;

        private ChargeProjectileSpawnerStrategy chargeProjectileSpawnerStrategy = new ChargeProjectileSpawnerStrategy();

        protected override void HandleEnter()
        {
            base.HandleEnter();

            hasReadCharge = false;
        }

        // Handles input change. Performs action when input is false
        private void HandleCurrentInputChange(bool newInput)
        {
            if (newInput || hasReadCharge)
                return;

            // Set the current information in the strategy
            chargeProjectileSpawnerStrategy.AngleVariation = currentAttackData.AngleVariation;
            chargeProjectileSpawnerStrategy.ChargeAmount = charge.TakeFinalChargeReading();

            // Set the strategy
            projectileSpawner.SetProjectileSpawnerStrategy(chargeProjectileSpawnerStrategy);

            // Turns off handle function till the next attack
            hasReadCharge = true;
        }

        #region Plumbing

        protected override void Start()
        {
            base.Start();

            projectileSpawner = GetComponent<ProjectileSpawner>();
            charge = GetComponent<Charge>();

            weapon.OnCurrentInputChange += HandleCurrentInputChange;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            weapon.OnCurrentInputChange -= HandleCurrentInputChange;
        }

        #endregion
    }
}