
using Tero.Combat.Damage;
using Tero.Utilities;
using UnityEngine;

namespace Tero.Utility
{
    public static class CombatUtilities
    {
        public static void Damage(GameObject gameObject, DamageData damageData)
        {
            // TryGetComponentInChildren is a custom GameObject extension method.
            if (gameObject.TryGetComponentInChildren(out IDamageable damageable))
            {
                damageable.Damage(damageData,null);
            }
        }

        public static void Damage(Collider2D[] colliders, DamageData damageData)
        {
            foreach (var collider in colliders)
            {
                Damage(collider.gameObject, damageData);
            }
        }
    }
}
