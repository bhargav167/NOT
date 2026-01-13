using System;
using Tero.ObjectPoolSystem;
using Tero.ProjectileSystem;
using Tero.ProjectileSystem.Components;
using Tero.Weapons.Components;
using UnityEngine;

namespace Tero.Weapons
{
    public class ProjectileSpawnerStrategy : IProjectileSpawnerStrategy
    {
        // Working variables
        private Vector2 spawnPos;
        private Vector2 spawnDir;
        private Projectile currentProjectile;
        // The function used to initiate the strategy
        public virtual void ExecuteSpawnStrategy
        (
            ProjectileSpawnInfo projectileSpawnInfo,
            Vector3 spawnerPos,
            Quaternion spawnerDir,
            int facingDirection,
            ObjectPools objectPools,
            Action<Projectile> OnSpawnProjectile
        )
        {
            // Simply spawns one projectile based on the passed in parameters
            SpawnProjectile(projectileSpawnInfo, spawnerDir, spawnerPos, facingDirection,
               objectPools, OnSpawnProjectile);
        }

        // Spawn a projectile
        public virtual void SpawnProjectile(ProjectileSpawnInfo projectileSpawnInfo, Quaternion spawnDirection,
            Vector3 spawnerPos,
            int facingDirection,
            ObjectPools objectPools, Action<Projectile> OnSpawnProjectile)
        {
            SetSpawnPosition(spawnerPos, spawnerPos, facingDirection);
            SetSpawnDirection(spawnDirection, facingDirection);
            GetProjectileAndSetPositionAndRotation(objectPools, projectileSpawnInfo.ProjectilePrefab, spawnDirection);
            InitializeProjectile(projectileSpawnInfo, OnSpawnProjectile);
        }

        protected virtual void GetProjectileAndSetPositionAndRotation(ObjectPools objectPools, Projectile prefab,Quaternion dir)
        {
            // Get projectile from pool
            currentProjectile = objectPools.GetObject(prefab);
            // Set position, rotation, and other related info
            currentProjectile.transform.position = spawnPos;
            //Bullet z-azix rotation adjusted
            dir.z -= 0.02f;
            //END
            currentProjectile.transform.rotation = dir;
        }

        protected virtual void InitializeProjectile(ProjectileSpawnInfo projectileSpawnInfo,
            Action<Projectile> OnSpawnProjectile)
        {
            // Reset projectile from pool
            currentProjectile.Reset();

            // Send through new data packages
            currentProjectile.SendDataPackage(projectileSpawnInfo.DamageData);
            currentProjectile.SendDataPackage(projectileSpawnInfo.KnockBackData);
            currentProjectile.SendDataPackage(projectileSpawnInfo.PoiseDamageData);

            // Broadcast new projectile has been spawned so other components can  pass through data packages
            OnSpawnProjectile?.Invoke(currentProjectile);

            // Initialize the projectile
            currentProjectile.Init();
        }

        protected virtual void SetSpawnDirection(Quaternion direction, int facingDirection)
        {
            // Change spawn direction based on FacingDirection
            spawnDir.Set(direction.x * facingDirection,
                direction.y);
        }

        protected virtual void SetSpawnPosition(Vector3 referencePosition, Vector2 offset, int facingDirection)
        {
            // Add offset to player position, accounting for FacingDirection
            spawnPos = referencePosition;
            spawnPos.Set(
                spawnPos.x,
                spawnPos.y
            );
        }
    }
}