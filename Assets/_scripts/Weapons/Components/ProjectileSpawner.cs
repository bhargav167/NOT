using System;
using Tero.ObjectPoolSystem;
using Tero.ProjectileSystem;
using UnityEngine;

namespace Tero.Weapons.Components
{
    public class ProjectileSpawner : WeaponComponent<ProjectileSpawnerData, AttackProjectileSpawner>
    {
        // Event fired off for each projectile before we call the Init() function on that projectile to allow other components to also pass through some data
        public event Action<Projectile> OnSpawnProjectile;
        public Player player;

        // Movement Core Comp needed to get FacingDirection 
        private CoreSystem.Movement coreMovement;
        private CoreSystem.Movement CoreMovement => coreMovement ? coreMovement : Core.getCoreComponents(ref coreMovement);
        // Object pool to store projectiles so we don't have to keep instantiating new ones
        private readonly ObjectPools objectPools = new ObjectPools();

        // The strategy we use to spawn a projectile
        private IProjectileSpawnerStrategy projectileSpawnerStrategy;

        public void SetProjectileSpawnerStrategy(IProjectileSpawnerStrategy newStrategy)
        {
            projectileSpawnerStrategy = newStrategy;
        }

        // Weapon Action Animation Event is used to trigger firing the projectiles
        private void HandleAttackAction()
        {
            // Spawn projectile based on the current strategy
            if (currentAttackData != null)
            {
                foreach (var projectileSpawnInfo in currentAttackData.SpawnInfos)
                {
                    if (projectileSpawnInfo.SpawnerName == ObjectName.BulletFire.ToString() && player.InputHandeler.IsreadyToFire){
                        // Spawn projectile based on the current strategy 
                        ParticleSystem particleSystem = GameObject.Instantiate(weapon.shootEffect, weapon.shootPoint.transform.position, Quaternion.Euler(weapon.shootPoint.transform.rotation.x, weapon.shootPoint.transform.rotation.y, weapon.shootPoint.transform.rotation.z-0.4f)) as ParticleSystem;
                        // particleSystem.startRotation = projectileSpawnInfo.Direction.y;
                        particleSystem.Play();
                        projectileSpawnerStrategy.ExecuteSpawnStrategy(projectileSpawnInfo, weapon.shootPoint.position, projectileSpawnInfo.Direction,
                                     -CoreMovement.FacingDirection, objectPools, OnSpawnProjectile);
                    } 
                    if (player.InputHandeler.IsreadyToThrow){
                        player.InputHandeler.IsreadyToThrow = false;
                        projectileSpawnerStrategy.ExecuteSpawnStrategy(projectileSpawnInfo, weapon.shootPoint.position, projectileSpawnInfo.Direction,
                                     -CoreMovement.FacingDirection, objectPools, OnSpawnProjectile);
                    }
                      if(projectileSpawnInfo.SpawnerName == ObjectName.BulletExtracter.ToString() && player.InputHandeler.IsreadyToFire){
                        projectileSpawnerStrategy.ExecuteSpawnStrategy(projectileSpawnInfo, weapon.bulletExtractPoint.position, projectileSpawnInfo.Direction,
                           CoreMovement.FacingDirection, objectPools, OnSpawnProjectile);
                    }
                }
            }
        }

        private void SetDefaultProjectileSpawnStrategy()
        {
            // The default spawn strategy is the base ProjectileSpawnerStrategy class. It simply spawns one projectile based on the data per request
            projectileSpawnerStrategy = new ProjectileSpawnerStrategy();
        }

        protected override void HandleExit()
        {
            base.HandleExit();
            // Reset the spawner strategy every time the attack finishes in case some other component adjusted it
            SetDefaultProjectileSpawnStrategy();
        }

        #region Plumbing

        protected override void Awake()
        {
            base.Awake();
            player = GetComponentInParent<Player>();
            SetDefaultProjectileSpawnStrategy();
        }

        protected override void Start()
        {
            base.Start();
            AnimationEventHandler.OnAttackAction += HandleAttackAction;
        } 
        protected override void OnDestroy()
        {
            base.OnDestroy();

            AnimationEventHandler.OnAttackAction -= HandleAttackAction;
        }

        private void OnDrawGizmosSelected()
        {
            if (data == null || !Application.isPlaying)
                return;

            foreach (var item in data.GetAllAttackData())
            {
                foreach (var point in item.SpawnInfos)
                {
                    var pos = (Vector3)weapon.shootPoint.position;

                    Gizmos.DrawWireSphere(pos, 0.2f);
                    Gizmos.color = Color.red;
                  //  Gizmos.DrawLine(pos, pos + (Quaternion)point.Direction.normalized);
                    Gizmos.color = Color.white;
                }
            }
        }

        #endregion
    }
}