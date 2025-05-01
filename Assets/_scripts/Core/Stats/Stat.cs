using System;
using UnityEngine;

namespace Tero.CoreSystem.StatsSystem
{
    [Serializable]
    public class Stat
    {
        public event Action OnDieEvents;
        public event Action OnHeadShotEvents;

        [field: SerializeField] public float MaxValue { get; set; }
        public float CurrentValue
        {
            get => currentValue;
            set
            {
                currentValue = Mathf.Clamp(value, 0f, MaxValue);
            }
        }
        private float currentValue;

        public void Init() => CurrentValue = MaxValue;

        public void Increase(float amount) => CurrentValue += amount;

        public void Decrease(float amount, RaycastHit2D? raycastHit2D) {
            if (raycastHit2D?.collider.name == ObjectName.Head.ToString())
                OnHeadShotEvents?.Invoke();

            CurrentValue -= amount;
            if (currentValue <= 0 && raycastHit2D?.collider.name != ObjectName.Head.ToString())
            {
                OnDieEvents?.Invoke();
            }
                
        } 
    }
}