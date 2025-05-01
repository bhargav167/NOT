
using System;
using Tero.Weapons.Components;
using UnityEngine;

namespace Tero.Weapons.Components
{
    [Serializable]
    public class AttackMovement : AttackData
    {
        [field: SerializeField] public Vector2 Direction { get; private set; }
        [field: SerializeField] public float Velocity { get; private set; }
    }
}