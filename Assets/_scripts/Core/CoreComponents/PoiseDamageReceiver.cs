using Tero.Combat.PoiseDamage;
using Tero.ModifierSystem;

namespace Tero.CoreSystem
{
    public class PoiseDamageReceiver : CoreComponent, IPoiseDamageable
    {
        private Stats stats;

        public Modifiers<Modifier<PoiseDamageData>, PoiseDamageData> Modifiers { get; } = new();

        public void DamagePoise(PoiseDamageData data)
        {
            data = Modifiers.ApplyAllModifiers(data);

            stats.Poise.Decrease(data.Amount,null);
        }

        protected override void Awake()
        {
            base.Awake();

            stats = core.getCoreComponents<Stats>();
        }
    }
}