using System.Collections.Generic;
using Tero.Combat.Damage;
using UnityEngine;

namespace Tero.Utilities
{
    public static class CombatDamageUtilities
    {
        public static bool TryDamage(GameObject gameObject, DamageData damageData, out IDamageable damageable)
        {
            // TryGetComponentInChildren is a custom GameObject extension method.
            if (gameObject.TryGetComponentInChildren(out damageable))
            {
                damageable.Damage(damageData,null);
                return true;
            }

            return false;
        }

        public static bool TryDamage(Collider2D[] colliders, DamageData damageData, out List<IDamageable> damageables)
        {
            var hasDamaged = false;
            damageables = new List<IDamageable>();
            foreach (var collider in colliders)
            {
                if (TryDamage(collider.gameObject, damageData, out IDamageable damageable))
                {
                    damageables.Add(damageable);
                    hasDamaged = true;
                }
            }

            return hasDamaged;
        }
    }
}