using System;
using Tero.ObjectPoolSystem;
using Tero.ProjectileSystem;
using Tero.Weapons.Components;
using UnityEngine;

namespace Tero.Weapons
{
    [Serializable]
    public class ChargeProjectileSpawnerStrategy : ProjectileSpawnerStrategy
    {
        public float AngleVariation;
        public int ChargeAmount;

        private Vector2 currentDirection;

    }
}