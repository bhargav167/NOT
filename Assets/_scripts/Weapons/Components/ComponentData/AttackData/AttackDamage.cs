using System;
using Tero.Weapons.Components;
using UnityEngine;

namespace Tero.Weapons.Components
{
    [Serializable]
    public class AttackDamage : AttackData
    {
        [field: SerializeField] public float Amount { get; private set; }
    }
}