using UnityEngine;

namespace Tero.Combat.Damage
{
    public interface IDamageable
    {
        void Damage(DamageData data, RaycastHit2D? hit);
    }
}