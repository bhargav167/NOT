using Tero.Combat.Damage;
using Tero.ModifierSystem;
using UnityEngine;

namespace Tero.CoreSystem
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        public Modifiers<Modifier<DamageData>, DamageData> Modifiers { get; } = new();

        private Stats stats;

        public void Damage(DamageData data,RaycastHit2D? hit)
        {
            data = Modifiers.ApplyAllModifiers(data);
            switch (hit?.collider.name)
            {
                case "Player":
                    data.Amount = 1; //Damage Amount setting for police bullet
                    break;
                case "Head":
                    data.Amount = stats.Health.MaxValue;
                    break;
                default:
                    data.Amount = 0.5f;
                    break;
            }
            stats.Health.Decrease(data.Amount, hit);
        }

        protected override void Awake()
        {
            base.Awake();
            stats = core.getCoreComponents<Stats>();
        }
    }
}