using System;
using Tero.ProjectileSystem;
using Tero.ProjectileSystem.DataPackages;
using UnityEngine;

namespace Tero.Weapons.Components
{
    [Serializable]
    public class AttackProjectileSpawner : AttackData
    {
        // This is an array as each attack can spawn multiple projectiles.
        [field: SerializeField] public ProjectileSpawnInfo[] SpawnInfos { get; private set; }
    }

    [Serializable]
    public struct ProjectileSpawnInfo
    {
        [field: SerializeField] public string SpawnerName { get; set; }
        [field: SerializeField] public Quaternion Direction { get; set; }
        // The projectile prefab, notice that the type is Projectile and not GameObject
        [field: SerializeField] public Projectile ProjectilePrefab { get; private set; }

        // The data to be passed to the projectile when it is spawned
        [field: SerializeField] public DamageDataPackage DamageData { get; private set; }
        [field: SerializeField] public KnockBackDataPackage KnockBackData { get; private set; }
        [field: SerializeField] public PoiseDamageDataPackage PoiseDamageData { get; private set; }
        [field: SerializeField] public SpriteDataPackage SpriteDataPackage { get; private set; }
    }
}