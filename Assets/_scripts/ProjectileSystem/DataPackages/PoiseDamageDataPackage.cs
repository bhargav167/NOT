using System;
using UnityEngine;

namespace Tero.ProjectileSystem.DataPackages
{
    [Serializable]
    public class PoiseDamageDataPackage : ProjectileDataPackage
    {
        [field: SerializeField] public float Amount { get; private set; }
    }
}