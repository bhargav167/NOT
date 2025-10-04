using Tero.Combat.KnockBack;
using Tero.ModifierSystem;
using UnityEngine;
namespace Tero.CoreSystem
{

    public class KnockBackReceiver : CoreComponent, IKnockBackable
    {
        public Modifiers<Modifier<KnockBackData>, KnockBackData> Modifiers { get; } = new();
        [SerializeField] private float maxKnockBackTime = 0.2f;

        public bool isKnockBackActive;
        public bool isKnockBackByGranadeActive;
        private float knockBackStartTime;
        private float _stuntTime = 3.5f;
        private Movement movement;
        public override void LogicUpdate()
        {
            CheckKnockBack();
            CheckKnockBackGranade();
        }

        public void KnockBack(KnockBackData data)
        {
            data = Modifiers.ApplyAllModifiers(data);
            movement.SetVelocity(data.Strength, data.Angle, data.Direction);
            movement.CanSetVelocity = false;
            isKnockBackActive = true;
            knockBackStartTime = Time.time;
        }
        
        public void KnockBackByGrenades(KnockBackData data)
        {
            isKnockBackByGranadeActive = true;
            movement.RB.gravityScale = 0;
            data = Modifiers.ApplyAllModifiers(data);
            movement.SetVelocity(data.Strength, data.Angle, data.Direction);
            movement.CanSetVelocity = false;
            knockBackStartTime = Time.time;
        }
        private void CheckKnockBack()
        {
            if ((isKnockBackActive)
                && ((movement.CurrectVelocity.y <= 0.01f)
                    && Time.time >= knockBackStartTime + maxKnockBackTime)
               )
            {
                isKnockBackActive = false;
                movement.CanSetVelocity = true;
            }
        }
        private void CheckKnockBackGranade()
        {
            if (isKnockBackByGranadeActive
                && ((movement.CurrectVelocity.y <= 0.01f)
                    && Time.time >= knockBackStartTime + maxKnockBackTime)
               )
            {
                movement.CanSetVelocity = true;
                movement.SetVelocity(0, Vector2.zero, 0);
            }
            if(Time.time >= knockBackStartTime + maxKnockBackTime)
                movement.RB.gravityScale = 10;
            if (Time.time >= knockBackStartTime + _stuntTime)
                isKnockBackByGranadeActive = false;
        }

        protected override void Awake()
        {
            base.Awake();
            movement = core.getCoreComponents<Movement>();
        }
    }
}