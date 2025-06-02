using UnityEngine;

namespace Tero.CoreSystem
{
    public class Death : CoreComponent
    {
        [SerializeField] private GameObject[] deathParticle;
        private ParticleManager ParticleManager => particleManager ? particleManager : core.getCoreComponents(ref particleManager);
        private ParticleManager particleManager;
        protected Movement Movement {get=>movement ?? core.getCoreComponents(ref movement);}
        private Movement movement;

        private Stats Stats => stats ? stats : core.getCoreComponents(ref stats);
        private Stats stats;
        public bool IsDead { get; private set; } = false;
        public bool IsHeadShot { get; private set; } = false;
        public void Die()
        {
            foreach (var particles in deathParticle)
            {
                particleManager.StartParticles(particles);
            }
            IsDead = true;
        }
        public void HeadDead()
        {
            IsHeadShot = true;
        }
        private void OnEnable()
        {
                Stats.Health.OnHeadShotEvents += HeadDead;
                Stats.Health.OnDieEvents += Die;
        }

        private void OnDisable()
        {
            IsDead = false;
            IsHeadShot = false;
            Stats.Health.OnHeadShotEvents -= HeadDead;
            Stats.Health.OnDieEvents -= Die;
        }
    }
}
