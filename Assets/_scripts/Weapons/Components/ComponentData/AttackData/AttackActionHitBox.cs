using System;
using Tero.Weapons.Components;
using UnityEngine;

namespace Tero.Weapons.Components
{
    [Serializable]
    public class AttackActionHitBox : AttackData
    {
        public bool Debug;
        [field: SerializeField] public Rect HitBox { get; private set; }
    }
}